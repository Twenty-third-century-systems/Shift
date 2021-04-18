using System;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models;

namespace TurnTable.InternalServices.PrivateEntityExamination {
    public class PrivateEntityExaminationService : IPrivateEntityExaminationService {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public PrivateEntityExaminationService(MainDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> FinishExaminationAsync(int applicationId)
        {
            var application = await _context.Applications.FindAsync(applicationId);
            application.Status = EApplicationStatus.Examined;
            application.DateExamined = DateTime.Now;
            return await _context.SaveChangesAsync();
            // TODO: send email or txt based on SaveChanges, message should contain if successful or has query
        }

        public async Task<int> RaiseQuery(int applicationId, int step, string comment)
        {
            return 0;
        }

        public async Task<int> RaiseQueryAsync(RaisedQueryRequestDto dto)
        {
            var application = await _context.Applications.FindAsync(dto.ApplicationId);
            application.RaisedQueries.Add(_mapper.Map<RaisedQuery>(dto));
            return await _context.SaveChangesAsync();
        }
    }
}