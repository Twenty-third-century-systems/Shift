using System.Collections.Generic;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityResponseDto {

        public TaskPrivateEntityResponseDto()
        {
            Members = new List<TaskPrivateEntityShareHolderResponseDto>();
            LocalEntityShareHolders = new List<TaskPrivateEntityLocalEntityShareHoldersRequestDto>();
            ForeignEntityShareHolders = new List<TaskPrivateEntityForeignEntityShareHoldersDto>();
        }
        public string Name { get; set; }

        public TaskPrivateEntityOfficeResponseDto Office { get; set; }

        public TaskPrivateEntityArticlesOfAssociationResponseDto ArticlesOfAssociation { get; set; }

        public TaskPrivateEntityMemorandumResponseDto MemorandumOfAssociation { get; set; }

        public List<TaskPrivateEntityShareHolderResponseDto> Members { get; set; }

        public List<TaskPrivateEntityLocalEntityShareHoldersRequestDto> LocalEntityShareHolders { get; set; }

        public List<TaskPrivateEntityForeignEntityShareHoldersDto> ForeignEntityShareHolders { get; set; }
    }
}