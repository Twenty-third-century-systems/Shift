using System.Collections.Generic;

namespace Cabinet.Dtos.External.Response {
    public class NameSearchSelectionValuesResponseDto {
        public List<SelectionValueResponseDto> ReasonsForSearch { get; set; }
        public List<SelectionValueResponseDto> TypesOfEntities { get; set; }
        public List<SelectionValueResponseDto> Designations { get; set; }
        public List<SelectionValueResponseDto> SortingOffices { get; set; }
    }
}