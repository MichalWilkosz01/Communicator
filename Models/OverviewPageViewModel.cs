namespace Communicator.Models
{
    public class OverviewPageViewModel
    {
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public List<string> UserFriendIds { get; set; } = new List<string>();
    }
}
