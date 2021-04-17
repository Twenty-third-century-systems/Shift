using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Internal.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TurnTable.InternalServices {
    public class ApplicationsService : IApplicationsService {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public ApplicationsService(MainDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AllocatedPrivateEntityTaskApplicationResponseDto>> GetApplicationsPendingApproval(
            int sortingOffice)
        {
            var privateEntities = await _context.PrivateEntities
                .Include(p => p.CurrentApplication)
                .ThenInclude(a => a.SortingOffice)
                .Include(p => p.CurrentApplication)
                .ThenInclude(a => a.RaisedQueries)
                .Include(p => p.MemorandumOfAssociation)
                .ThenInclude(m => m.ShareClauses)
                .Include(p => p.MemorandumOfAssociation)
                .ThenInclude(m => m.MemorandumObjects)
                .Include(p => p.ArticlesOfAssociation)
                .ThenInclude(a => a.AmendedArticles)
                .Include(p => p.Directors)
                .ThenInclude(d => d.Country)
                .Include(p => p.Secretary)
                .ThenInclude(d => d.Country)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsPersons)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.PersonSubscriptions)
                .ThenInclude(s => s.ShareClause)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsForeignEntities)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.ForeignEntitySubscriptions)
                .ThenInclude(s => s.ShareClause)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsPrivateEntities)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.Name)
                .Include(p => p.PersonHoldsSharesInPrivateEntities)
                .ThenInclude(p => p.ShareHolder)
                .ThenInclude(p => p.PersonRepresentsPrivateEntities)
                .ThenInclude(p => p.Beneficiary)
                .ThenInclude(b => b.PrivateEntitySubscriptions)
                .ThenInclude(s => s.ShareClause)
                .Where(p => p.CurrentApplication.SortingOffice.CityId == sortingOffice &&
                            p.CurrentApplication.Status == EApplicationStatus.Examined &&
                            p.CurrentApplication.RaisedQueries.Count == 0)
                .ToListAsync();

            List<AllocatedPrivateEntityTaskApplicationResponseDto> privateEntityApplications =
                new List<AllocatedPrivateEntityTaskApplicationResponseDto>();

            foreach (var privateEntity in privateEntities)
            {
                var privateEntityApplication =
                    _mapper.Map<AllocatedPrivateEntityTaskApplicationResponseDto>(privateEntity);

                foreach (var nominee in privateEntity.PersonHoldsSharesInPrivateEntities)
                {
                    var nomineeDto = _mapper.Map<TaskPrivateEntityShareHolderResponseDto>(nominee.ShareHolder);
                    foreach (var beneficiary in nominee.ShareHolder.PersonRepresentsPersons)
                    {
                        var benefactor = _mapper.Map<TaskPrivateEntityShareHolderResponseDto>(beneficiary.Beneficiary);
                        foreach (var subscription in beneficiary.Beneficiary.PersonSubscriptions)
                        {
                            var sub = _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(subscription);
                            benefactor.Subscriptions.Add(sub);
                        }

                        nomineeDto.Beneficiaries.Add(benefactor);
                    }

                    foreach (var beneficiaryEntity in nominee.ShareHolder.PersonRepresentsPrivateEntities)
                    {
                        var entity = _mapper.Map<TaskShareHoldingEntityRequestDto>(beneficiaryEntity.Beneficiary);
                        foreach (var entitySubscription in beneficiaryEntity.Beneficiary.PrivateEntitySubscriptions)
                        {
                            var sub =
                                _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(entitySubscription);
                            entity.Subscriptions.Add(sub);
                        }

                        nomineeDto.RepresentedEntities.Add(entity);
                    }

                    foreach (var beneficiaryEntity in nominee.ShareHolder.PersonRepresentsForeignEntities)
                    {
                        var entity = _mapper.Map<TaskShareHoldingEntityRequestDto>(beneficiaryEntity.Beneficiary);
                        foreach (var entitySubscription in beneficiaryEntity.Beneficiary.ForeignEntitySubscriptions)
                        {
                            var sub =
                                _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(entitySubscription);
                            entity.Subscriptions.Add(sub);
                        }

                        nomineeDto.RepresentedEntities.Add(entity);
                    }

                    foreach (var subscription in nominee.ShareHolder.PersonSubscriptions)
                    {
                        var sub = _mapper.Map<TaskPrivateEntityShareholderSubscriptionResponseDto>(subscription);
                        nomineeDto.Subscriptions.Add(sub);
                    }

                    privateEntityApplication.Members.Add(nomineeDto);
                }

                privateEntityApplications.Add(privateEntityApplication);
            }

            return privateEntityApplications;
        }

        public async Task<int> ApprovePrivateEntityApplication(int applicationId)
        {
            var application = await _context.Applications.Include(a => a.RaisedQueries)
                .SingleAsync(a => a.ApplicationId == applicationId && a.Status == EApplicationStatus.Examined);
            application.Status = EApplicationStatus.Approved;
            return await _context.SaveChangesAsync();
        }
    }
}