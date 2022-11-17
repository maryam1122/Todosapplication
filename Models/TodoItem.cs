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
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime ItemCreated { get; set; }
        public DateTime ItemUpdated { get; set; }

        // Either declare this here or see TodoItem class. Both ways should work
        public UserInfo userInfo { get; set; }
    }
}
