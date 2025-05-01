using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ThongTinLietSi.Models
{
    public class Role
    {
        public int Id { get; set; } // Changed from string to int
        public string Ten { get; set; } // Changed from string to int

        public ICollection<User> Users { get; set; } = new List<User>();


    }
}
