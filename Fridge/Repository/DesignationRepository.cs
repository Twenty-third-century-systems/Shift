using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class DesignationRepository {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public DesignationRepository(MainDatabaseContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public List<SelectionValueResponseDto> GetDesignationsForSelection()
        {
            return _mapper.ProjectTo<SelectionValueResponseDto>(_context.Designations.AsNoTracking())
                .ToList();
        }
    }
}