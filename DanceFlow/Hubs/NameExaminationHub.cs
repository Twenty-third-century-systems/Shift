using Cabinet.Dtos.Internal.Request;
using Drinkers.InternalApiClients.NameSearch;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace DanceFlow.Hubs {
    public class NameExaminationHub : Hub {
        private readonly INameSearchApiClientService _nameSearchApiClientService;

        public NameExaminationHub(INameSearchApiClientService nameSearchApiClientService)
        {
            _nameSearchApiClientService = nameSearchApiClientService;
        }

        public async Task UpdateName(ExaminedNameRequestDto dto)
        {
            if (await _nameSearchApiClientService.ChangeNameStatusAsync(dto.EntityNameId, dto.Status))
                await Clients.Caller.SendAsync("ReceiveExaminationUpdate", dto);
        }

        public async Task Finish(int nameSearchId)
        {
            if(await _nameSearchApiClientService.FinishNameSearchExaminationAsync(nameSearchId))
                await Clients.Caller.SendAsync("ReceiveApplicationUpdate", nameSearchId);
        }
    }
}