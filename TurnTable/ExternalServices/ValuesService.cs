using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Fridge.Models;
using Fridge.Repository;

namespace TurnTable.ExternalServices {
    public class ValuesService : IValuesService {
        private ServiceApplicationRepository _applicationRepository;
        private PrivateEntityRepository _privateEntityRepository;
        private NameSearchRepository _nameSearchRepository;
        private ReasonForSearchRepository _reasonForSearchRepository;
        private ServiceTypeRepository _serviceTypeRepository;
        private DesignationRepository _designationRepository;
        private CityRepository _cityRepository;

        public ValuesService(ServiceApplicationRepository applicationRepository,
            PrivateEntityRepository privateEntityRepository,
            NameSearchRepository nameSearchRepository, ReasonForSearchRepository reasonForSearchRepository,
            ServiceTypeRepository serviceTypeRepository, DesignationRepository designationRepository, CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
            _designationRepository = designationRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _reasonForSearchRepository = reasonForSearchRepository;
            _nameSearchRepository = nameSearchRepository;
            _privateEntityRepository = privateEntityRepository;
            _applicationRepository = applicationRepository;
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
            //Getting Values from Db
            var serviceApplications =
                await _applicationRepository.GetAllApplicationsByUserAsync(userId);
            var registeredEntities =
                await _privateEntityRepository.GetRegisteredEntitiesAsync(userId);
            var approvedNameSearches =
                await _nameSearchRepository.GetApprovedNameSearchesAsync(userId);

            //Constructing and returning resource
            return new ExternalUserDashboardRequestDto
            {
                SubmittedApplicationsCount = serviceApplications.Count,
                RegisteredEntitiesCount = registeredEntities.Count,
                RecentActivity = serviceApplications.Take(10),
                ApprovedApplications = registeredEntities.Concat(approvedNameSearches)
            };
        }

        /// <summary>
        /// Gathers values for selection on
        /// the ns form
        /// </summary>
        /// <returns></returns>
        public NameSearchSelectionValuesResponseDto GetNameSearchValuesForSelection()
        {
            return new NameSearchSelectionValuesResponseDto
            {
                ReasonsForSearch = _reasonForSearchRepository.GetReasonsForSearchForSelection(),
                TypesOfEntities = _serviceTypeRepository.GetServiceTypesForSearchForSelection(),
                Designations = _designationRepository.GetDesignationsForSelection(),
                SortingOffices = _cityRepository.GetSortingOfficesForSelection()
            };
        }
    }
}