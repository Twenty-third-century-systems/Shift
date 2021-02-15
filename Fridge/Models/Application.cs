using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Fridge.Constants;

#nullable disable

namespace Fridge.Models {
    public class Application {
        public Application()
        {
            RaisedQueries = new HashSet<RaisedQuery>();
        }
        public int ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public EService Service { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime? DateExamined { get; set; }
        public EApplicationStatus Status { get; set; }
        public int CityId { get; set; }
        public int? TaskId { get; set; }
        public ICollection<RaisedQuery> RaisedQueries { get; set; }
        public bool SoftDeleted { get; private set; }


        public NameSearch NameSearch { get; set; }
        public ExaminationTask ExaminationTask { get; set; }
        public City SortingOffice { get; set; }
        public PrivateEntity PrivateEntity { get; set; }
        public PrivateEntity PrivateEntityLastApplication { get; set; }
        public PrivateEntity PrivateEntityNameSearchApplication { get; set; }

        public void Delete()
        {
            SoftDeleted = true;
        }

        public bool WasSubmittedBy(Guid userId)
        {
            return this.UserId.CompareTo(userId) == 0;
        }

        public string FormattedDateOfSubmission()
        {
            return DateSubmitted.ToString("d");
        }

        public bool WasExamined()
        {
            return Status == EApplicationStatus.Examined;
        }

        public bool HasQueries()
        {
            return RaisedQueries.Count > 0;
        }

        public bool IsInACountableState()
        {
            return !Status.Equals(EApplicationStatus.Incomplete);
        }

        public bool IsAPrivateEntityApplication()
        {
            return Service.Equals(EService.PrivateLimitedCompany);
        }

        public bool WasExaminedAndApproved()
        {
            return Status.Equals(EApplicationStatus.Examined) && RaisedQueries.Count.Equals(0);
        }

        public bool IsAnApprovedPrivateEntity()
        {
            return IsAPrivateEntityApplication() && WasExaminedAndApproved();
        }

        public bool ByUser(Guid userId)
        {
            return UserId.CompareTo(UserId).Equals(0);
        }

        public bool IsANameSearchApplication()
        {
            return Service.Equals(EService.NameSearch);
        }

        public bool IsAnApprovedNameSearch()
        {
            if (Service.Equals(EService.NameSearch) && WasExamined())
            {
                if (NameSearch != null)
                {
                    if (NameSearch.Names.Count > 0)
                    {
                        foreach (var name in NameSearch.Names)
                        {
                            if (name.Status.Equals(ENameStatus.Reserved))
                                return true;
                        }
                    }
                }
            }

            return false;
        }
    }

    public class RaisedQuery {
        public EApplicationSteps Step { get; set; }
        public string Comment { get; set; }
    }
}