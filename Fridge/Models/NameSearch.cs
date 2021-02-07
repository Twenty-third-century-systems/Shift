using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class NameSearch
    {
        public NameSearch()
        {
            EntityNames = new HashSet<EntityName>();
        }

        public int NameSearchId { get; set; }
        public int ServiceId { get; set; }
        public string Justification { get; set; }
        public int DesignationId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int ServiceApplicationId { get; set; }
        public int ReasonForSearchId { get; set; }
        public string Reference { get; set; }

        public ServiceType Service { get; set; }
        public Designation Designation { get; set; }
        public ServiceApplication ServiceApplication { get; set; }
        public ReasonForNameSearch ReasonForNameSearch { get; set; }
        public ICollection<EntityName> EntityNames { get; set; }

        public bool WasApproved()
        {
            return ExpiryDate != null;
        }
    }
}
