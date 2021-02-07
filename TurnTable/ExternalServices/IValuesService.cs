using System;
using System.Threading.Tasks;
using Cabinet.Dtos;
using Cabinet.Dtos.Response;

namespace TurnTable.ExternalServices {
    public interface IValuesService {
        public Task<ExternalUserDashboardRequestDto> GetUserDashBoardValuesAsync(Guid userId);

        public NameSearchSelectionValuesResponseDto GetNameSearchValuesForSelection();
    }
}