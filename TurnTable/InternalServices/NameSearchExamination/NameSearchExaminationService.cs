using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Fridge.Constants;
using Fridge.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TurnTable.InternalServices.NameSearchExamination {
    public class NameSearchExaminationService : INameSearchExaminationService {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public NameSearchExaminationService(MainDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> ChangeNameStatusAsync(int nameId, int status)
        {
            var name = await _context.Names.FindAsync(nameId);
            name.Status = (ENameStatus) status;

            if (status.Equals((int) ENameStatus.Reserved))
            {
                await _context.Entry(name).Reference(n => n.NameSearch).LoadAsync();
                await _context.Entry(name.NameSearch).Collection(n => n.Names).LoadAsync();

                var unconsideredNames = name.NameSearch.Names.Where(n =>
                        !n.EntityNameId.Equals(name.EntityNameId) && n.Status.Equals(ENameStatus.Pending))
                    .ToList();
                foreach (var unconsideredName in unconsideredNames)
                {
                    unconsideredName.Status = ENameStatus.NotConsidered;
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> FinishExaminationAsync(int nameSearchId)
        {
            var nameSearch = await _context.NameSearches
                .Include(n => n.Names)
                .Include(n => n.Application)
                .SingleAsync(n => n.NameSearchId.Equals(nameSearchId));

            foreach (var name in nameSearch.Names)
            {
                if (name.Status.Equals(ENameStatus.Pending))
                    name.Status = ENameStatus.NotConsidered;
            }

            nameSearch.Application.Status = EApplicationStatus.Examined;
            nameSearch.Application.DateExamined = DateTime.Now;
            nameSearch.ExpiryDate = DateTime.Now.AddDays(30);

            return await _context.SaveChangesAsync();
            // TODO: send email and text msg here based on SaveChanges return result, message should contain if successful or has query
        }

        public async Task<List<NameRequestDto>> GetNamesThatStartWithAsync(string searchQuery)
        {
            return await _mapper.ProjectTo<NameRequestDto>(_context.Names.Include(n => n.NameSearch)
                    .ThenInclude(n => n.Application)
                    .Where(n => n.Value.StartsWith(searchQuery) && n.Value != searchQuery))
                .ToListAsync();
        }

        public async Task<List<NameRequestDto>> GetNamesThatContainAsync(string searchQuery)
        {
            return await _mapper.ProjectTo<NameRequestDto>(_context.Names.Include(n => n.NameSearch)
                    .ThenInclude(n => n.Application)
                    .Where(n => n.Value.Contains(searchQuery) && n.Value != searchQuery))
                .ToListAsync();
        }

        public async Task<List<NameRequestDto>> GetNamesThatEndsWithAsync(string searchQuery)
        {
            return await _mapper.ProjectTo<NameRequestDto>(_context.Names.Include(n => n.NameSearch)
                    .ThenInclude(n => n.Application)
                    .Where(n => n.Value.EndsWith(searchQuery) && n.Value != searchQuery))
                .ToListAsync();
        }
    }
}