using System;
using System.Threading.Tasks;
using Fridge.Constants;
using Fridge.Contexts;
using Fridge.Models;

namespace TurnTable.InternalServices {
    public class PrivateEntityExaminationService : IPrivateEntityExaminationService {
        private readonly MainDatabaseContext _context;

        public PrivateEntityExaminationService(MainDatabaseContext context)
        {
            _context = context;
        }

        public async Task FinishExamination(int applicationId)
        {
            var application = await _context.Applications.FindAsync(applicationId);
            application.Status = EApplicationStatus.Examined;
            application.DateExamined = DateTime.Now;
            await _context.SaveChangesAsync();
            // TODO: send email or txt based on SaveChanges, message should contain if successful or has query
        }

        public async Task<int> RaiseQuery(int applicationId, int step, string comment)
        {
            var application = await _context.Applications.FindAsync(applicationId);
            application.RaisedQueries.Add(new RaisedQuery(step, comment));
            return await _context.SaveChangesAsync();
        }
    }
}