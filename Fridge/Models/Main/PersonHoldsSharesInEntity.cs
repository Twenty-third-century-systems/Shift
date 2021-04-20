namespace Fridge.Models.Main {
    public class PersonHoldsSharesInPrivateEntity {
        public PersonHoldsSharesInPrivateEntity()
        {
            
        }
        public PersonHoldsSharesInPrivateEntity(PrivateEntity privateEntity, ShareHolder shareHolder)
        {
            PrivateEntitySubscribed = privateEntity;
            ShareHolder = shareHolder;
        }

        public int ShareHolderId { get; set; }
        public int PrivateEntityId { get; set; }

        public PrivateEntity PrivateEntitySubscribed { get; set; }
        public ShareHolder ShareHolder { get; set; }
    }
}