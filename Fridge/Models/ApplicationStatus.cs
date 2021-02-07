using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class ApplicationStatus
    {
        public int ApplicationStatusId { get; set; }
        public string Description { get; set; }
        
        public ICollection<EntityName> EntityNames { get; set; }
        public ICollection<ServiceApplication> Applications { get; set; }
    }
}
