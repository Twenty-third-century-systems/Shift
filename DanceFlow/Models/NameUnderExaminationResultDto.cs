using System;

namespace DanceFlow.Dtos {
    public class NameUnderExaminationResultDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string TypeOfBusiness { get; set; }
        public string Status { get; set; }
    }
}