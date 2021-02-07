using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class ServiceTypeRepository {
        private MainDatabaseContext _context;
        private IMapper _mapper;
        private const int NameSearch = 0;
        private const int PrivateEntity = 0;

        public ServiceTypeRepository(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceType> GetNameSearchServiceAsync()
        {
            return await _context.Services.SingleAsync(s => s.Description.Equals("name search"));
        }

        public async Task<ServiceType> GetPrivateEntityServiceAsync()
        {
            return await _context.Services.SingleAsync(s => s.Description.Equals("private entity"));
        }

        public List<SelectionValueResponseDto> GetServiceTypesForSearchForSelection()
        {
            return _mapper
                .ProjectTo<SelectionValueResponseDto>(_context.Services.Where(s => s.CanBeApplied)
                    .AsNoTracking()).ToList();
        }
    }
}