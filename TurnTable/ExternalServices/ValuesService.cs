using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace TurnTable.ExternalServices {
    public class ValuesService : IValuesService {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public ValuesService(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Gets Values to display on User Dashboard
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// ExternalUserDashboardRequestDto
        /// </returns>
        public async Task<ExternalUserDashboardRequestDto> GetUserDashBoardValuesAsync(Guid userId)
        {
            ExternalUserDashboardRequestDto dto = new ExternalUserDashboardRequestDto();
            // All applications by user
            var applicationsByUser = await _mapper
                .ProjectTo<SubmittedApplicationResponseDto>(
                    _context.Applications
                        .Where(a => a.IsInACountableState() && a.ByUser(userId))).ToListAsync();

            if (applicationsByUser.Count > 0)
            {
                // Submitted application count
                dto.SubmittedApplicationsCount = applicationsByUser.Count;

                //Account Use HttpService

                // Recent Activity
                dto.RecentActivity = applicationsByUser.Take(10);                
                
                // Approved applications
                var approvedNameSearches = await _mapper
                    .ProjectTo<SubmittedApplicationResponseDto>(_context.Applications
                        .Include(a => a.NameSearch)
                        .ThenInclude(n => n.Names)
                        .Where(a => a.IsANameSearchApplication())).ToListAsync();

                var approvedEntities = await _mapper
                    .ProjectTo<SubmittedApplicationResponseDto>(_context.Applications
                        .Include(a => a.PrivateEntity)
                        .Where(a => a.PrivateEntity.WasExaminedAndApproved())).ToListAsync();
                
                
                dto.ApprovedApplications = approvedNameSearches.Concat(approvedEntities);
                dto.ApprovedApplications = dto.ApprovedApplications.OrderBy(a => a.DateSubmitted);

                // Registered Entities count
                dto.RegisteredEntitiesCount = approvedEntities.Count;
            }

            return dto;
        }

        public async Task<List<SelectionValueResponseDto>> GetSortingOffices()
        {
            return await _mapper
                .ProjectTo<SelectionValueResponseDto>(_context.Cities.Where(c => c.CanSort))
                .ToListAsync();
        }
    }
}