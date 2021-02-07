using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class CityRepository {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public CityRepository(MainDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<City> GetHarareCityAsync()
        {
            return await _context.Cities.SingleAsync(c => c.Name.Equals("Harare"));
        }

        public async Task<City> GetBulawayoCityAsync()
        {
            return await _context.Cities.SingleAsync(c => c.Name.Equals("Bulawayo"));
        }

        public List<SelectionValueResponseDto> GetSortingOfficesForSelection()
        {
            return _mapper.ProjectTo<SelectionValueResponseDto>(_context.Cities.Where(c => c.CanSort)
                    .AsNoTracking()).ToList();
        }
    }
}