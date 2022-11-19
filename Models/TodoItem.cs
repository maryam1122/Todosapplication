using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Todolistapplication.Models
{   
  
    public enum status
    {
         NotStarted, OnGoing, Completed
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
        public User user { get; set; }
        public int UserId { get; set; }

        public status status { get; set; }
    }
}
