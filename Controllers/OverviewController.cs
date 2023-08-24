using Communicator.Data;
using Communicator.Models;
using Communicator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Communicator.Controllers
{
    public class OverviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FriendshipService _friendshipService;
        public OverviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, FriendshipService friendshipService = null)
        {
            _context = context;
            _userManager = userManager;
            _friendshipService = friendshipService;
        }

        [HttpGet]
        public IActionResult Search(string searchingText)
        {
            var id = _userManager.GetUserId(this.User);
            OverviewPageViewModel overviewPageViewModel = new OverviewPageViewModel();
            List<ApplicationUser> users = _context.Users.Where(x => x.Name.Contains(searchingText) || x.LastName.Contains(searchingText) || x.Nick.Contains(searchingText)).Where(u=>u.Id != id).ToList();          
            var userFriendIds = _context.Friendships.Where(u => u.UserId == id).Select(f => f.FriendId).ToList();
            overviewPageViewModel.Users = users;
            overviewPageViewModel.UserFriendIds = userFriendIds;
            
            
           
            TempData["SearchResults"] = JsonConvert.SerializeObject(users);

            return View("Index", overviewPageViewModel);
        }


        [HttpPost]
        [Authorize]

        public IActionResult AddFriend(string userId)
        {
            var id = _userManager.GetUserId(this.User);
            _friendshipService.AddFriend(id, userId);
            
            var userFriendIds = _context.Friendships.Where(u => u.UserId == id).Select(f => f.FriendId).ToList();
            var usersJson = TempData["SearchResults"] as string;

            var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(usersJson);
            var overviewPageViewModel = new OverviewPageViewModel();
            overviewPageViewModel.UserFriendIds = userFriendIds;
            overviewPageViewModel.Users = users;
            TempData["SearchResults"] = usersJson;

            return View("Index", overviewPageViewModel);
            
        }

    }
}
