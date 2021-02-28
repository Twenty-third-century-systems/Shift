using System.Collections.Generic;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityArticlesOfAssociationResponseDto {
        public string TableOfArticles { get; set; }
        public List<TaskPrivateEntityAmendedArticleDto> AmendedArticles { get; set; }
    }
}