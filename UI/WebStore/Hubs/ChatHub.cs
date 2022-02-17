using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string Message) =>
            await Clients.Others.SendAsync("MessageFromClient", Message);
    }
}
