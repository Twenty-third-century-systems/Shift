using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewArticleOfAssociationRequestDto {
        public NewArticleOfAssociationRequestDto()
        {
            AmendedArticles = new List<NewAmendedArticleRequestDto>();
        }
        public int ApplicationId { get; set; }
        // Send null and List of Amended articles for OTHER
        public int? TableOfArticles { get; set; }
        public List<NewAmendedArticleRequestDto> AmendedArticles { get; set; }

        public bool IsAnAmendedArticle()
        {
            return TableOfArticles.Equals(null) && AmendedArticles.Count > 0;
        }
    }
}