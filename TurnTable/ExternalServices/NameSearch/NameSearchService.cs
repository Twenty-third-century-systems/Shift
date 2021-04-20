using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models.Main;
using Microsoft.EntityFrameworkCore;
using TurnTable.ExternalServices.Payments;

namespace TurnTable.ExternalServices.NameSearch {
    public class NameSearchService : INameSearchService {
        private readonly IMapper _mapper;
        private readonly MainDatabaseContext _context;
        private readonly IPaymentsService _paymentsService;

        public NameSearchService(IMapper mapper, MainDatabaseContext context, IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
            _context = context;
            _mapper = mapper;
        }

        public bool NameIsAvailable(string suggestedName)
        {
            // TODO: test this logic
            var entityNames = _context.Names.Include(n => n.NameSearch)
                .Where(n =>
                    n.Value.Equals(suggestedName) && (n.Status.Equals(ENameStatus.Reserved) ||
                                                      n.Status.Equals(ENameStatus.Blacklisted) ||
                                                      n.Status.Equals(ENameStatus.Used))).ToList();
            entityNames = entityNames.Where(n => DateTime.Now - n.NameSearch.ExpiryDate <= TimeSpan.FromDays(0))
                .ToList();

            return entityNames.Count == 0;
        }

        public async Task<SubmittedNameSearchResponseDto> CreateNewNameSearchAsync(Guid user,
            NewNameSearchRequestDto dto)
        {
            var application =
                new Application(user, EService.NameSearch, EApplicationStatus.Submitted, dto.SortingOffice);
            var nameSearch = _mapper.Map<NewNameSearchRequestDto, Fridge.Models.Main.NameSearch>(dto);
            application.NameSearch = nameSearch;
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {                
                await _context.AddAsync(application);
                await _context.SaveChangesAsync();
                application.NameSearch.Reference = $"NS/{nameSearch.NameSearchId}";
                
                Guid payment;
                try
                {
                    payment = await _paymentsService.BillAsync(EService.NameSearch, user, nameSearch.Reference);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                application.Payment = payment;
                await _context.SaveChangesAsync();                
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }

            // TODO: verify if SaveChangesSuccessful
            return new SubmittedNameSearchResponseDto
            {
                Id = application.ApplicationId,
                NameSearch = nameSearch.NameSearchId,
                Reference = nameSearch.Reference
            };
        }

        public async Task<int> TestApproveNameSearch(Guid user, int applicationId)
        {
            var application = await _context.Applications.Include(a => a.NameSearch).ThenInclude(n => n.Names)
                .SingleAsync(a =>
                    a.User.Equals(user) && a.ApplicationId.Equals(applicationId));

            bool reserved = false;
            foreach (var name in application.NameSearch.Names)
            {
                if (!reserved)
                {
                    name.Status = ENameStatus.Reserved;
                    reserved = true;
                }
                else name.Status = ENameStatus.Rejected;
            }

            application.DateExamined = DateTime.Now;
            application.NameSearch.ExpiryDate = DateTime.Now.AddDays(30);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> FurtherReserveUnexpiredNameAsync(string reference)
        {
            var nameSearch = await _context.NameSearches.Include(n => n.Names)
                .SingleAsync(n => n.Reference.Equals(reference));
            if (DateTime.Now - nameSearch.ExpiryDate <= TimeSpan.FromDays(0))
            {
                nameSearch.ExpiryDate = DateTime.Now.AddDays(30);
                return await _context.SaveChangesAsync();
                // TODO: send email and/or sms
            }

            throw new Exception("The name is no longer available for further reservation.");
        }
    }
}