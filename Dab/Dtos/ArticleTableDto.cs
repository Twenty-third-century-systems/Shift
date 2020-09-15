using Dab.Models;

namespace Dab.Dtos {
    public class ArticleTableDto {
        public int ApplicationId { get; set; }
        public string PvtEntityId { get; set; }
        public ArticleTable Table { get; set; }
    }
}