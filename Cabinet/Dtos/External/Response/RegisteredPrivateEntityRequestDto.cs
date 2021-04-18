using System;

namespace Cabinet.Dtos.External.Response {
    public class RegisteredPrivateEntityRequestDto {
        public string Name { get; set; }
        public string Reference { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}