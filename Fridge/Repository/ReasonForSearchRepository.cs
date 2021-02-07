using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cabinet.Dtos.Response;
using Fridge.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Repository {
    public class ReasonForSearchRepository {
        private MainDatabaseContext _context;
        private IMapper _mapper;

        public ReasonForSearchRepository(MainDatabaseContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<SelectionValueResponseDto> GetReasonsForSearchForSelection()
        {
            return _mapper.ProjectTo<SelectionValueResponseDto>(_context.ReasonForSearches.AsNoTracking())
                .ToList();
        }
    }
}