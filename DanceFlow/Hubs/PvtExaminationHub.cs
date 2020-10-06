using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DanceFlow.Hubs {
    public class PvtExaminationHub : Hub {
        public async Task Finish(int applicationId)
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.PatchAsync($"{ApiUrls.FinishPvtApplicationExamination}/{applicationId}", null);
                if (response.IsSuccessStatusCode)
                {
                    await Clients.Caller.SendAsync("ReceivePvtExaminationUpdate", applicationId);
                }
            }
        }
    }
}