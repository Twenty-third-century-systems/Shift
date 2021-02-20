using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewArticleOfAssociationRequestDto {
        public int Type { get; set; }
        public List<NewAmendedArticleRequestDto> AmendedArticles { get; set; }
    }
}