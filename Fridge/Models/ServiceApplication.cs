using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Fridge.Models {
    public partial class ServiceApplication {        

        public int ServiceApplicationId { get; set; }
        public Guid UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int StatusId { get; set; }
        public int CityId { get; set; }
        public DateTime? DateExamined { get; set; }
        public int? TaskId { get; set; }
        public bool SoftDeleted { get; private set; }

        
        public NameSearch NameSearch { get; set; }
        public ExaminationTask ExaminationTask { get; set; }
        public ServiceType ServiceType { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public City City { get; set; }
        public PrivateEntity PrivateEntity { get; set; }
        public PrivateEntity PrivateEntityLastApplication { get; set; }

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
    }
}