using Microsoft.AspNetCore.SignalR;

namespace WebBlog.WebSocket.Hubs
{
    public class PostHub : Hub
    {
        public async Task SendPostNotification(string message)
        {
            await Clients.All.SendAsync("ReceivePostNotification", message);
        }
    }
}
