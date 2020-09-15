using System.Collections.Generic;
using Dab.Models;

namespace Dab.Dtos {
    public class AmmendedArticleDto {        
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<AmendedArticle> AmendedArticles { get; set; }
    }
}