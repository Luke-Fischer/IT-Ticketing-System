using System.ComponentModel.DataAnnotations;

namespace IT_Ticketing_System.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
  
        public string Message { get; set; }

        [Required]
        public string Priority { get; set; }
        [Required]
        public string Status { get; set; }

        public DateTime timeSubmitted { get; set; }
    }
}
