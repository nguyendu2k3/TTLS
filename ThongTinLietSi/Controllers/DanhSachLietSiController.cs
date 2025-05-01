using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;
using ThongTinLietSi.Models;

namespace ThongTinLietSi.Controllers
{
    public class DanhSachLietSiController : Controller
    {
        private readonly ILogger<DanhSachLietSiController> _logger;
        private readonly ApplicationDbContext _context;
        public DanhSachLietSiController(ILogger<DanhSachLietSiController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.NghiaTrangs = new SelectList(_context.NghiaTrangs, "Id", "TenNghiaTrang");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LienHe()
        {
            return View();
        }

        public IActionResult DanhSachLietSi()
        {
            return View();
        }

        public async Task<IActionResult> TinTuc()
        {
            var tinTucList = await _context.TinTucs.ToListAsync();

            return View(tinTucList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string taiKhoan, string matKhau)
        {
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                ModelState.AddModelError(string.Empty, "Tên tài khoản và mật khẩu không được để trống.");
                return View();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.TaiKhoan == taiKhoan && u.MatKhau == matKhau);

            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role.Ten);

                if (user.Role.Ten == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (user.Role.Ten == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Tên tài khoản hoặc mật khẩu không đúng.");
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult TraCuu()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Search(LietSiSearchViewModel searchModel)
        {
            var results = _context.LietSis.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.HoTen))
            {
                results = results.Where(l => l.HoTen.Contains(searchModel.HoTen));
            }

            if (searchModel.NamSinh > 0)
            {
                results = results.Where(l => l.NamSinh == searchModel.NamSinh);
            }

            if (searchModel.NamHySinh > 0)
            {
                results = results.Where(l => l.NamHySinh == searchModel.NamHySinh);
            }
            if (!string.IsNullOrEmpty(searchModel.DonVi))
            {
                results = results.Where(l => l.DonVi == searchModel.DonVi);
            }

            if (!string.IsNullOrEmpty(searchModel.QueQuanTinhThanh))
            {
                results = results.Where(l => l.QueQuanTinhThanh.Contains(searchModel.QueQuanTinhThanh));
            }

            if (!string.IsNullOrEmpty(searchModel.QueQuanQuanHuyen))
            {
                results = results.Where(l => l.QueQuanQuanHuyen.Contains(searchModel.QueQuanQuanHuyen));
            }

            if (!string.IsNullOrEmpty(searchModel.QueQuanXaPhuong))
            {
                results = results.Where(l => l.QueQuanXaPhuong.Contains(searchModel.QueQuanXaPhuong));
            }


            if (searchModel.IDNghiaTrang > 0)
            {
                results = results.Where(l => l.IDNghiaTrang == searchModel.IDNghiaTrang);
            }

            var listResults = results.ToList();

            HttpContext.Session.SetObjectAsJson("SearchResults", listResults);

            ViewBag.NghiaTrangs = new SelectList(_context.NghiaTrangs, "Id", "TenNghiaTrang");

            return View("Index", listResults);
        }

        public IActionResult TinTucDetail(int id)
        {
            // Retrieve the main news article by ID
            var tinTuc = _context.TinTucs.FirstOrDefault(t => t.Id == id);

            if (tinTuc == null)
            {
                return NotFound();
            }

            var relatedArticles = _context.TinTucs
                                          .Take(5)
                                          .ToList();

            // Create a view model to pass the main article and related articles to the view
            var viewModel = new TinTucDetailViewModel
            {
                TinTuc = tinTuc,
                RelatedArticles = relatedArticles
            };

            return View(viewModel);
        }




        public IActionResult ExportExcel()
        {
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Get the list of LietSi that were filtered during the search.
            var listResults = HttpContext.Session.GetObjectFromJson<List<LietSi>>("SearchResults");

            if (listResults == null || !listResults.Any())
            {
                // If no data found in the session or if the list is empty
                return RedirectToAction("Index", "Home");
            }

            // Create an Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Liệt sĩ");

                // Add headers
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Họ tên";
                worksheet.Cells[1, 3].Value = "Năm sinh";
                worksheet.Cells[1, 4].Value = "Năm hy sinh";
                worksheet.Cells[1, 5].Value = "Nguyên quán";
                worksheet.Cells[1, 6].Value = "Nghĩa trang";
                worksheet.Cells[1, 7].Value = "Tỉnh thành";
                worksheet.Cells[1, 8].Value = "Quận huyện";
                worksheet.Cells[1, 9].Value = "Phường xã";

                // Fill in the data
                for (int i = 0; i < listResults.Count; i++)
                {
                    var item = listResults[i];

                    worksheet.Cells[i + 2, 1].Value = i + 1; // Index as STT
                    worksheet.Cells[i + 2, 2].Value = item.HoTen;
                    worksheet.Cells[i + 2, 3].Value = item.NamSinh;
                    worksheet.Cells[i + 2, 4].Value = item.NamHySinh;
                    worksheet.Cells[i + 2, 5].Value = item.QueQuanTinhThanh;
                    worksheet.Cells[i + 2, 6].Value = item.NghiaTrang?.TenNghiaTrang;
                    worksheet.Cells[i + 2, 7].Value = item.QueQuanTinhThanh;
                    worksheet.Cells[i + 2, 8].Value = item.QueQuanQuanHuyen;
                    worksheet.Cells[i + 2, 9].Value = item.QueQuanXaPhuong;
                }

                // Save the Excel package to a memory stream
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                // Return the file for download
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "liet_si.xlsx");
            }
        }
    }   
}
