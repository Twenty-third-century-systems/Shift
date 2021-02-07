using System;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Request;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Fridge.Models;
using Fridge.Repository;

namespace TurnTable.ExternalServices {
    public class NameSearchService : INameSearchService {
        private readonly NameSearchRepository _nameSearchRepository;
        private readonly ApplicationStatusRepository _applicationStatusRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ServiceApplicationRepository _serviceApplicationRepository;
        private MainDatabaseContext _context;

        public NameSearchService(ServiceApplicationRepository serviceApplicationRepository,
            NameSearchRepository nameSearchRepository,
            ApplicationStatusRepository applicationStatusRepository, ServiceTypeRepository serviceTypeRepository,
            IMapper mapper,MainDatabaseContext context)
        {
            _context = context;
            _serviceApplicationRepository = serviceApplicationRepository;
            _mapper = mapper;
            _serviceTypeRepository = serviceTypeRepository;
            _applicationStatusRepository = applicationStatusRepository;
            _nameSearchRepository = nameSearchRepository;
        }

        public async Task<bool> NameAvailable(string suggestedName)
        {
            return await _nameSearchRepository.IsNameAvailable(suggestedName);
        }
        
        /// <summary>
        /// 
        /// Submits a name search
        /// Creates a ServiceApplicationObject and Chain everything in there
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns>
        /// SubmittedNameSearchResponseDto
        /// </returns>

        public async Task<SubmittedNameSearchResponseDto> CreateNewNameSearch(Guid userId, NewNameSearchRequestDto dto)
        {
            // Initialize a new ServiceApplication
            var serviceApplication = new ServiceApplication
            {
                ServiceType = await _serviceTypeRepository.GetNameSearchServiceAsync(),
                DateSubmitted = DateTime.Now,
                ApplicationStatus = await _applicationStatusRepository.GetSubmittedStatusAsync(),
                CityId = dto.SortingOffice
            };

            // Initialize a NameSearch
            var nameSearch = _mapper.Map<NewNameSearchRequestDto, NameSearch>(dto);
            // Create NameSearch Reference
            nameSearch.Reference = NewNameSearchReference(dto.SortingOffice);

            // Mark all names as PENDING
            var pendingStatus = await _applicationStatusRepository.GetPendingStatusAsync();
            foreach (var name in nameSearch.EntityNames)
            {
                name.ApplicationStatus = pendingStatus;
            }

            // Associate NameSearch with ServiceApplication and mark for creation
            serviceApplication.NameSearches.Add(nameSearch);
            await _serviceApplicationRepository.AddApplicationAsync(serviceApplication);

            // Commit to db
            await _context.SaveChangesAsync();
            
            // Creation and return of resource
            return new SubmittedNameSearchResponseDto
            {
                Id = serviceApplication.ServiceApplicationId,
                NameSearch = nameSearch.NameSearchId,
                Reference = nameSearch.Reference
            };
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