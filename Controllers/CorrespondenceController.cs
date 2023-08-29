using Communicator.Data;
using Communicator.Models;
using Communicator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Communicator.Controllers
{
    public class CorrespondenceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CorrespondenceController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {

            _userManager = userManager;
            _context = context;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(string correspondentId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && correspondentId != null)
            {
                if (!_context.Correspondences.Any(c => (c.SenderId == user.Id && c.ReceiverId == correspondentId) || (c.SenderId == correspondentId && c.ReceiverId == user.Id)))
                {
                    var newCorrespondence = new Correspondence
                    {
                        Sender = user,
                        SenderId = user.Id,
                        ReceiverId = correspondentId,
                        Receiver = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == correspondentId)

                    };
                    _context.Correspondences.Add(newCorrespondence);
                    _context.SaveChanges();
                    return View(newCorrespondence);
                }
                else
                {
                    var correspondence = await _context.Correspondences.Include(s => s.Sender).Include(r => r.Receiver).Include(m => m.Messages).FirstOrDefaultAsync(c => (c.SenderId == user.Id && c.ReceiverId == correspondentId) || (c.SenderId == correspondentId && c.ReceiverId == user.Id));
                    return View(correspondence);
                }

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveMessage(string content, int correspondenceId)
        {
            var correspondence = await _context.Correspondences.FirstOrDefaultAsync(c => c.Id == correspondenceId);
            if (correspondence != null)
            {
                var userId = _userManager.GetUserId(User);
                if (correspondence.Messages != null && userId !=null)
                {
                   

                    correspondence.Messages.Add(new Models.Message
                        {
                            Content = content,
                            SenderId = userId,
                            Sender = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId)
                        }
                        );
                        await _context.SaveChangesAsync();
                    return Ok();
                } 
                
            }
            return NotFound();
        }


    }
}
