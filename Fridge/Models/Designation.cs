using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<NameSearch> NameSearches { get; set; }
    }
}
