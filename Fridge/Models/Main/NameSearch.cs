using System;
using System.Collections.Generic;
using Fridge.Constants;

#nullable disable

namespace Fridge.Models.Main {
    public class NameSearch {
        public int NameSearchId { get; set; }
        public int ApplicationId { get; set; }
        public EService Service { get; set; }
        public string Justification { get; set; }
        public EDesignation Designation { get; set; }
        public EReasonForSearch ReasonForSearch { get; set; }
        public string MainObject { get; set; }
        public string Reference { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public ICollection<EntityName> Names { get; set; }
        
        
        public Application Application { get; set; }

        
        public bool WasApproved()
        {
            foreach (var name in Names)
            {
                if (name.Status == ENameStatus.Reserved)
                    return true;
            }

            return false;
        }

        public bool WasExamined()
        {
            return Application.WasExamined();
        }
    }
}