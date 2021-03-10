using System;

namespace Fridge.Models {
    public class Director : Person {
        public int PrivateEntityId { get; set; }        
        public DateTime DateOfAppointment { get; set; }
        
        public PrivateEntity PrivateEntity { get; set; }
    }
}