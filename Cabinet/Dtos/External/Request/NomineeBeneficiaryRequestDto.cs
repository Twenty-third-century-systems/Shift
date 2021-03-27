using System;

namespace Cabinet.Dtos.External.Request {
    public class NomineeBeneficiaryRequestDto {
        public string NomineeCountryCode { get; set; }        
        public string NomineeSurname { get; set; }       
        public string NomineeNames { get; set; }       
        public int NomineeGender { get; set; }       
        public DateTime NomineeDateOfBirth { get; set; }       
        public string NomineeNationalIdentification { get; set; }       
        public string NomineePhysicalAddress { get; set; }       
        public string NomineeMobileNumber { get; set; }       
        public string NomineeEmailAddress { get; set; }       
        public DateTime NomineeDateOfAppointment { get; set; }        
        public string NomineeOccupation { get; set; }       
        public string ShareClass { get; set; }       
        public int AmountSubscribed { get; set; }       
        public string BeneficiaryCountryCode { get; set; }       
        public string BeneficiarySurname { get; set; }       
        public string BeneficiaryNames { get; set; }       
        public int BeneficiaryGender { get; set; }       
        public DateTime BeneficiaryDateOfBirth { get; set; }       
        public string BeneficiaryNationalIdentification { get; set; }       
        public string BeneficiaryPhysicalAddress { get; set; }       
        public string BeneficiaryMobileNumber { get; set; }       
        public string BeneficiaryEmailAddress { get; set; }       
        public DateTime BeneficiaryDateOfAppointment { get; set; }       
        public string BeneficiaryEntityCountry { get; set; }       
        public string BeneficiaryEntityName { get; set; }       
        public string BeneficiaryEntityReference { get; set; }
    }
}