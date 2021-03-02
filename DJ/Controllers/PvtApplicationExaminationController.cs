using System;
using System.Linq;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cooler.DataModels;
using Fridge.Models;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnTable.ExternalServices;
using TurnTable.InternalServices;

namespace DJ.Controllers {
    [Route("api/ex/pvt")]
    public class PvtApplicationExaminationController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;
        private readonly IPrivateEntityExaminationService _privateEntityExaminationService;

        public PvtApplicationExaminationController(EachDB eachDb, PoleDB poleDb,
            IPrivateEntityExaminationService privateEntityExaminationService)
        {
            _eachDb = eachDb;
            _poleDb = poleDb;
            _privateEntityExaminationService = privateEntityExaminationService;
        }

        [AllowAnonymous]
        [HttpPatch("q")]
        public async Task<IActionResult> RaiseQuery([FromBody] RaisedQueryRequestDto dto)
        {
            if (await _privateEntityExaminationService.RaiseQueryAsync(dto) > 0)
                return Created("", dto);
            return BadRequest("Could not raise query");
        }

        [AllowAnonymous]
        [HttpPatch("f/{applicationId}")]
        public async Task<IActionResult> FinishPvtApplicationExamination(int applicationId)
        {
            // var application = (
            //     from a in _eachDb.Applications
            //     where a.Id == applicationId
            //     select a
            // ).FirstOrDefault();
            //
            // if (application != null)
            // {
            //     var service = (from s in _poleDb.Services
            //             where s.Id == application.ServiceId
            //             select s
            //         ).FirstOrDefault();
            //
            //     if (service != null && service.Description.Equals("private limited company"))
            //     {
            //         var pvtApplication = (
            //             from p in _eachDb.PvtEntities
            //             where p.ApplicationId == application.Id &&
            //                   p.Reference.Equals(null)
            //             select p
            //         ).FirstOrDefault();
            //
            //         if (pvtApplication != null)
            //         {
            //             application.Status = (
            //                 from s in _poleDb.Status
            //                 where s.Description.Equals("examined")
            //                 select s.Id
            //             ).FirstOrDefault();
            //
            //             application.DateExamined = DateTime.Now;
            //
            //             pvtApplication.Reference =
            //                 "PVT/" + DateTime.Now.Year.ToString() + "/" + application.Id.ToString();
            //
            //             if (_eachDb.Update(application) + _eachDb.Update(pvtApplication) == 2)
            //             {
            //                 // Send Sms Notifications and email here
            //                 return NoContent();
            //             }
            //         }
            //     }
            // }
            if (await _privateEntityExaminationService.FinishExaminationAsync(applicationId) > 0)
                return NoContent();

            return BadRequest("Could not finish this application.");
        }
    }
}