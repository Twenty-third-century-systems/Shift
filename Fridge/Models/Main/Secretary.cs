using System;

namespace Fridge.Models.Main {
    public class Secretary : Person {              
        public DateTime DateOfAppointment { get; set; }
        public PrivateEntity PrivateEntity { get; set; }
    }
}