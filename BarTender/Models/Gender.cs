namespace BarTender.Models {
    public class Gender {
        public int Id { get; set; }
        public string Value { get; set; }

        public void Format()
        {
            Value = Value.ToUpper();
        }
    }
}