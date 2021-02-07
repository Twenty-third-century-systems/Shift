using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cabinet.Dtos;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class NameSearchRepository {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public NameSearchRepository(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SubmittedApplicationRequestDto>> GetApprovedNameSearchesAsync(Guid userId)
        {
            return await _mapper
                .ProjectTo<SubmittedApplicationRequestDto>(_context.NameSearches.Where(n =>
                    n.ServiceApplication.WasSubmittedBy(userId) && n.WasApproved()))
                .ToListAsync();
        }

        public async Task<bool> IsNameAvailable(string suggestedName)
        {
            var entityName = await _context.Names.SingleOrDefaultAsync(n => n.Value.Equals(suggestedName));
            return entityName == null;
        }
    }
}