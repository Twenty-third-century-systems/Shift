using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewAmendedArticlesRequestDto {
        public int ApplicationId { get; set; }        
        public List<NewAmendedArticleRequestDto> AmendedArticles { get; set; }
    }
}