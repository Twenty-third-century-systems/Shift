using DanceFlow.Dtos;
using Drinkers.InternalClients.NameSearch;
using Drinkers.InternalClients.Task;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace DanceFlow.Hubs {
    public class NameExaminationHub : Hub {
        private readonly ITaskApiClientService _taskApiClientService;
        private readonly INameSearchApiClientService _nameSearchApiClientService;

        public NameExaminationHub(ITaskApiClientService taskApiClientService,INameSearchApiClientService nameSearchApiClientService)
        {
            _taskApiClientService = taskApiClientService;
            _nameSearchApiClientService = nameSearchApiClientService;
        }

        public async Task UpdateName(NameExaminedFromExaminerDto name)
        {
            if (await _nameSearchApiClientService.ChangeNameStatusAsync(name.EntityNameId, name.Status))
                await Clients.Caller.SendAsync("ReceiveExaminationUpdate", name);
            
            
            // using (var client = new HttpClient())
            // {                
            //     var response = await client.PatchAsync($"{ApiUrls.ExamineName}/{name.Id}/{name.Status}", null);
            //     if(response.IsSuccessStatusCode)
            //     {                    
            //         await Clients.Caller.SendAsync("ReceiveExaminationUpdate", name);                
            //     }                
            // }
        }

        public async Task Finish(int nameSearchId)
        {
            if(await _nameSearchApiClientService.FinishNameSearchExaminationAsync(nameSearchId))
                await Clients.Caller.SendAsync("ReceiveApplicationUpdate", nameSearchId);
            // using (var client = new HttpClient())
            // {
            //     var response = await client.PatchAsync($"{ApiUrls.FinishNameSearchExamination}/{applicationId}", null);
            //     if (response.IsSuccessStatusCode)
            //     {
            //         await Clients.Caller.SendAsync("ReceiveApplicationUpdate", applicationId);
            //     }
            // }
        }
    }
}