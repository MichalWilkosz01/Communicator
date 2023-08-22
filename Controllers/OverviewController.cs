using Communicator.Data;
using Communicator.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Communicator.Controllers
{
    public class OverviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public OverviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search(string searchingText)
        {
            OverviewPageViewModel overviewPageViewModel = new OverviewPageViewModel();
            List<ApplicationUser> users = _context.Users.Where(x => x.Name.Contains(searchingText) || x.LastName.Contains(searchingText) || x.Nick.Contains(searchingText)).ToList();
            overviewPageViewModel.Users = users;

            TempData["SearchResults"] = JsonConvert.SerializeObject(users);

            return View("Index", overviewPageViewModel);
        }


        [HttpPost]
        [ActionName("AddFriend")]

        public IActionResult AddFriend(string userId)
        {
            var usersJson = TempData["SearchResults"] as string;

            var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(usersJson);
            var overviewPageViewModel = new OverviewPageViewModel();
            overviewPageViewModel.Users = users;
            TempData["SearchResults"] = JsonConvert.SerializeObject(users);

            return View("Index", overviewPageViewModel);
            
        }

    }
}
