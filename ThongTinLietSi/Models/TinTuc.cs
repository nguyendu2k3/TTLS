using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ThongTinLietSi.Models
{
    public class TinTuc
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Content { get; set; } 
        public string? img { get; set; } 


    }
}
