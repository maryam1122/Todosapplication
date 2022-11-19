﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Todolistapplication.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(50)]
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public ICollection<TodoItem>? TodoItems { get; set; }
    }
}