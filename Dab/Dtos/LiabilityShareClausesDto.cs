﻿using Dab.Models;

namespace Dab.Dtos {
    public class LiabilityShareClausesDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public LiabilityShareClauses Clauses { get; set; }
    }
}