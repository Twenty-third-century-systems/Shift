using System;
using Microsoft.VisualBasic;

namespace Dab.Dtos {
    public class RegisteredNameDto {
        public int NameId { get; set; }
        public string Ref { get; set; }
        public string Name { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime? DateExp { get; set; }
    }
}