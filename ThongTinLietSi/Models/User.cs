using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ThongTinLietSi.Models
{
    public class User
    {
        public int Id { get; set; } // Changed from string to int
        public string Ten { get; set; } // Changed from string to int
        public string DiaChi { get; set; } // Changed from string to int
        public string Email { get; set; } // Changed from string to int
        public string SDT { get; set; } // Changed from string to int
        public string TaiKhoan { get; set; } // Changed from string to int
        public string MatKhau { get; set; } // Changed from string to int

        public int? IDRole { get; set; }
        public Role Role { get; set; }

    }
}
