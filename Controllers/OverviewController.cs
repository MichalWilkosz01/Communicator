using Communicator.Data;
using Communicator.Models;
using Microsoft.AspNetCore.Mvc;

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

            return View("Index", overviewPageViewModel);
        }
    }
}
