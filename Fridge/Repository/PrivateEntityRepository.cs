using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class PrivateEntityRepository {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public PrivateEntityRepository(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SubmittedApplicationRequestDto>> GetRegisteredEntitiesAsync(Guid userId)
        {
            return await _mapper
                .ProjectTo<SubmittedApplicationRequestDto>(
                    _context.PrivateEntities.Where(p => p.ServiceApplication.WasSubmittedBy(userId) && p.WasExaminedAndApproved())).
                ToListAsync();
        }
    }
}