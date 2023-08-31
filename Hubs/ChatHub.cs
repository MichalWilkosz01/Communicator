using Microsoft.AspNetCore.SignalR;

namespace Communicator.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public async Task SendDirectMessage(string sender, string receiver, string messageContent)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", sender, messageContent);
            await Clients.Group(receiver).SendAsync("ReceiveMessage", sender, messageContent);


        }

    }
}
