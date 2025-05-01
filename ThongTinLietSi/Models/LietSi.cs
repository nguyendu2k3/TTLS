using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ThongTinLietSi.Models
{
    public class LietSi
    {
        public int Id { get; set; } 
        public string HoTen { get; set; } 
        public int NamSinh { get; set; }
        public int NamHySinh { get; set; }

        public string QueQuanTinhThanh { get; set; }
        public string QueQuanQuanHuyen { get; set; }
        public string QueQuanXaPhuong { get; set; }
        public string NguyenQuanChiTiet { get; set; } 
        public string DonVi {  get; set; }

        public int? IDNghiaTrang { get; set; }
        public NghiaTrang NghiaTrang { get; set; }

        public string LoMo { get; set; } // Changed from string to int
        public string KhuMo { get; set; } // Changed from string to int
        public string Map { get; set; } // Changed from string to int


    }
}
