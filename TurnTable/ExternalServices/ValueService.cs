﻿using System;
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
                        .Include(a => a.PrivateEntity)
                        .Where(a => a.Status != EApplicationStatus.Incomplete && a.User == user)
                        .OrderBy(a => a.DateSubmitted)
                        .Take(10))
                .ToListAsync();

            if (applicationsByUser.Count > 0)
            {
                // Submitted application count
                dto.SubmittedApplicationsCount = applicationsByUser.Count;

                //Account Use HttpService

                // Recent Activity
                dto.RecentActivity = applicationsByUser.Take(10);

                // Approved applications
                // var examinedNameSearches = await _mapper
                //     .ProjectTo<SubmittedApplicationSummaryResponseDto>(_context.Applications
                //         .Include(a => a.NameSearch)
                //         .ThenInclude(n => n.Names)
                //         .Where(a => a.Service.Equals(EService.NameSearch) && a.Status == EApplicationStatus.Examined)).ToListAsync();

                var examinedNameSearches = await _context.Applications
                    .Include(a => a.NameSearch)
                    .ThenInclude(n => n.Names)
                    .Where(a => a.Service.Equals(EService.NameSearch) && a.Status == EApplicationStatus.Examined)
                    .ToListAsync();

                var approvedNamesSearches = new List<SubmittedApplicationSummaryResponseDto>();
                foreach (var examinedNameSearch in examinedNameSearches)
                {
                    try
                    {
                        var approvedName =
                            examinedNameSearch.NameSearch.Names.Single(n =>
                                n.Status == ENameStatus.Reserved || n.Status == ENameStatus.Used);
                        if (approvedName != null)
                            approvedNamesSearches.Add(
                                _mapper.Map<SubmittedApplicationSummaryResponseDto>(examinedNameSearch));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        // throw;
                    }
                }

                var approvedEntities = await _mapper
                    .ProjectTo<SubmittedApplicationSummaryResponseDto>(_context.Applications
                        .Include(a => a.PrivateEntity)
                        .Where(a => a.Service == EService.PrivateLimitedCompany &&
                                    a.Status == EApplicationStatus.Approved))
                    .ToListAsync();


                dto.ApprovedApplications = approvedNamesSearches.Concat(approvedEntities);
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