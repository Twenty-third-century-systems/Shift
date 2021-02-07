using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class ReasonForNameSearch
    {
        public int ReasonForNameSearchId { get; set; }
        public string Description { get; set; }

        public ICollection<NameSearch> NameSearches { get; set; }
    }
}
