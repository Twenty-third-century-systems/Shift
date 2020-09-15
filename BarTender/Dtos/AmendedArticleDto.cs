using System.Collections.Generic;
using BarTender.Models;

namespace BarTender.Dtos {
    public class AmendedArticleDto {        
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public List<AmendedArticle> AmendedArticles { get; set; }
    }
}