namespace Communicator.Models
{
    public class OverviewPageViewModel
    {
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public Dictionary<string, bool> FriendshipStatus { get; set; } = new Dictionary<string, bool>();

    }
}
