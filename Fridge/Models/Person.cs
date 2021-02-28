using System;
using System.Collections.Generic;
using Fridge.Constants;

// director member
namespace Fridge.Models {
    public class Person {
        public Person()
        {
            Beneficiaries = new HashSet<PrivateEntityOwnerHasPrivateEntityOwner>();
            Nominees = new HashSet<PrivateEntityOwnerHasPrivateEntityOwner>();
            ShareHoldingEntities = new HashSet<PrivateEntityHasPrivateEntityOwner>();
            RepresentedForeignEntities = new HashSet<ShareHoldingForeignEntityHasPrivateEntityOwner>();
            Subscriptions = new HashSet<PrivateEntityOwnerHasShareClause>();
        }

        public int PrivateEntityOwnerId { get; set; }
        public string CountryCode { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public EGender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalIdentification { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public bool IsSecretary { get; set; }
        public bool IsDirector { get; set; }
        public string Occupation { get; set; }
        public DateTime? DateOfTakeUp { get; set; }
        

        public Country Country { get; set; }
        public ICollection<PrivateEntityOwnerHasPrivateEntityOwner> Beneficiaries { get; set; }
        public ICollection<PrivateEntityOwnerHasPrivateEntityOwner> Nominees { get; set; }
        public ICollection<PrivateEntityHasPrivateEntityOwner> ShareHoldingEntities { get; set; }
        public ICollection<ShareHoldingForeignEntityHasPrivateEntityOwner> RepresentedForeignEntities { get; set; }
        public ICollection<PrivateEntityOwnerHasShareClause> Subscriptions { get; set; }
    }
}