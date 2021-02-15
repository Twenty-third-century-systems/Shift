using System;
using System.Collections.Generic;

#nullable disable

namespace Zett.World
{
    public partial class Countrylanguage
    {
        public string CountryCode { get; set; }
        public string Language { get; set; }
        public string IsOfficial { get; set; }
        public decimal Percentage { get; set; }

        public virtual Country CountryCodeNavigation { get; set; }
    }
}
