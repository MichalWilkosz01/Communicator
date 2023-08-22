using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Communicator.Models
{
    public class Correspondence
    {
        [Required]
        public int Id { get; set; }      
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; } = null!;
        public string ReceiverId { get; set; } 
        public ApplicationUser Receiver { get; set; } = null!;
        public int MessageId { get; set; }
        public Message Message { get; set; } = null!;
    }
}
