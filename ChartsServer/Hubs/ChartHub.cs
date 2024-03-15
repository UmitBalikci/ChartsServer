using Microsoft.AspNetCore.SignalR;

namespace ChartsServer.Hubs
{
    public class ChartHub : Hub
    {
        public async Task SendMessageAsync()
        {
            await Clients.All.SendAsync("receiveMessage", "Hello");
        }
    }
}
