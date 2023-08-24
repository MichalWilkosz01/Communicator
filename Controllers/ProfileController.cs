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
        public IActionResult EditUser()
        {
            
            var id =  _userManager.GetUserId(this.User);
            var user =  _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var editModel = new EditUserViewModel
                {
                    Id = user.Id,
                    Nick = user.Nick,
                    LastName = user.LastName,
                    City = user.City,
                    Country = user.Country,
                    PhoneNumber = user.PhoneNumber,
                    Age = user.Age,
                    Name = user.Name,
                    Email = user.Email
                };
            return View(editModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel userModel)
        {
             
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userModel.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = userModel.Name;
            user.LastName = userModel.LastName;
            user.Nick = userModel.Nick;
            user.City = userModel.City;
            user.Country = userModel.Country;
            user.Age = userModel.Age;
            user.PhoneNumber = userModel.PhoneNumber;
            user.Email = userModel.Email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
