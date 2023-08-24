using Communicator.Controllers;
using Communicator.Data;
using Communicator.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Communicator.Services
{
    public class FriendshipService
    {
       
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger<HomeController> _logger;
            private readonly ApplicationDbContext _context;

            public FriendshipService(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
            {
                _logger = logger;
                _userManager = userManager;
                _context = context;
            }
            public void DeleteFriend(string userId, string friendId)
            {
              
                if (userId != null)
                {
                    var friend = _context.Friendships.Where(u => u.UserId == userId).Where(f => f.FriendId == friendId).FirstOrDefault();
                    if (friend != null)
                    {
                        _context.Remove(friend);
                        _context.SaveChanges();


                    }

                }


            }
        public void AddFriend(string userId, string friendId)
        {
            if(userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
                var friend = _userManager.Users.FirstOrDefault(u => u.Id == friendId);
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
            }
        }
        }
    }

