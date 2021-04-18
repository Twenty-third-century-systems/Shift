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

namespace TurnTable.ExternalServices.PrivateEntity {
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
            // TODO: create a dto including the IndustrySector
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
            var privateEntity = new Fridge.Models.PrivateEntity(name);
            privateEntity.MemorandumOfAssociation = new MemorandumOfAssociation();
            application.PrivateEntity = privateEntity;


            bool resubmission = false;
            // Mark name as used
            if (name.Status == ENameStatus.Used)
            {
                // await _context.Entry(name).Collection(n => n.PrivateEntities).Query().Include(p => p.CurrentApplication)
                //     .LoadAsync();
                // if (name.PrivateEntities.Count > 0)
                // {
                //     var entity = name.PrivateEntities.First();
                // }
                resubmission = true;
            }
            else
            {
                name.Status = ENameStatus.Used;
            }


            // Mark application for insertion
            await _context.AddAsync(application);

            return await ReturnApplicationResponse(application);
        }

        public async Task<ApplicationResponseDto> InsertOfficeAsync(Guid user, NewPrivateEntityOfficeRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);

            application.PrivateEntity.Office = _mapper.Map<Office>(dto);
            application.PrivateEntity.IndustrySector = dto.IndustrySector;
            return await ReturnApplicationResponse(application);
        }

        // TODO: change from dto to just a string
        public async Task<ApplicationResponseDto> InsertLiabilityClauseAsync(Guid user,
            NewLiabilityClauseRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);
            await LoadSavedMemorandumAsync(application);
            application.PrivateEntity.MemorandumOfAssociation ??= new MemorandumOfAssociation();
            application.PrivateEntity.MemorandumOfAssociation.LiabilityClause = dto.LiabilityClause;

            return await ReturnApplicationResponse(application);
        }

        public async Task<ApplicationResponseDto> InsertMemorandumObjectsAsync(Guid user,
            NewMemorandumOfAssociationObjectsRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);
            await LoadSavedMemorandumAsync(application);
            application.PrivateEntity.MemorandumOfAssociation ??= new MemorandumOfAssociation();
            application.PrivateEntity.MemorandumOfAssociation.MemorandumObjects = _mapper
                .Map<List<NewMemorandumOfAssociationObjectRequestDto>, List<MemorandumOfAssociationObject>
                >(dto.Objects);
            return await ReturnApplicationResponse(application);
        }

        public async Task<ApplicationResponseDto> InsertArticlesOfAssociationAsync(Guid user,
            NewArticleOfAssociationRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);
            application.PrivateEntity.ArticlesOfAssociation =
                _mapper.Map<NewArticleOfAssociationRequestDto, ArticlesOfAssociation>(dto);
            return await ReturnApplicationResponse(application);
        }

        public async Task<ApplicationResponseDto> InsertAmendedArticles(Guid user, NewAmendedArticlesRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);
            application.PrivateEntity.ArticlesOfAssociation ??= new ArticlesOfAssociation();
            application.PrivateEntity.ArticlesOfAssociation.AmendedArticles =
                _mapper.Map<List<AmendedArticle>>(dto.AmendedArticles);
            return await ReturnApplicationResponse(application);
        }

        public async Task<ApplicationResponseDto> InsertDirectors(Guid user, NewDirectorsRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user,dto.ApplicationId);
            application.PrivateEntity.Directors = _mapper.Map<List<Director>>(dto.Directors);
            return await ReturnApplicationResponse(application);
        }

        public async Task<ApplicationResponseDto> InsertSecretary(Guid user, NewSecretaryRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user,dto.ApplicationId);
            application.PrivateEntity.Secretary = _mapper.Map<Secretary>(dto.Secretary);
            return await ReturnApplicationResponse(application);
        }


        public async Task<ApplicationResponseDto> InsertShareClauseAsync(Guid user, NewShareClausesRequestDto dto)
        {
            var application = await GetPrivateEntityApplicationAsync(user, dto.ApplicationId);
            await LoadSavedMemorandumAsync(application);
            application.PrivateEntity.MemorandumOfAssociation.ShareClauses =
                _mapper.Map<List<ShareClause>>(dto.Clauses);
            return await ReturnApplicationResponse(application);
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
            foreach (var personDto in dto.People)
            {
                var person = MapPerson(personDto, shareClauses);
                await _context.AddAsync(person);
                if (personDto.HasBeneficiaries())
                    foreach (var benefactor in personDto.PeopleRepresented)
                    {
                        var beneficiary = MapPerson(benefactor, shareClauses);
                        await _context.AddAsync(beneficiary);
                        person.PersonRepresentsPersonss.Add(new PersonRepresentsPerson(person,
                            beneficiary));
                    }                
                AddPrivateEntityMember(application, person);
            }

            // Map shareholding Entities
            foreach (var entity in dto.Entities)
            {
                if (entity.IsRepresented())
                {
                    if (entity.CountryCode.Equals("ZWE"))
                    {
                        var registeredEntity = await _context.PrivateEntities
                            .Include(p => p.Name)
                            .ThenInclude(a => a.NameSearch)
                            .ThenInclude(n => n.Names)
                            .SingleOrDefaultAsync(p =>
                                p.Name.NameSearch.Names
                                    .Single(n => n.Status.Equals(ENameStatus.Used)).Value == entity.Name &&
                                p.Reference.Equals(entity.CompanyReference));

                        if (!registeredEntity.Equals(null))
                        {
                            foreach (var nominee in entity.Nominees)
                            {
                                var person = MapPerson(nominee, shareClauses);
                                AddPrivateEntityMember(application, person);

                                registeredEntity.PersonRepresentsPrivateEntity.Add(
                                    new PersonRepresentsPrivateEntity(registeredEntity, person));
                            }
                        }
                        else throw new Exception("One of the local shareholding entities is not registered.");
                    }
                    else
                    {
                        var shareholdingForeignEntity = _mapper.Map<ForeignEntity>(entity);

                        foreach (var subscription in entity.Subs)
                        {
                            shareholdingForeignEntity.ForeignEntitySubscriptions.Add(
                                new ForeignEntitySubscription(shareholdingForeignEntity,
                                    shareClauses.Single(s => s.Title.Equals(subscription.Title)), subscription.Amount));
                        }

                        foreach (var nominee in entity.Nominees)
                        {
                            var person = MapPerson(nominee, shareClauses);
                            shareholdingForeignEntity.PersonRepresentsForeignEntities.Add(
                                new PersonRepresentsForeignEntity(shareholdingForeignEntity, person));
                            await _context.AddAsync(shareholdingForeignEntity);
                            AddPrivateEntityMember(application, person);
                        }

                        // await _context.SaveChangesAsync();
                    }
                }
                else throw new Exception("One of the shareholding entities is not represented.");
            }
            return await ReturnApplicationResponse(application);
        }

        public async Task<int> FinishApplicationAsync(Guid user, int applicationId)
        {
            var application = await GetPrivateEntityApplicationAsync(user, applicationId);
            application.PrivateEntity.Reference = "PVT-LTD: " + application.PrivateEntity.PrivateEntityId;
            //
            
            application.Status = EApplicationStatus.Submitted;
            return await _context.SaveChangesAsync();
        }

        public async Task<ApplicationResponseDto> ResubmitApplicationAsync(Guid user, int applicationId)
        {
            var previousApplication = await _context.Applications
                .Include(a => a.PrivateEntity)
                .AsNoTracking()
                .SingleAsync(a => a.ApplicationId.Equals(applicationId));
            if (previousApplication.Status == EApplicationStatus.Examined &&
                previousApplication.RaisedQueries.Count > 0)
            {
                var newApplication = await CreateApplicationAsync(user, previousApplication.PrivateEntity.NameId);
                var createdApplication = await _context.Applications.FindAsync(newApplication.Id);
                createdApplication.PrivateEntity.LastApplicationId = previousApplication.ApplicationId;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return newApplication;
                }
            }
            else throw new Exception("This application does not qualify for resubmission.");

            return null;
        }
        
        // Helper methods

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

        private ShareHolder MapPerson(NewShareHolderRequestDto dto,ICollection<ShareClause> shareClauses)
        {
            var person = _mapper.Map<ShareHolder>(dto);
            foreach (var subscription in dto.Subs)
            {
                person.PersonSubscriptions.Add(
                    new PersonSubscription(person, shareClauses.Single(s => s.Title.Equals(subscription.Title)),
                        subscription.Amount));
            }

            return person;
        }        

        private static void AddPrivateEntityMember(Application application, ShareHolder shareHolder)
        {
            application.PrivateEntity.PersonHoldsSharesInPrivateEntities.Add(
                new PersonHoldsSharesInPrivateEntity(application.PrivateEntity, shareHolder));
        }

        private async Task<ApplicationResponseDto> ReturnApplicationResponse(Application application)
        {
            if (await _context.SaveChangesAsync() > 0)
                return new ApplicationResponseDto
                {
                    Id = application.ApplicationId,
                    Service = application.Service.ToString()
                };
            return null;
        }
    }
}