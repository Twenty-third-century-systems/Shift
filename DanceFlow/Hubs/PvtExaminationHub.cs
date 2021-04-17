﻿using System.Net.Http;
using System.Threading.Tasks;
using Drinkers.ExternalClients.PrivateEntity;
using Drinkers.InternalClients.PvtEntity;
using Microsoft.AspNetCore.SignalR;

namespace DanceFlow.Hubs {
    public class PvtExaminationHub : Hub {
        private readonly IPvtEntityApiClientService _privateEntityApiClientService;

        public PvtExaminationHub(IPvtEntityApiClientService privateEntityApiClientService)
        {
            _privateEntityApiClientService = privateEntityApiClientService;
        }

        public async Task Finish(int applicationId)
        {
            if (await _privateEntityApiClientService.FinishAsync(applicationId))
                await Clients.Caller.SendAsync("ReceivePvtExaminationUpdate", applicationId);
        }
    }
}