using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communicator.Models
{
    public class Friendship
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        [ForeignKey("FriendId")]
        public string FriendId { get; set; }
        public ApplicationUser Friend { get; set; } = null!;
    }
}
