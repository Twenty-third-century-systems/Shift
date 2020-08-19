using System.Linq;
using BarTender.DataModels;
using BarTender.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarTender.Controllers
{
    [Route("api/name")]
    public class NameSearchController : Controller
    {
        private INameSearchRepository _nameSearchRepository;
        private PoleDB _poleDb;
        private ShwaDB _shwaDb;

        public NameSearchController(INameSearchRepository nameSearchRepository, PoleDB poleDb, ShwaDB shwaDb)
        {
            _nameSearchRepository = nameSearchRepository;
            _poleDb = poleDb;
            _shwaDb = shwaDb;
        }

        [HttpGet("defaults")]
        public IActionResult GetDefaults()
        {
            var defaults = _nameSearchRepository.GetDefaults(_poleDb,_shwaDb);
            return Ok(defaults);            
        }
    }
}