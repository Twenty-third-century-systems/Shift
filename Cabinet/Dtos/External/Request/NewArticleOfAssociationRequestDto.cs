﻿using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewArticleOfAssociationRequestDto {
        public int ApplicationId { get; set; }
        public int? TableOfArticles { get; set; }
    }
}