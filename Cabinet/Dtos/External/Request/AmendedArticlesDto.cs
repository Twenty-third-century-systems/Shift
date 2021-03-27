using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class AmendedArticlesDto {        
        public int ApplicationId { get; set; }
        public List<AmendedArticleDto> AmendedArticles { get; set; }
    }
}