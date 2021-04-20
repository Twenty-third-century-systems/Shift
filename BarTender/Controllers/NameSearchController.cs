using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BarTender.Models;
using Cabinet.Dtos.External.Request;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TurnTable.ExternalServices.NameSearch;
using TurnTable.ExternalServices.Values;

namespace BarTender.Controllers {
    [Route("api/name")]
    public class NameSearchController : Controller {
        private readonly IOptions<List<ServicesForNameSearchSelection>> _serviceValues;
        private readonly IOptions<List<ReasonForSearchForNameSearchSelection>> _reasonsValues;
        private readonly IOptions<List<DesignationsForNameSearchSelection>> _designationValues;
        private readonly IValueService _valueService;
        private readonly INameSearchService _nameSearchService;

        public NameSearchController(IOptions<List<ServicesForNameSearchSelection>> serviceValues,
            IOptions<List<ReasonForSearchForNameSearchSelection>> reasonsValues,
            IOptions<List<DesignationsForNameSearchSelection>> designationValues, IValueService valueService,
            INameSearchService nameSearchService)
        {
            _nameSearchService = nameSearchService;
            _valueService = valueService;
            _designationValues = designationValues;
            _reasonsValues = reasonsValues;
            _serviceValues = serviceValues;
        }

        [AllowAnonymous]
        [HttpGet("defaults")]
        public async Task<IActionResult> GetDefaults()
        {
            var servicesForNameSearchSelections = _serviceValues.Value.Where(v => !v.id.Equals(0));
            return Ok(new
            {
                Services = servicesForNameSearchSelections,
                ReasonForSearch = _reasonsValues.Value,
                Designations = _designationValues.Value,
                SortingOffices = await _valueService.GetSortingOfficesAsync()
            });
        }

        [AllowAnonymous]
        [HttpHead("{name}/availability")]
        public IActionResult GetNameAvailability(string name)
        {
            if (_nameSearchService.NameIsAvailable(name))
                return NoContent();
            return BadRequest("The suggested name is not available for reservation");
        }

        [AllowAnonymous]
        [HttpPost("submit")]
        public async Task<IActionResult> PostNewNameSearch([FromBody] NewNameSearchRequestDto details)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    var userDetailsFromAuth = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(userDetailsFromAuth);
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Created("", await _nameSearchService.CreateNewNameSearchAsync(user.Sub, details));
        }


        [HttpHead("further")]
        public async Task<IActionResult> FurtherReserveUnexpiredName(string reference)
        {
            try
            {
                if (await _nameSearchService.FurtherReserveUnexpiredNameAsync(reference) > 0)
                    return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NotFound();
        }
    }
}