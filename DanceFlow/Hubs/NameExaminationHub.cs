using DanceFlow.Dtos;
using Drinkers.InternalClients.NameSearch;
using Drinkers.InternalClients.Task;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace DanceFlow.Hubs {
    public class NameExaminationHub : Hub {
        private readonly ITaskApiClientService _taskApiClientService;
        private readonly INameSearchApiService _nameSearchApiService;

        public NameExaminationHub(ITaskApiClientService taskApiClientService,INameSearchApiService nameSearchApiService)
        {
            _taskApiClientService = taskApiClientService;
            _nameSearchApiService = nameSearchApiService;
        }

        public async Task UpdateName(NameExaminedFromExaminerDto name)
        {
            if (await _nameSearchApiService.ChangeNameStatusAsync(name.EntityNameId, name.Status))
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
            if(await _nameSearchApiService.FinishNameSearchExaminationAsync(nameSearchId))
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