


using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }

}

