using System;

namespace Cabinet.Dtos.External.Response {
    public class RegisteredNameResponseDto {
        public string Id { get; set; }
        public string NameSearchReference { get; set; }
        public string Name { get; set; }
        public string DateSubmitted { get; set; }
        public string ExpiryDate { get; set; }
    }
}