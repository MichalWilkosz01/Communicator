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
        public async Task SendMessage(string receiver, string messageContent)
        {
            var sender = Context.User.Identity.Name;

            await Clients.User(receiver).SendAsync("ReceiveMessage", sender, messageContent);
            await Clients.Caller.SendAsync("ReceiveMessage", sender, messageContent);
        }
        public async Task SendMessageToGroup(string sender, string receiver, string messageContent)
        {
            
            await Clients.Group(receiver).SendAsync("ReceiveMessage", sender, messageContent);
            
            //return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, messageContent);
        }

    }
}
