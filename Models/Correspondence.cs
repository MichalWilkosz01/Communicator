using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communicator.Models
{
    public class Correspondence
    {
        [Required]
        public int Id { get; set; }
        public DateTime SendingTime { get; set; } = DateTime.Now;
        [Required]
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        [ValidateNever]
        public ApplicationUser Sender { get; set; }
        public int ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        [ValidateNever]
        public ApplicationUser Receiver { get; set; }
    }
}
