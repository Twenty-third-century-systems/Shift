using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Cabinet.Dtos.Internal.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace TurnTable.InternalServices {
    public class TaskService : ITaskService {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public TaskService(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<UnallocatedApplicationResponseDto>> GetAllUnAllocatedApplicationsAsync(int sortingOffice)
        {
            return await _mapper.ProjectTo<UnallocatedApplicationResponseDto>(
                    _context.Applications.Where(a =>
                        a.TaskId == null &&
                        a.CityId.Equals(sortingOffice) &&
                        a.Status.Equals(EApplicationStatus.Submited)))
                .ToListAsync();
        }

        public async Task<int> AllocateTasksAsync(NewTaskAllocationRequestDto dto)
        {
            if (dto.ApplicationId > 0)
            {
                await AllocateSingleApplicationAsync(dto);
            }
            else
            {
                await AllocateMultipleApplicationsAsync(dto);
            }

            await _context.SaveChangesAsync();
            return await _context.Applications.CountAsync(a =>
                a.Service.Equals(dto.Service) && a.CityId.Equals(dto.SortingOffice) && a.TaskId.Equals(null));
        }

        private async Task AllocateMultipleApplicationsAsync(NewTaskAllocationRequestDto dto)
        {
            var applications = await _context.Applications.Where(a =>
                    a.Service == (EService) dto.Service &&
                    a.CityId.Equals(dto.SortingOffice) &&
                    a.TaskId == null)
                .Take(dto.NumberOfApplications)
                .ToListAsync();

            var task = _mapper.Map<ExaminationTask>(dto);

            foreach (var application in applications)
            {
                application.ExaminationTask = task;
                application.Status = EApplicationStatus.Assigned;
            }
        }

        private async Task AllocateSingleApplicationAsync(NewTaskAllocationRequestDto dto)
        {
            var application = await _context.Applications.SingleAsync(a =>
                a.ApplicationId.Equals(dto.ApplicationId) &&
                a.CityId.Equals(dto.SortingOffice) &&
                a.TaskId == null);
            application.ExaminationTask = _mapper.Map<ExaminationTask>(dto);
            application.Status = EApplicationStatus.Assigned;
        }

        public async Task<List<AllocatedTaskResponseDto>> GetAllocatedTasksAsync(Guid examiner)
        {
            return await _mapper.ProjectTo<AllocatedTaskResponseDto>(_context.ExaminationTasks
                    .Include(e => e.Applications).Where(e => e.Examiner.Equals(examiner)))
                .ToListAsync();
        }

        public async Task<List<AllocatedNameSearchTaskApplicationResponseDto>> GetNameSearchTaskApplicationsAsync(
            int taskId)
        {
            return await _mapper.ProjectTo<AllocatedNameSearchTaskApplicationResponseDto>(_context.Applications
                    .Include(a => a.NameSearch).ThenInclude(n => n.Names).Where(a => a.TaskId.Equals(taskId)))
                .ToListAsync();
        }

        public async Task<AllocatedPrivateEntityTaskApplicationResponseDto> GetPrivateEntityTaskApplicationAsync(
            int taskId)
        {
            var applications = await _context.Applications
                .Include(a => a.PrivateEntity)
                .Where(a => a.TaskId.Equals(taskId)).ToListAsync();

            AllocatedPrivateEntityTaskApplicationResponseDto privateEntityApplication = null;
            foreach (var application in applications)
            {
                privateEntityApplication =
                    _mapper.Map<AllocatedPrivateEntityTaskApplicationResponseDto>(application);
            
                await _context.Entry(application.PrivateEntity).Reference(p => p.MemorandumOfAssociation)
                    .Query()
                    .Include(m => m.ShareClauses)
                    .Include(m => m.MemorandumObjects)
                    .LoadAsync();
            
                privateEntityApplication.PrivateEntity.MemorandumOfAssociation =
                    _mapper.Map<TaskPrivateEntityMemorandumResponseDto>(application.PrivateEntity
                        .MemorandumOfAssociation);
            
                await _context.Entry(application.PrivateEntity).Collection(p => p.Members).Query()
                    .Include(m => m.Member)
                    .ThenInclude(m => m.Subscriptions)
                    .ThenInclude(s => s.ShareClauseClass)
                    .LoadAsync();
            
                foreach (var member in application.PrivateEntity.Members)
                {
                    await _context.Entry(member.Member).Collection(m => m.Nominees).LoadAsync();
                    await _context.Entry(member.Member).Collection(m => m.ShareHoldingEntities).Query()
                        .Include(s => s.Entity).LoadAsync();
                    await _context.Entry(member.Member).Collection(m => m.RepresentedForeignEntities).Query()
                        .Include(s => s.ForeignEntity).ThenInclude(f => f.Country).LoadAsync();
            
                    if (member.Member.ShareHoldingEntities.Count > 0)
                    {
                        foreach (var shareHoldingEntity in member.Member.ShareHoldingEntities)
                        {
                            var nameApplication = await _context.Applications.Include(a => a.NameSearch)
                                .ThenInclude(n => n.Names)
                                .SingleAsync(a =>
                                    a.ApplicationId.Equals(shareHoldingEntity.Entity.NameSearchApplicationId));
            
                            var entityShareHolder = new TaskPrivateEntityLocalEntityShareHoldersRequestDto
                            {
                                Name = nameApplication.NameSearch.Names.Single(n => n.Status.Equals(ENameStatus.Used))
                                    .Value,
                                Reference = shareHoldingEntity.Entity.Reference
                            };
                            entityShareHolder.Nominees.Add(
                                _mapper.Map<TaskPrivateEntityShareHolderResponseDto>(member.Member));
                            privateEntityApplication.PrivateEntity.LocalEntityShareHolders.Add(entityShareHolder);
                        }
                    }
                    else if (member.Member.RepresentedForeignEntities.Count > 0)
                    {
                        foreach (var foreignEntity in member.Member.RepresentedForeignEntities)
                        {
                            await _context.Entry(foreignEntity.ForeignEntity).Collection(f => f.Subscriptions)
                                .LoadAsync();
                            var foreignEntityDto =
                                _mapper.Map<TaskPrivateEntityForeignEntityShareHoldersDto>(foreignEntity.ForeignEntity);
                            foreignEntityDto.Nominees.Add(
                                _mapper.Map<TaskPrivateEntityShareHolderResponseDto>(member.Member));
                            privateEntityApplication.PrivateEntity.ForeignEntityShareHolders.Add(foreignEntityDto);
                        }
                    }
                    else
                    {
                        privateEntityApplication.PrivateEntity.Members.Add(
                            _mapper.Map<TaskPrivateEntityShareHolderResponseDto>(member.Member));
                    }
                }
            }
            
            return privateEntityApplication;

            // return await _context.PrivateEntities.Include(p => p.CurrentApplication)
            //     .SingleAsync(p => p.CurrentApplication.TaskId.Equals(taskId));
        }
    }
}