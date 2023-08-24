using Communicator.Data;
using Communicator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace Communicator.Controllers
{
    public class ProfileController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
       
        public ProfileController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser()
        {
            
            var id = _userManager.GetUserId(this.User);
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);

            var editModel = new EditUserViewModel
                {
                    Id = user.Id,
                    Nick = user.Nick,
                    LastName = user.LastName,
                    City = user.City,
                    Country = user.Country,
                    PhoneNumber = user.PhoneNumber,
                    Age = user.Age,
                    Name = user.Name
                };
            return View(editModel);
        }
        
    }
}
