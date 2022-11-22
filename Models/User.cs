using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Todolistapplication.SecureUtility;

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
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        [MinLength(8)]
        [NotMapped]
        public string Password
        {
            set => PasswordHash = value.GetPasswordHash();
        }
        
        public byte[] PasswordHash { get; private set; }
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public ICollection<TodoItem>? TodoItems { get; set; }
    }
}
