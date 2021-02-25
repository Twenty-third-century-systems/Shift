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
    public class ApplicationService : IApplicationService {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public ApplicationService(MainDatabaseContext context, IMapper mapper)
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
                await AllocateMultipleApplications(dto);
            }

            await _context.SaveChangesAsync();
            return await _context.Applications.CountAsync(a =>
                a.Service.Equals(dto.Service) && a.CityId.Equals(dto.SortingOffice) && a.TaskId.Equals(null));
        }

        private async Task AllocateMultipleApplications(NewTaskAllocationRequestDto dto)
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
    }
}