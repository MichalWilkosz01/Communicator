using Communicator.Data;
using Communicator.Models;
using Communicator.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace Communicator.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly FriendshipService _friendshipService;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context, FriendshipService friendshipService = null)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _friendshipService = friendshipService;
        }

        public IActionResult Index()
        {
            var id = _userManager.GetUserId(this.User);
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                ViewData["UserName"] = $"{user.Name} {user.LastName}";
            }
            var userFriends = _context.Friendships.Where(x => x.UserId == id).Select(f => f.Friend).ToList();
            return View(userFriends);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult DeleteFriend(string friendId)
        {
            var id = _userManager.GetUserId(this.User);
            if (id != null)
            {
                
                _friendshipService.DeleteFriend(id, friendId);


            }
            return RedirectToAction("Index", "Home");
        }
    }
}