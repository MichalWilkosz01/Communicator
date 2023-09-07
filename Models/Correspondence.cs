using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communicator.Models
{
    public class Correspondence
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("SenderId")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; } = null!;
        [ForeignKey("ReceiverId")]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; } = null!;
        public ICollection<Message>? Messages { get; set; } = new List<Message>();
    }
}
