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

namespace TurnTable.InternalServices.Task {
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
                        a.Status.Equals(EApplicationStatus.Submitted)))
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

        private async System.Threading.Tasks.Task AllocateMultipleApplicationsAsync(NewTaskAllocationRequestDto dto)
        {
            var dtoService = (EService) dto.Service;
            var applications = await _context.Applications.Where(a =>
                    a.Service == dtoService &&
                    a.CityId.Equals(dto.SortingOffice) &&
                    a.TaskId == null)
                .Take(dto.NumberOfApplications)
                .ToListAsync();

            var task = _mapper.Map<ExaminationTask>(dto);

            foreach (var application in applications)
            {
                if (application.Service == EService.NameSearch)
                {
                    application.ExaminationTask = task;
                    application.Status = EApplicationStatus.Assigned;
                }

                if (application.Service == EService.PrivateLimitedCompany)
                {
                    if (application.Status == EApplicationStatus.Submitted)
                    {
                        application.ExaminationTask = task;
                        application.Status = EApplicationStatus.Assigned;
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task AllocateSingleApplicationAsync(NewTaskAllocationRequestDto dto)
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
                    .Include(e => e.Applications)
                    .Where(e => e.Examiner.Equals(examiner) && e.Status == ETaskStatus.Incomplete))
                .ToListAsync();
        }

        public async Task<List<AllocatedNameSearchTaskApplicationResponseDto>> GetNameSearchTaskApplicationsAsync(
            int taskId)
        {
            return await _mapper.ProjectTo<AllocatedNameSearchTaskApplicationResponseDto>(_context.Applications
                    .Include(a => a.NameSearch).ThenInclude(n => n.Names).Where(a => a.TaskId.Equals(taskId)))
                .ToListAsync();
        }

        public async Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>>
            GetPrivateEntityTaskApplicationAsync(int taskId)
        {
            var privateEntities = await _context.PrivateEntities
                .Include(p => p.CurrentApplication)
                .Include(p => p.MemorandumOfAssociation)
                .ThenInclude(m => m.ShareClauses)
                .Include(p => p.MemorandumOfAssociation)
                .ThenInclude(m => m.MemorandumObjects)
                .Include(p => p.ArticlesOfAssociation)
                .ThenInclude(a => a.AmendedArticles)
                .Include(p => p.Directors)
                .ThenInclude(d => d.Country)
                .Include(p => p.Secretary)
                .ThenInclude(d => d.Country)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsPersons)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.PersonSubscriptions)
                .ThenInclude(s => s.ShareClause)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsForeignEntities)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.ForeignEntitySubscriptions)
                .ThenInclude(s => s.ShareClause)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsPrivateEntities)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.Name)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsPrivateEntities)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.PrivateEntitySubscriptions)
                .ThenInclude(s => s.ShareClause)
                .Where(p => p.CurrentApplication.TaskId == taskId)
                .ToListAsync();

            List<AllocatedPrivateEntityTaskApplicationResponseDto> privateEntityApplications =
                new List<AllocatedPrivateEntityTaskApplicationResponseDto>();

            foreach (var privateEntity in privateEntities)
            {
                var privateEntityApplication =
                    _mapper.Map<AllocatedPrivateEntityTaskApplicationResponseDto>(privateEntity);

                foreach (var nominee in privateEntity.PersonHoldsSharesInPrivateEntities)
                {
                    var nomineeDto = _mapper.Map<TaskPrivateEntityShareHolderResponseDto>(nominee.ShareHolder);
                    foreach (var beneficiary in nominee.ShareHolder.PersonRepresentsPersons)
                    {
                        var benefactor = _mapper.Map<TaskPrivateEntityShareHolderResponseDto>(beneficiary.Beneficiary);
                        foreach (var subscription in beneficiary.Beneficiary.PersonSubscriptions)
                        {
                            var sub = _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(subscription);
                            benefactor.Subscriptions.Add(sub);
                        }

                        nomineeDto.Beneficiaries.Add(benefactor);
                    }

                    foreach (var beneficiaryEntity in nominee.ShareHolder.PersonRepresentsPrivateEntities)
                    {
                        var entity = _mapper.Map<TaskShareHoldingEntityRequestDto>(beneficiaryEntity.Beneficiary);
                        foreach (var entitySubscription in beneficiaryEntity.Beneficiary.PrivateEntitySubscriptions)
                        {
                            var sub =
                                _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(entitySubscription);
                            entity.Subscriptions.Add(sub);
                        }

                        nomineeDto.RepresentedEntities.Add(entity);
                    }

                    foreach (var beneficiaryEntity in nominee.ShareHolder.PersonRepresentsForeignEntities)
                    {
                        var entity = _mapper.Map<TaskShareHoldingEntityRequestDto>(beneficiaryEntity.Beneficiary);
                        foreach (var entitySubscription in beneficiaryEntity.Beneficiary.ForeignEntitySubscriptions)
                        {
                            var sub =
                                _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(entitySubscription);
                            entity.Subscriptions.Add(sub);
                        }

                        nomineeDto.RepresentedEntities.Add(entity);
                    }

                    foreach (var subscription in nominee.ShareHolder.PersonSubscriptions)
                    {
                        var sub = _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(subscription);
                        nomineeDto.Subscriptions.Add(sub);
                    }

                    privateEntityApplication.Members.Add(nomineeDto);
                }

                privateEntityApplications.Add(privateEntityApplication);
            }

            return privateEntityApplications;
        }

        public async Task<int> FinishTaskAsync(int taskId)
        {
            var examinationTask = await _context.ExaminationTasks.Include(e => e.Applications)
                .SingleAsync(e => e.ExaminationTaskId.Equals(taskId));
            examinationTask.Status = ETaskStatus.Completed;
            var applications = examinationTask.Applications.Where(a => a.Status != EApplicationStatus.Examined)
                .ToList();
            if (applications.Count.Equals(0))
                return await _context.SaveChangesAsync();
            else throw new Exception("Some applications in this task haven't been examined.");
        }
    }
}