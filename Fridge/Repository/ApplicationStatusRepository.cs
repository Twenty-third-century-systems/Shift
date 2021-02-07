using System.Threading.Tasks;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class ApplicationStatusRepository {
        private MainDatabaseContext _context;

        public ApplicationStatusRepository(MainDatabaseContext context)
        {
            _context = context;
        }

        public async Task<ApplicationStatus> GetSubmittedStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("submitted"));
        }
        
        public async Task<ApplicationStatus> GetAssignedStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("assigned"));
        }
        
        public async Task<ApplicationStatus> GetExaminedStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("examined"));
        }
        
        public async Task<ApplicationStatus> GetNotConsideredStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("not considered"));
        }
        
        public async Task<ApplicationStatus> GetRejectedStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("rejected"));
        }
        
        public async Task<ApplicationStatus> GetBlacklistedStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("blacklisted"));
        }
        
        public async Task<ApplicationStatus> GetPendingStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("pending"));
        }
        
        public async Task<ApplicationStatus> GetReservedStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("reserved"));
        }
        
        public async Task<ApplicationStatus> GetIncompleteStatusAsync()
        {
            return await _context.Statuses.SingleAsync(s => s.Description.Equals("incomplete"));
        }
    }
}