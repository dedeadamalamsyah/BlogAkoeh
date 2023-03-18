using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAkoeh.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Username can't >12 character or <3 character")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string? Photo { get; set; }
    }
}
