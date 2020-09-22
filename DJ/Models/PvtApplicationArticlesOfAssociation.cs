using System.Collections.Generic;

namespace DJ.Models {
    public class PvtApplicationArticlesOfAssociation {
        public ArticleTable ArticleTable { get; set; }
        public List<AmendedArticle> AmendedArticles { get; set; } = new List<AmendedArticle>();
    }
}