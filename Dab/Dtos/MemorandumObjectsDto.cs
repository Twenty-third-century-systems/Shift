using System.Collections.Generic;
using Dab.Models;

namespace Dab.Dtos {
    public class MemorandumObjectsDto {        
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<SingleObjective> Objects { get; set; }
    }
}