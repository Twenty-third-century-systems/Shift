using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class AmendedArticle
    {
        public int AmendedArticleId { get; set; }
        public string Value { get; set; }
        public int ArticleId { get; set; }

        public ArticleOfAssociation ArticleOfAssociation { get; set; }
    }
}
