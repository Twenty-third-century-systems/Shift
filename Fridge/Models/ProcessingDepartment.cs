using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class ProcessingDepartment
    {
        public ProcessingDepartment()
        {
            Services = new HashSet<ServiceType>();
        }

        public int ProcessingDepartmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<ServiceType> Services { get; set; }
    }
}
