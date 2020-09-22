using System.Collections.Generic;
using BarTender.Models;

namespace BarTender.Dtos {
    public class NewNameSearchApplicationDto {
        public string Value { get; set; }
        public int? ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }
        public List<Gender> Genders { get; set; }
    }
}