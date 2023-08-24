using Microsoft.Build.Framework;

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
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
