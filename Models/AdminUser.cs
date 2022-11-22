using System.ComponentModel.DataAnnotations;

namespace OwnerApp.Models
{
    public class AdminUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public AdminUser() { }
        public AdminUser(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
