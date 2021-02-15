namespace Fridge.Models {
    public class MemorandumOfAssociationObject {        
        public int MemorandumOfAssociationObjectId { get; set; }
        public int MemorandumId { get; set; }
        public string Value { get; set; }

        public MemorandumOfAssociation Memorandum { get; set; }
    }
}