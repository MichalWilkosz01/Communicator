namespace Communicator.Models
{
    public class HomePageViewModel
    {
        public List<ApplicationUser> Friends { get; set; } = new List<ApplicationUser>();
        public List<Correspondence> Correspondences { get; set; }  = new List<Correspondence> { };
    }
}
