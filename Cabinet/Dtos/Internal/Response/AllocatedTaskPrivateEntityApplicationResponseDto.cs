using System;
using System.Collections.Generic;
using Cabinet.Dtos.External.Response;

namespace Cabinet.Dtos.Internal.Response {
    public class AllocatedPrivateEntityTaskApplicationResponseDto {
        public SubmittedApplicationResponseDto Application { get; set; }

        public AllocatedPrivateEntityTaskApplicationResponseDto()
        {
            Members = new List<TaskPrivateEntityShareHolderResponseDto>();
            Directors = new List<TaskPrivateEntityPersonResponseDto>();
        }

        public string Name { get; set; }

        public string IndustrySector { get; set; }
        
        public TaskPrivateEntityOfficeResponseDto Office { get; set; }

        public TaskPrivateEntityArticlesOfAssociationResponseDto ArticlesOfAssociation { get; set; }

        public TaskPrivateEntityMemorandumResponseDto MemorandumOfAssociation { get; set; }

        public List<TaskPrivateEntityPersonResponseDto> Directors { get; set; }

        public TaskPrivateEntityPersonResponseDto Secretary { get; set; }

        public List<TaskPrivateEntityShareHolderResponseDto> Members { get; set; }
    }
}