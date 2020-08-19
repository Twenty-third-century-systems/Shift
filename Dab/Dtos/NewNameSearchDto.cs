using System;

namespace BarTender.Dtos {
    public class NewNameSearchDto {
        public Guid Id { get; set; }
        public int Reason { get; set; }
        public int Type { get; set; }
        public int Designation { get; set; }
        public int Office { get; set; }
        public string Justification { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
    }
}