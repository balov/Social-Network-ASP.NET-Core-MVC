using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SocialNetwork.Web.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.InvokeAsync("Send", message);
        }

        public void Join(string groupName)
        {
            Groups.AddAsync(Context.ConnectionId, groupName);
        }
    }
}