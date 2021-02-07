using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string Description { get; set; }
        public bool CanBeApplied { get; set; }
        // public int IsAnEntity { get; set; }
        public int ProcessingDepartmentId { get; set; }

        public ProcessingDepartment ProcessingDepartment { get; set; }
        public ICollection<NameSearch> NameSearches { get; set; }
        public ICollection<ServiceApplication> Applications { get; set; }
    }
}
