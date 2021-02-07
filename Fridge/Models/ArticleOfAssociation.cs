using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class ArticleOfAssociation
    {
        public ArticleOfAssociation()
        {
            AmendedArticles = new HashSet<AmendedArticle>();
            PvtEntities = new HashSet<PrivateEntity>();
        }

        public int ArticleOfAssociationId { get; set; }
        public bool TableA { get; set; }
        public bool TableB { get; set; }
        public bool Other { get; set; }

        public ICollection<AmendedArticle> AmendedArticles { get; set; }
        public ICollection<PrivateEntity> PvtEntities { get; set; }
    }
}
