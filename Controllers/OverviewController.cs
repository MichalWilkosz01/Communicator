using Communicator.Data;
using Communicator.Models;
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
        public OverviewController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Search(string searchingText)
        {
            var id = _userManager.GetUserId(this.User);
            OverviewPageViewModel overviewPageViewModel = new OverviewPageViewModel();
            List<ApplicationUser> users = _context.Users.Where(x => x.Name.Contains(searchingText) || x.LastName.Contains(searchingText) || x.Nick.Contains(searchingText)).Where(u=>u.Id != id).ToList();
            overviewPageViewModel.Users = users;

            TempData["SearchResults"] = JsonConvert.SerializeObject(users);

            return View("Index", overviewPageViewModel);
        }


        [HttpPost]
        [Authorize]

        public IActionResult AddFriend(string userId)
        {
            var id = _userManager.GetUserId(this.User);
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            var friend = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null && friend != null)
            {
                var friendship = new Friendship
                {
                    User = user,
                    UserId = user.Id,
                    Friend = friend,
                    FriendId = friend.Id

                };
                _context.Add(friendship);
            }
            _context.SaveChanges();
            var usersJson = TempData["SearchResults"] as string;

            var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(usersJson);
            var overviewPageViewModel = new OverviewPageViewModel();
            overviewPageViewModel.Users = users;
            TempData["SearchResults"] = usersJson;

            return View("Index", overviewPageViewModel);
            
        }

    }
}
