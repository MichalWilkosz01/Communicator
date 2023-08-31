using Microsoft.AspNetCore.SignalR;

namespace Communicator.Hubs
{
    public class ChatHub: Hub
    {
        public override  Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string messageContent)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, messageContent);
        }
        public Task SendMessageToGroup(string sender, string receiver, string messageContent)
        {
           return Clients.Group(receiver).SendAsync("ReceiveMessage",sender, messageContent);
        }

    }
}
