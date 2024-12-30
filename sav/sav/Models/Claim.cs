using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sav.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        
        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        
        public string Status { get; set; }

        public DateTime Date { get; set; }

        // Navigation properties
        public virtual Client Client { get; set; }
        public virtual Article Article { get; set; }
    }
}
