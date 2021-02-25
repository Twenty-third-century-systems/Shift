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
    public class PrivateEntityService : IPrivateEntityService {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public PrivateEntityService(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<RegisteredNameResponseDto>> GetRegisteredNamesAsync(Guid user)
        {
            var registeredNameResponseDtos = await _mapper.ProjectTo<RegisteredNameResponseDto>(_context.Names
                .Include(n => n.NameSearch)
                .ThenInclude(ns => ns.Application).Where(n =>
                    n.Status.Equals(ENameStatus.Reserved) &&
                    !n.NameSearch.ExpiryDate.Equals(null) &&
                    n.NameSearch.ReasonForSearch.Equals(EReasonForSearch.NewRegistration) &&
                    n.NameSearch.Application.User.Equals(user))).ToListAsync();
            return registeredNameResponseDtos;
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
                new Application(user, EService.PrivateLimitedCompany, EApplicationStatus.Incomplete,
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

        public async Task<ApplicationResponseDto> InsertOfficeAsync(Guid user, NewPrivateEntityOfficeRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);

            application.PrivateEntity.Office = _mapper.Map<NewPrivateEntityOfficeRequestDto, Office>(dto);
            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertMemorandumOfAssociationAsync(Guid user,
            NewMemorandumRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);
            application.PrivateEntity.MemorandumOfAssociation =
                _mapper.Map<NewMemorandumRequestDto, MemorandumOfAssociation>(dto);

            await _context.SaveChangesAsync();

            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertMemorandumObjectsAsync(Guid user,
            NewMemorandumOfAssociationObjectsRequestDto dto)
        {
            // Load Application from db
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);

            // Load Memo
            await LoadSavedMemorandumAsync(application);

            // Add Objects
            application.PrivateEntity.MemorandumOfAssociation.MemorandumObjects = _mapper
                .Map<List<NewMemorandumOfAssociationObjectRequestDto>, List<MemorandumOfAssociationObject>
                >(dto.Objects);

            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertArticlesOfAssociationAsync(Guid user,
            NewArticleOfAssociationRequestDto dto)
        {
            if (!dto.TableOfArticles.Equals(null) && dto.AmendedArticles.Count > 0)
                throw new Exception("You must use the set tables of Articles or a custom table, not both.");

            // Getting Application from database 
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);

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

        public async Task<ApplicationResponseDto> InsertShareClauseAsync(Guid user, NewShareClausesRequestDto dto)
        {
            // Get application from database
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);

            await LoadSavedMemorandumAsync(application);

            // Add ShareClause(s)
            application.PrivateEntity.MemorandumOfAssociation.ShareClauses =
                _mapper.Map<List<NewShareClauseRequestDto>, List<ShareClause>>(dto.Clauses);

            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };
        }

        public async Task<ApplicationResponseDto> InsertMembersAsync(Guid user, NewShareHoldersRequestDto dto)
        {
            // Get application from db
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);
            await LoadSavedMemorandumAsync(application);
            await _context.Entry(application.PrivateEntity.MemorandumOfAssociation)
                .Collection(m => m.ShareClauses)
                .LoadAsync();

            // Get share clause
            var shareClauses = application.PrivateEntity.MemorandumOfAssociation.ShareClauses;

            // Map shareholding pple
            foreach (var person in dto.People)
            {
                var privateEntityOwner =
                    MapPrivateEntityOwner(person, shareClauses);
                if (person.HasNominees())
                    foreach (var nominee in person.Nominees)
                    {
                        privateEntityOwner.Nominees.Add(new PrivateEntityOwnerHasPrivateEntityOwner(privateEntityOwner,
                            MapPrivateEntityOwner(nominee, shareClauses)));
                    }

                application.PrivateEntity.Members.Add(
                    new PrivateEntityHasPrivateEntityOwner(application.PrivateEntity, privateEntityOwner));
            }

            // Map shareholding Entities
            foreach (var entity in dto.Entities)
            {
                if (entity.IsRepresented())
                {
                    if (entity.CountryCode.Equals("ZWE"))
                    {
                        // TODO: add name to search Query add the adding of share clause
                        var registeredEntity =
                            await _context.PrivateEntities
                                .SingleAsync(p => p.Reference.Equals(entity.CompanyReference));
                        if (!registeredEntity.Equals(null))
                        {
                            registeredEntity.OwnedEntities.Add(
                                new PrivateEntityHasPrivateEntity(application.PrivateEntity, registeredEntity));
                        }
                        else throw new Exception("One of the local shareholding entities is not registered.");
                    }
                    else
                    {
                        var shareholdingForeignEntity =
                            _mapper.Map<NewShareHoldingEntityRequestDto, ShareholdingForeignEntity>(entity);

                        foreach (var subscription in entity.Subs)
                        {
                            var shareClause = shareClauses.Single(s => s.Title.Equals(subscription.Title));
                            shareholdingForeignEntity.Subscriptions.Add(
                                new ShareHoldingForeignEntityHasShareClause(shareholdingForeignEntity, shareClause,
                                    subscription.Amount));
                        }

                        foreach (var nominee in entity.Nominees)
                        {
                            shareholdingForeignEntity.Representatives.Add(
                                new ShareHoldingForeignEntityHasPrivateEntityOwner(shareholdingForeignEntity,
                                    MapPrivateEntityOwner(nominee, shareClauses)));
                        }
                        await _context.AddAsync(shareholdingForeignEntity);

                        foreach (var representative in shareholdingForeignEntity.Representatives)
                        {
                            representative.Nominee.ShareHoldingEntities.Add(new PrivateEntityHasPrivateEntityOwner(application.PrivateEntity,representative.Nominee));
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                else throw new Exception("One of the shareholding entities is not represented.");
            }

            await _context.SaveChangesAsync();
            return new ApplicationResponseDto
            {
                Id = application.ApplicationId,
                Service = application.Service.ToString()
            };            
        }

        public async Task<int> SubmitApplicationAsync(Guid user, int applicationId)
        {
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);
            application.Status = EApplicationStatus.Submited;
            return await _context.SaveChangesAsync();
        }

        public async Task<Application> GetPrivateEntityApplicationAsync(Guid user, int applicationId)
        {
            return await _context.Applications
                .Include(a => a.PrivateEntity)
                .SingleAsync(a =>
                    a.ApplicationId.Equals(applicationId) && a.User.CompareTo(user).Equals(0));
        }

        public async Task LoadSavedMemorandumAsync(Application application)
        {
            // Load the MemorandumOfAssociation
            await _context.Entry(application.PrivateEntity)
                .Reference(p => p.MemorandumOfAssociation).LoadAsync();
        }

        private PrivateEntityOwner MapPrivateEntityOwner(NewShareHolderRequestDto dto,
            ICollection<ShareClause> shareClauses)
        {
            var privateEntityOwner = _mapper.Map<NewShareHolderRequestDto, PrivateEntityOwner>(dto);
            foreach (var subscription in dto.Subs)
            {
                var shareClause = shareClauses.Single(s => s.Title.Equals(subscription.Title));
                privateEntityOwner.Subscriptions.Add(
                    new PrivateEntityOwnerHasShareClause(privateEntityOwner, shareClause, subscription.Amount));
            }

            return privateEntityOwner;
        }
    }
}