using Fridge.Constants;

namespace Fridge.Models.Payments {
    public class PriceItem {
        public int PriceItemId { get; set; }
        public EService Service { get; set; }
        public double Price { get; set; }
    }
}