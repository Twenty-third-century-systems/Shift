using System;
using System.Collections.Generic;
using Fridge.Constants;

// director member
namespace Fridge.Models {
    public class Person {
        public Person()
        {
            PersonSubscriptions = new HashSet<PersonSubscription>();
            PersonHoldsSharesInPrivateEntities = new HashSet<PersonHoldsSharesInPrivateEntity>();
            PersonRepresentsForeignEntities = new HashSet<PersonRepresentsForeignEntity>();
            PersonRepresentsPersons = new HashSet<PersonRepresentsPerson>();
            PersonRepresentsPersonss = new HashSet<PersonRepresentsPerson>();
            PersonRepresentsPrivateEntities = new HashSet<PersonRepresentsPrivateEntity>();
        }

        public int PersonId { get; set; }
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

        //subscriptions
        public ICollection<PersonSubscription> PersonSubscriptions { get; set; }

        // entities subscribed
        public ICollection<PersonHoldsSharesInPrivateEntity> PersonHoldsSharesInPrivateEntities { get; set; }

        // Beneficiary foreign entities
        public ICollection<PersonRepresentsForeignEntity> PersonRepresentsForeignEntities { get; set; }

        // Beneficiary people
        public ICollection<PersonRepresentsPerson> PersonRepresentsPersons { get; set; }

        //people  Nominees
        public ICollection<PersonRepresentsPerson> PersonRepresentsPersonss { get; set; }

        // Beneficiary Private Entities
        public ICollection<PersonRepresentsPrivateEntity> PersonRepresentsPrivateEntities { get; set; }
    }
}