using System.Threading.Tasks;
using AutoMapper;
using BarTender.Models;
using Cabinet.Dtos.External.Request;
using Drinkers.ExternalApiClients.NameSearch;
using Microsoft.AspNetCore.Mvc;

namespace Dab.Controllers {
    [Route("name-search")]
    public class NameSearchController : Controller {
        private readonly INameSearchApiClientService _nameSearchApiClientService;
        private readonly IMapper _mapper;

        public NameSearchController(INameSearchApiClientService nameSearchApiClientService, IMapper mapper)
        {
            _nameSearchApiClientService = nameSearchApiClientService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        public async Task<IActionResult> NewNameSearch()
        {
            ViewBag.Defaults = await _nameSearchApiClientService.GetNameSearchDefaultsAsync();
            return View();
        }

        [HttpPost("submission")]
        public async Task<IActionResult> NewSubmission(NewNameSearchFormRequestDto dto)
        {
            var newNameSearchRequestDto = _mapper.Map<NewNameSearchRequestDto>(dto);

            newNameSearchRequestDto.Names.Add(new SuggestedEntityNameRequestDto
            {
                Value = dto.Name1.ToUpper()
            });

            newNameSearchRequestDto.Names.Add(new SuggestedEntityNameRequestDto
            {
                Value = dto.Name2.ToUpper()
            });

            if (!string.IsNullOrEmpty(dto.Name3))
                newNameSearchRequestDto.Names.Add(new SuggestedEntityNameRequestDto
                {
                    Value = dto.Name3.ToUpper()
                });

            if (!string.IsNullOrEmpty(dto.Name4))
                newNameSearchRequestDto.Names.Add(new SuggestedEntityNameRequestDto
                {
                    Value = dto.Name4.ToUpper()
                });

            if (!string.IsNullOrEmpty(dto.Name5))
                newNameSearchRequestDto.Names.Add(new SuggestedEntityNameRequestDto
                {
                    Value = dto.Name5.ToUpper()
                });

            var submittedNameSearch = await _nameSearchApiClientService.NewNameSearchAsync(newNameSearchRequestDto);
            if (submittedNameSearch != null)
            {
                return Ok(submittedNameSearch);
            }

            return BadRequest("Insufficient funds");
        }

        [HttpGet("availability/name")]
        public async Task<IActionResult> NameAvailability(Names name)
        {
            string nameToSend;
            if (!string.IsNullOrEmpty(name.name1))
                nameToSend = name.name1;
            else if (!string.IsNullOrEmpty(name.name2))
                nameToSend = name.name2;
            else if (!string.IsNullOrEmpty(name.name3))
                nameToSend = name.name3;
            else if (!string.IsNullOrEmpty(name.name4))
                nameToSend = name.name4;
            else
                nameToSend = name.name5;

            var nameAvailable = await _nameSearchApiClientService.IsNameAvailableAsync(nameToSend);
            if (nameAvailable)
                return Ok(true);
            else return Ok("\"This name is not available\"");
        }

        [HttpGet("f-reserve")]
        public IActionResult FurtherReserveUnexpiredName()
        {
            return View();
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> FurtherReserve(NameSearchReservationRequestDto dto)
        {
            if (await _nameSearchApiClientService.FurtherReserveUnexpiredNameAsync(dto.Reference))
                return Ok();
            return BadRequest();
        }
    }
}