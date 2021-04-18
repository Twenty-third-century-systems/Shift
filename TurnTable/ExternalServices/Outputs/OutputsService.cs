using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.External.Response;
using Fridge.Constants;
using Fridge.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TurnTable.ExternalServices.Outputs {
    public class OutputsService : IOutputsService {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public OutputsService(MainDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservedNameRequestDto> NameSearchSummary(int applicationId)
        {
            var application = await _context.Applications
                .Include(a => a.NameSearch)
                .ThenInclude(n => n.Names)
                .SingleAsync(a => a.ApplicationId == applicationId);

            var reservedName = application.NameSearch.Names.SingleOrDefault(n =>
                n.Status == ENameStatus.Reserved || n.Status == ENameStatus.Used);
            if (reservedName != null)
                return _mapper.Map<ReservedNameRequestDto>(application);
            return null;
        }

        public async Task<int> GetUsedNameSearchApplicationId(int applicationId)
        {
            var usedName = await _context.PrivateEntities.Include(p => p.Name)
                .ThenInclude(n => n.NameSearch)
                .ThenInclude(n => n.Application)
                .SingleAsync(p => p.CurrentApplication.ApplicationId == applicationId);

            return usedName.Name.NameSearch.Application.ApplicationId;
        }

        public async Task<PrivateEntitySummaryRequestDto> GetRegisteredPrivateEntitySummary(int applicationId)
        {
            var privateEntity = await _context.PrivateEntities
                .Include(p => p.CurrentApplication)
                .ThenInclude(a => a.SortingOffice)
                .Include(p => p.Name)
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
                .SingleOrDefaultAsync(p => p.CurrentApplication.ApplicationId == applicationId);

            var dto = _mapper.Map<PrivateEntitySummaryRequestDto>(privateEntity);
            foreach (var nominee in privateEntity.PersonHoldsSharesInPrivateEntities)
            {
                dto.Subscribers.Add(_mapper.Map<PrivateEntitySummaryRequestDto.Subscriber>(nominee.ShareHolder));
            }

            return dto;
        }

        public async Task<RegisteredPrivateEntityRequestDto> GetRegisteredPrivateEntity(int applicationId)
        {
            return await _mapper.ProjectTo<RegisteredPrivateEntityRequestDto>(
                _context.PrivateEntities
                    .Include(p => p.Name)
                    .Include(p => p.CurrentApplication)
                    .Where(p => p.ApplicationId == applicationId))
                .SingleAsync();
        }
    }
}