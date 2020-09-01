using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using DanceFlow.Dtos;
using DanceFlow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace DanceFlow.Hubs {
    public class NameExaminationHub: Hub {

        public async Task UpdateName(NameExaminedFromExaminerDto name)
        {
            using (var client = new HttpClient())
            {                
                var response = await client.PatchAsync($"{ApiUrls.ExamineName}/{name.Id}/{name.Status}", null);
                if(response.IsSuccessStatusCode)
                {                    
                    await Clients.Caller.SendAsync("ReceiveExaminationUpdate", name);                
                }                
            }
        }

        public async Task Finish(int applicationId)
        {
            using (var client = new HttpClient())
            {                
                var response = await client.PatchAsync($"{ApiUrls.FinishNameSearchExamination}/{applicationId}", null);
                if (response.IsSuccessStatusCode)
                {
                    await Clients.Caller.SendAsync("ReceiveApplicationUpdate", applicationId);
                }
            }
        }
    }
}