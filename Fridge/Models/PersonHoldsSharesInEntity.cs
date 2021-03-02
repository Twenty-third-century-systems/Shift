namespace Fridge.Models {
    public class PersonHoldsSharesInPrivateEntity {
        public PersonHoldsSharesInPrivateEntity()
        {
            
        }
        public PersonHoldsSharesInPrivateEntity(PrivateEntity privateEntity, Person shareHolder)
        {
            PrivateEntitySubscribed = privateEntity;
            ShareHolder = shareHolder;
        }

        public int ShareHolderId { get; set; }
        public int PrivateEntityId { get; set; }

        public PrivateEntity PrivateEntitySubscribed { get; set; }
        public Person ShareHolder { get; set; }
    }
}