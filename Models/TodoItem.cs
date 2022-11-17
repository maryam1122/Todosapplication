using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Todolistapplication.Models
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        [Required]
        public string item_Name { get; set; }
        public string item_Description { get; set; }
        public DateTime item_created { get; set; }
        public DateTime item_updated { get; set; }

        public user_info user { get; set; }

       





    }
}
