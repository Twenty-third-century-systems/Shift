using System;
using System.Collections.Generic;
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
    public class PrivateEntityService {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public PrivateEntityService(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        private async Task<Application> GetPrivateEntityApplicationAsync(Guid user, int applicationId)
        {
            return await _context.Applications
                .Include(a => a.PrivateEntity)
                .SingleAsync(a =>
                    a.ApplicationId.Equals(applicationId) && a.User.CompareTo(user).Equals(0));
        }

        private async Task LoadSavedMemorandumAsync(Application application)
        {
            // Load the MemorandumOfAssociation
            await _context.Entry(application.PrivateEntity)
                .Reference(p => p.MemorandumOfAssociation).LoadAsync();
        }

        public async Task<List<RegisteredNameResponseDto>> GetRegisteredNamesAsync(Guid user)
        {
            return await _mapper.ProjectTo<RegisteredNameResponseDto>(_context.Names.Include(n => n.NameSearch)
                .ThenInclude(ns => ns.Application).Where(n =>
                    n.Status.Equals(ENameStatus.Reserved) &&
                    !n.NameSearch.ExpiryDate.Equals(null) &&
                    n.NameSearch.ReasonForSearch.Equals(EReasonForSearch.NewRegistration) &&
                    n.NameSearch.Application.User.CompareTo(user).Equals(0))).ToListAsync();
        }

        public async Task<ApplicationResponseDto> CreateApplicationAsync(Guid user, int nameId)
        {
            // Getting the reserved name
            var name = await _context.Names
                .Include(n => n.NameSearch)
                .ThenInclude(n => n.Application)
                .ThenInclude(a => a.SortingOffice)
                .SingleAsync(n => n.EntityNameId.Equals(nameId));

            // Constructing a new Application object
            var application =
                new Application(user, (int) EService.PrivateLimitedCompany, EApplicationStatus.Incomplete,
                    name.NameSearch.Application.SortingOffice.CityId);

            // Constructing a new Private entity and associating with the application
            var privateEntity = new PrivateEntity(name.NameSearch.Application);
            application.PrivateEntity = privateEntity;

            // Mark name as used
            name.Status = ENameStatus.Used;

            // Mark application for insertion
            await _context.AddAsync(application);

            // Commit to database
            await _context.SaveChangesAsync();

            // Construct and return resource
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertOfficeAsync(Guid user, int applicationId,
            NewPrivateEntityOfficeRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);

            application.PrivateEntity.Office = _mapper.Map<NewPrivateEntityOfficeRequestDto, Office>(dto);
            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertArticlesOfAssociationAsync(Guid user, int applicationId,
            NewArticleOfAssociationRequestDto dto)
        {
            // Getting Application from database 
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);

            // Adding ArticlesOfAssociation
            application.PrivateEntity.ArticlesOfAssociation =
                _mapper.Map<NewArticleOfAssociationRequestDto, ArticlesOfAssociation>(dto);

            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertMemorandumOfAssociationAsync(Guid user, int applicationId,
            NewMemorandumRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);
            application.PrivateEntity.MemorandumOfAssociation =
                _mapper.Map<NewMemorandumRequestDto, MemorandumOfAssociation>(dto);

            await _context.SaveChangesAsync();

            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertShareClauseAsync(Guid user, int applicationId,
            List<NewShareClauseRequestDto> dtos)
        {
            // Get application from database
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);

            await LoadSavedMemorandumAsync(application);

            // Add ShareClause(s)
            application.PrivateEntity.MemorandumOfAssociation.ShareClauses =
                _mapper.Map<List<NewShareClauseRequestDto>, List<ShareClause>>(dtos);

            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertMemorandumObjectsAsync(Guid user, int applicationId,
            List<NewMemorandumOfAssociationObjectRequestDto> dtos)
        {
            // Load Application from db
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);

            // Load Memo
            await LoadSavedMemorandumAsync(application);

            // Add Objects
            application.PrivateEntity.MemorandumOfAssociation.MemorandumObjects = _mapper
                .Map<List<NewMemorandumOfAssociationObjectRequestDto>, List<MemorandumOfAssociationObject>>(dtos);

            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task InsertMembers(Guid user, int applicationId, List<NewShareHolderRequestDto> dtos)
        {
            // Get application from db
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);
            await LoadSavedMemorandumAsync(application);
            await _context.Entry(application.PrivateEntity.MemorandumOfAssociation)
                .Reference(m => m.ShareClauses)
                .LoadAsync();

            
            
        }
    }
}