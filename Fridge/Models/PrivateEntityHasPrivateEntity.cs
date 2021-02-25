using System.Collections.Generic;

namespace Fridge.Models {
    public class PrivateEntityHasPrivateEntity {
        public PrivateEntityHasPrivateEntity()
        {
            
        }

        public PrivateEntityHasPrivateEntity(PrivateEntity entity, PrivateEntity shareHolder)
        {
            Entity = entity;
            ShareHoldingEntity = shareHolder;
        }
        public int EntityId { get; set; }
        public int ShareHoldingEntityId { get; set; }

        public PrivateEntity Entity { get; set; }
        public PrivateEntity ShareHoldingEntity { get; set; }
    }
}