using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communicator.Models
{
    public class Friendship
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }


        [Required]
        [ForeignKey("FriendId")]
        public string FriendId { get; set; }
        [ValidateNever]
        public ApplicationUser Friend { get; set; }
    }
}
