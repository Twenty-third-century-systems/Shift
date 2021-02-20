using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.External.Response;

namespace TurnTable.ExternalServices {
    public interface IValueService {
        public Task<ExternalUserDashboardRequestDto> GetUserDashBoardValuesAsync(Guid user);
        public Task<List<SelectionValueResponseDto>> GetSortingOfficesAsync();
    }
}