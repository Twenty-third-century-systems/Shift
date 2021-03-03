using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace TurnTable.ExternalServices {
    public class NameSearchService : INameSearchService {
        private readonly IMapper _mapper;
        private MainDatabaseContext _context;
        private IPaymentService _paymentService;
        private INameSearchMutualExclusionService _nameSearchMutualExclusionService;

        public NameSearchService(IMapper mapper, MainDatabaseContext context, IPaymentService paymentService,
            INameSearchMutualExclusionService nameSearchMutualExclusionService)
        {
            _nameSearchMutualExclusionService = nameSearchMutualExclusionService;
            _paymentService = paymentService;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Checks if suggested name can be applied for
        /// </summary>
        /// <param name="suggestedName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Submits a name search
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dto"></param>
        /// <returns>
        /// SubmittedNameSearchResponseDto
        /// </returns>
        public async Task<SubmittedNameSearchResponseDto> CreateNewNameSearchAsync(Guid user,
            NewNameSearchRequestDto dto)
        {
            // Initialize a new Application
            var application =
                new Application(user, EService.NameSearch, EApplicationStatus.Submited, dto.SortingOffice);

            // Initialize a NameSearch fro mapper
            var nameSearch = _mapper.Map<NewNameSearchRequestDto, NameSearch>(dto);

            // Create NameSearch Reference
            nameSearch.Reference = NewNameSearchReference(dto.SortingOffice);

            // Associate NameSearch with Application
            application.NameSearch = nameSearch;
            var transaction = await _context.Database.BeginTransactionAsync();
            // _nameSearchMutualExclusionService.Lock();
            try
            {
                // BillAsync for service and commit
                // await _paymentService.BillAsync(EService.NameSearch, user, nameSearch.Reference);

                // Mark for insertion and commit
                await _context.AddAsync(application);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                // _nameSearchMutualExclusionService.UnLock();
            }


            // Creation and return of resource
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

        public async Task FurtherReserveName(string reference)
        {
            var nameSearch = await _context.NameSearches.Include(n => n.Names)
                .SingleAsync(n => n.Reference.Equals(reference));
            if (DateTime.Now - nameSearch.ExpiryDate > TimeSpan.FromDays(0))
            {
                // TODO: check for funds here if not enough raise exception
                var name = nameSearch.Names.SingleOrDefault(n => n.Status.Equals(ENameStatus.Reserved));
                if (name != null && NameIsAvailable(name.Value))
                {
                    nameSearch.ExpiryDate = DateTime.Now.AddDays(30);
                    await _context.SaveChangesAsync();
                    // TODO: send email and/or sms
                }
                else throw new Exception("The name is no longer available.");
            }
            else throw new Exception("The name has not expired yet");
        }

        /// <summary>
        /// Creates a new NameSearch.Reference
        /// By joining few info together
        /// </summary>
        /// <param name="sortingOffice"></param>
        /// <returns>string</returns>
        private static string NewNameSearchReference(int sortingOffice)
        {
            return "NS/" + DateTime.Now.Year.ToString() + sortingOffice.ToString() + "/" +
                   Guid.NewGuid().ToString();
        }
    }
}