using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ThongTinLietSi.Models
{
    public class NghiaTrang
    {
        public int Id { get; set; } // Changed from string to int
        public string TinhThanh { get; set; } // Changed from string to int
        public string QuanHuyen { get; set; } // Changed from string to int
        public string XaPhuong { get; set; } // Changed from string to int
        public string TenNghiaTrang { get; set; } // Changed from string to int
       
        public ICollection<LietSi> LietSis { get; set; } = new List<LietSi>();
    }
}
