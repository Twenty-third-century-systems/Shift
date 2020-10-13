﻿using System;
using System.Linq;
using Cooler.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace DJ.Controllers {
    [Route("api/ex/pvt")]
    public class PvtApplicationExaminationController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;

        public PvtApplicationExaminationController(EachDB eachDb, PoleDB poleDb)
        {
            _eachDb = eachDb;
            _poleDb = poleDb;
        }

        [HttpPatch("f/{applicationId}")]
        public IActionResult FinishPvtApplicationExamination(int applicationId)
        {
            var application = (
                from a in _eachDb.Applications
                where a.Id == applicationId
                select a
            ).FirstOrDefault();

            if (application != null)
            {
                var service = (from s in _poleDb.Services
                        where s.Id == application.ServiceId
                        select s
                    ).FirstOrDefault();

                if (service != null && service.Description.Equals("private limited company"))
                {
                    var pvtApplication = (
                        from p in _eachDb.PvtEntities
                        where p.ApplicationId == application.Id &&
                              p.Reference.Equals(null)
                        select p
                    ).FirstOrDefault();

                    if (pvtApplication != null)
                    {
                        application.Status = (
                            from s in _poleDb.Status
                            where s.Description.Equals("examined")
                            select s.Id
                        ).FirstOrDefault();

                        application.DateExamined = DateTime.Now;
                        
                        pvtApplication.Reference = "PVT/" + DateTime.Now.Year.ToString() + "/" + application.Id.ToString();
                        
                        if (_eachDb.Update(application) + _eachDb.Update(pvtApplication) == 2)
                        {
                            // Send Sms Notifications and email here
                            return NoContent();
                        }
                    }                    
                }
            }

            return BadRequest("Application has incorrect information");
        }
    }
}