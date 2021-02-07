using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class ServiceApplicationRepository {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public ServiceApplicationRepository(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task AddApplicationAsync(ServiceApplication application)
        {
            await _context.AddAsync(application);
        }

        public ServiceApplication FindApplication(int id)
        {
            return _context.Find<ServiceApplication>(id);
        }

        public bool DeleteApplication(int id)
        {
            var application = FindApplication(id);
            if (application != null)
            {
                application.Delete();
                return true;
            }

            return false;
        }

        public Task<List<SubmittedApplicationRequestDto>> GetAllApplicationsByUserAsync(Guid userId)
        {
            return _mapper
                .ProjectTo<SubmittedApplicationRequestDto>(
                    _context.Applications.Where(a => a.WasSubmittedBy(userId)))
                .ToListAsync();
        }
    }
}