using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cabinet.Dtos.Response;

namespace TurnTable.ExternalServices {
    public interface IValuesService {
        public Task<ExternalUserDashboardRequestDto> GetUserDashBoardValuesAsync(Guid userId);
        public Task<List<SelectionValueResponseDto>> GetSortingOffices();
    }
}