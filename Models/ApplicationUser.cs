using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Communicator.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Nick { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
       
        public ICollection<ApplicationUser>? Friends { get; set; }
    }
}
