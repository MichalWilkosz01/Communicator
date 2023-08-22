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


        [HttpGet]
        public IActionResult Index()
        {
            OverviewPageViewModel overviewPageViewModel = new OverviewPageViewModel();

            // Tutaj możesz zainicjować model lub przeprowadzić inne operacje, które są potrzebne

            return View(overviewPageViewModel); // Przekazujemy model widoku do widoku
        }


        [HttpPost]
        [ActionName("AddFriend")]
        [ValidateAntiForgeryToken]

        public IActionResult AddFriend(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                // Obsłuż błąd braku userId
                return RedirectToAction("Search"); // Przekieruj na widok Index
            }

            // Tutaj dodaj kod do logiki dodawania użytkownika o podanym userId do listy przyjaciół zalogowanego użytkownika.
            // Możesz użyć _context, aby uzyskać dostęp do bazy danych.

            // Przykład przekierowania na stronę Index po pomyślnym dodaniu przyjaciela:
            
            return RedirectToAction("Search");
        }

    }
}
