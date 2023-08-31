using Communicator.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Communicator.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public async Task SendDirectMessage(string receiver, string messageContent)
        {
            var senderId = _userManager.GetUserId(Context.User);
            var sender = _userManager.Users.FirstOrDefault(u => u.Id == senderId);
            await Clients.Caller.SendAsync("ReceiveMessage", sender, messageContent);
            await Clients.Group(receiver).SendAsync("ReceiveMessage", sender, messageContent);
            


        }

    }
}
