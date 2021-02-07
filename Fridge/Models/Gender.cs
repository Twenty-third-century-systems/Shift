using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class Gender
    {
        public int GenderId { get; set; }
        public string Description { get; set; }
        
        public ICollection<PrivateEntitySubscriber> PrivateEntitySubscribers { get; set; }
    }
}
