using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace TurnTable.ExternalServices {
    public class ValueService : IValueService {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;
        private IPaymentService _paymentService;

        public ValueService(MainDatabaseContext context, IMapper mapper, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Gets Values to display on User Dashboard
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// ExternalUserDashboardRequestDto
        /// </returns>
        public async Task<ExternalUserDashboardRequestDto> GetUserDashBoardValuesAsync(Guid user)
        {
            ExternalUserDashboardRequestDto dto = new ExternalUserDashboardRequestDto();

            // User account balance
            dto.AccountBalance = await _paymentService.GetBalanceAsync(user);
            // All applications by user
            var applicationsByUser = await _mapper
                .ProjectTo<SubmittedApplicationSummaryResponseDto>(
                    _context.Applications
                        .Where(a => !a.Status.Equals(EApplicationStatus.Incomplete) && a.User == user))
                .ToListAsync();

            if (applicationsByUser.Count > 0)
            {
                // Submitted application count
                dto.SubmittedApplicationsCount = applicationsByUser.Count;

                //Account Use HttpService

                // Recent Activity
                dto.RecentActivity = applicationsByUser.Take(10);

                // Approved applications
                var approvedNameSearches = await _mapper
                    .ProjectTo<SubmittedApplicationSummaryResponseDto>(_context.Applications
                        .Include(a => a.NameSearch)
                        .ThenInclude(n => n.Names)
                        .Where(a => a.Service.Equals(EService.NameSearch))).ToListAsync();

                var approvedEntities = await _mapper
                    .ProjectTo<SubmittedApplicationSummaryResponseDto>(_context.Applications
                        .Include(a => a.PrivateEntity)
                        .Where(a => !a.DateExamined.Equals(null) && !string.IsNullOrEmpty(a.PrivateEntity.Reference)))
                    .ToListAsync();


                dto.ApprovedApplications = approvedNameSearches.Concat(approvedEntities);
                dto.ApprovedApplications = dto.ApprovedApplications.OrderBy(a => a.DateSubmitted);

                // Registered Entities count
                dto.RegisteredEntitiesCount = approvedEntities.Count;
            }

            return dto;
        }

        public async Task<List<SelectionValueResponseDto>> GetSortingOfficesAsync()
        {
            return await _mapper
                .ProjectTo<SelectionValueResponseDto>(_context.Cities.Where(c => c.CanSort))
                .ToListAsync();
        }
    }
}