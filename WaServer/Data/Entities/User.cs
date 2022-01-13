using System.ComponentModel.DataAnnotations;

namespace WaServer.Data.Entities
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required, StringLength(255)]
        public string Email { get; set; }
        [Required, StringLength(500)]
        public string Password { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }

    }
}
