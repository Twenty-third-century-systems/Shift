using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Request;
using Cabinet.Dtos.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models;

namespace TurnTable.ExternalServices {
    public class NameSearchService : INameSearchService {
        private readonly IMapper _mapper;
        private MainDatabaseContext _context;

        public NameSearchService(IMapper mapper,MainDatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool NameAvailable(string suggestedName)
        {
            var entityNames = _context.Names.Where(n => n.Value.Equals(suggestedName)).ToList();
            return entityNames.Count == 0;
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
            var application = new Application
            {
                DateSubmitted = DateTime.Now,
                Status = EApplicationStatus.Submited,
                CityId = dto.SortingOffice
            };

            // Initialize a NameSearch
            var nameSearch = _mapper.Map<NewNameSearchRequestDto, NameSearch>(dto);
            // Create NameSearch Reference
            nameSearch.Reference = NewNameSearchReference(dto.SortingOffice);

            // Mark all names as PENDING
            foreach (var name in nameSearch.Names)
            {
                name.Status = ENameStatus.Pending;
            }

            // Associate NameSearch with ServiceApplication and mark for creation
            application.NameSearch = nameSearch;
            await _context.AddAsync(application);

            // Commit to db
            await _context.SaveChangesAsync();
            
            // Creation and return of resource
            // TODO: verify if SaveChangesSuccessful
            return new SubmittedNameSearchResponseDto
            {
                Id = application.ApplicationId,
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