using System;

namespace Dab.Dtos {
    public class ReservedNameDto {
        public string NameSearchRef { get; set; }
        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}