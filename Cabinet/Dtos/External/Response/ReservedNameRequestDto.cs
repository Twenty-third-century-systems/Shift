using System;

namespace Cabinet.Dtos.External.Response {
    public class ReservedNameRequestDto {
        public string Reference { get; set; }
        public string Value { get; set; }
        public string ExpiryDate { get; set; }
    }
}