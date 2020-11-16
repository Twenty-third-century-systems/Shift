using System;
using System.Collections.Generic;

namespace Till.Models {
    public class Credit {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int Service { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? Application { get; set; }

        public virtual Payment PaymentNavigation { get; set; }
    }
}