﻿using System.Collections.Generic;
using BarTender.Models;
using Dab.Models;

namespace BarTender.Dtos {
    public class PopulatedApplicationDetailDto {
        //Office details
        public Office Office { get; set; }
        //Memorandum of association
        //TODO: Create proper object
        public string LiabilityClause { get; set; }
        //TODO: Create proper object
        public string ShareClause { get; set; }
        //Objectives
        public List<SingleObjective> Objectives { get; set; } = new List<SingleObjective>();
        //Articles of association
        public string TableOfArticles { get; set; }
        //Amended Article
        public List<AmendedArticle> AmendedArticles { get; set; } = new List<AmendedArticle>();    
        //Subscribers to go here
        public List<ShareHoldingMember> Members { get; set; } = new List<ShareHoldingMember>();
        //TODO: Create proper object
        
    }
}