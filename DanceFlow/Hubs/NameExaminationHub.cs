using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using DanceFlow.Client;
using DanceFlow.Dtos;
using DanceFlow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace DanceFlow.Hubs {
    public class NameExaminationHub : Hub {
        private readonly IApiClientService _apiClientService;

        public NameExaminationHub(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task UpdateName(NameExaminedFromExaminerDto name)
        {
            if (await _apiClientService.ChangeNameStatusAsync(name.EntityNameId, name.Status))
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
            if(await _apiClientService.FinishNameSearchExaminationAsync(nameSearchId))
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