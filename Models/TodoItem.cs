using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Todolistapplication.Models
{
    public enum Status
    {
         NotStarted, 
         OnGoing, 
         Completed
    }
    
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        [Required]
        public string ItemName { get; set; }
        
        public string ItemDescription { get; set; }
        
        public DateTime ItemCreated { get; set; }
        
        public DateTime ItemUpdated { get; set; }
        public Status Status { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        
        public int UserId { get; set; }

     
    }
}
