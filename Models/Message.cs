using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communicator.Models
{
    public class Message
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime SendingTime { get; set; } = DateTime.Now;
        [ForeignKey("SenderId")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; } = null!;


    }
}
