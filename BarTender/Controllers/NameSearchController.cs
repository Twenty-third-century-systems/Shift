using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BarTender.Dtos;
using BarTender.Models;
using BarTender.Repositories;
using Cabinet.Dtos.External.Request;
using Cooler.DataModels;
using IdentityModel.Client;
using LinqToDB;
using LinqToDB.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TurnTable.ExternalServices;
using NameSearchResponseDto = BarTender.Dtos.NameSearchResponseDto;

namespace BarTender.Controllers {
    [Route("api/name")]
    public class NameSearchController : Controller {
        private INameSearchRepository _nameSearchRepository;
        private PoleDB _poleDb;
        private ShwaDB _shwaDb;
        private EachDB _eachDb;
        private readonly IOptions<List<Val>> _values;
        private readonly IConfiguration _configuration;
        private IOptions<List<ServicesForNameSearchSelection>> _serviceValues;
        private IOptions<List<ReasonForSearchForNameSearchSelection>> _reasonsValues;
        private IOptions<List<DesignationsForNameSearchSelection>> _designationValues;
        private IValueService _valueService;
        private INameSearchService _nameSearchService;

        public NameSearchController(INameSearchRepository nameSearchRepository, PoleDB poleDb, ShwaDB shwaDb,
            EachDB eachDb, IOptions<List<ServicesForNameSearchSelection>> serviceValues,
            IOptions<List<ReasonForSearchForNameSearchSelection>> reasonsValues,
            IOptions<List<DesignationsForNameSearchSelection>> designationValues, IValueService valueService,
            INameSearchService nameSearchService)
        {
            _nameSearchService = nameSearchService;
            _valueService = valueService;
            _designationValues = designationValues;
            _reasonsValues = reasonsValues;
            _serviceValues = serviceValues;
            _nameSearchRepository = nameSearchRepository;
            _poleDb = poleDb;
            _shwaDb = shwaDb;
            _eachDb = eachDb;
        }

        [AllowAnonymous]
        [HttpGet("defaults")]
        public async Task<IActionResult> GetDefaults()
        {
            return Ok(new
            {
                Services = _serviceValues.Value.Where(v => !v.id.Equals(1)),
                ReasonForSearch = _reasonsValues.Value,
                Designations = _designationValues.Value,
                SortingOffices = await _valueService.GetSortingOfficesAsync()
            });
        }

        [AllowAnonymous]
        [HttpGet("{name}/availability")]
        public IActionResult GetNameAvailability(string name)
        {
            // var namesInDataBase = (
            //     from n in _eachDb.Names
            //     where n.Value == name
            //     select n
            // ).ToList();
            //
            // foreach (var nameReturned in namesInDataBase)
            // {
            //     if (nameReturned.Status != 4)
            //     {
            //         return BadRequest();
            //     }
            // }

            if (_nameSearchService.NameIsAvailable(name))
                return NoContent();
            return BadRequest("The suggested name is not available for reservation");
        }

        [AllowAnonymous]
        [HttpPost("submit")]
        public async Task<IActionResult> PostNewNameSearch([FromBody] NewNameSearchRequestDto details)
        {
            // User user;
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
            //     if (response.IsSuccessStatusCode)
            //     {
            //         var userDetailsFromAuth = await response.Content.ReadAsStringAsync();
            //         user = JsonConvert.DeserializeObject<User>(userDetailsFromAuth);
            //     }
            //     else
            //     {
            //         // TODO: to substitute with NOT ALLOWED
            //         return BadRequest("Not allowed");
            //     }
            // }
            //
            //
            // using (var client = new HttpClient())
            // {
            //     var paymentDataDto = new PaymentDataDto
            //     {
            //         Email = "brightonkofu@outlook.com",
            //         Service = 1,
            //         UserId = Guid.Parse(user.Sub)
            //     };
            //
            //     //TODO: get email from authority
            //
            //
            //     var responce = await client
            //         .PostAsJsonAsync<PaymentDataDto>("https://localhost:44375/api/Payments/Service", paymentDataDto);
            //
            //
            //     if (responce.IsSuccessStatusCode)
            //     {
            //         int status = 1;
            //
            //         var service = (
            //             from value in _poleDb.Services
            //             where value.Description == "name search"
            //             select value
            //         ).FirstOrDefault();
            //
            //         if (service != null)
            //         {
            //             var applicationId = _eachDb.Applications
            //                 .Value(a => a.UserId, user.Sub)
            //                 .Value(a => a.ServiceId, service.Id)
            //                 .Value(a => a.DateSubmitted, DateTime.Now)
            //                 .Value(a => a.Status, status)
            //                 .Value(a => a.SortingOffice, details.Details.SortingOffice)
            //                 .InsertWithInt32Identity();
            //
            //             if (applicationId != null)
            //             {
            //                 Guid nameSearchId = Guid.NewGuid();
            //                 int nameSearchSubmissionResult = _eachDb.NameSearches
            //                     .Value(b => b.Id, nameSearchId.ToString())
            //                     .Value(b => b.Service, details.Details.TypeOfEntity)
            //                     .Value(b => b.Justification, details.Details.Justification)
            //                     .Value(b => b.DesignationId, details.Details.Designation)
            //                     .Value(b => b.ApplicationId, applicationId)
            //                     .Value(b => b.ReasonForSearch, details.Details.ReasonForSearch)
            //                     .Value(b => b.Reference, Guid.NewGuid().ToString())
            //                     .Insert();
            //                 if (nameSearchSubmissionResult == 1)
            //                 {
            //                     int namesSubmited = 0;
            //                     foreach (var name in details.Names)
            //                     {
            //                         int nameStatus = 7;
            //                         namesSubmited += _eachDb.Names
            //                             .Value(c => c.Value, name)
            //                             .Value(c => c.Status, nameStatus)
            //                             .Value(c => c.NameSearchId, nameSearchId.ToString())
            //                             .Insert();
            //                     }
            //
            //                     if (namesSubmited >= 2)
            //                     {
            //                         return Created("/submited", new NameSearchResponseDto
            //                         {
            //                             Id = nameSearchId,
            //                             Details = details.Details,
            //                             Names = details.Names
            //                         });
            //                     }
            //                     else
            //                     {
            //                         return StatusCode(StatusCodes.Status500InternalServerError,
            //                             "Failed to insert Names");
            //                     }
            //                 }
            //                 else
            //                 {
            //                     return StatusCode(StatusCodes.Status500InternalServerError,
            //                         "Failed to insert NewNameSearchApplicationDto search");
            //                 }
            //             }
            //             else
            //             {
            //                 return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create application");
            //             }
            //         }
            //         else
            //         {
            //             return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            //         }
            //     }
            //     else
            //     {
            //         return BadRequest("Insufficient funds");
            //     }
            // }

            return Created("", await _nameSearchService.CreateNewNameSearchAsync(Guid.NewGuid(), details));
        }
    }
}