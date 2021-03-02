namespace Fridge.Models {
    public class PersonRepresentsPerson {
        public PersonRepresentsPerson()
        {
            
        }
        public PersonRepresentsPerson(Person nominee, Person beneficiary)
        {
            Nominee = nominee;
            Beneficiary = beneficiary;
        }

        public int BeneficiaryId { get; set; }
        public int NomineeId { get; set; }

        public Person Beneficiary { get; set; }
        public Person Nominee { get; set; }
    }
}