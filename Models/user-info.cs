using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Todolistapplication.Models
{
    public class user_info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        [Required]
        public string? Email { get; set; }

        [StringLength(50)]
        [Required]
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    


    }
}
