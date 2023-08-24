
using System.ComponentModel.DataAnnotations;

namespace Communicator.Models
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]     
        public string Nick { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
