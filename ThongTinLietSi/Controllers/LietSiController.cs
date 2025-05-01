using Microsoft.AspNetCore.Mvc;
using ThongTinLietSi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThongTinLietSi.Controllers
{
    public class LietSiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LietSiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LietSi/Index
        public IActionResult Index()
        {
            var lietSis = _context.LietSis.Include(l => l.NghiaTrang).ToList();
            return View(lietSis);
        }


        public IActionResult Create()
        {
            var nghiaTrangs = _context.NghiaTrangs.ToList();
            ViewBag.NghiaTrangs = new SelectList(nghiaTrangs, "Id", "TenNghiaTrang");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string HoTen, int NamSinh, int NamHySinh, string QueQuanTinhThanh, string QueQuanQuanHuyen, string QueQuanXaPhuong, string NguyenQuanChiTiet,string DonVi, string LoMo, string KhuMo, int IDNghiaTrang, string Map)
        {
            if (ModelState.IsValid)
            {
                var lietSi = new LietSi
                {
                    HoTen = HoTen,
                    NamSinh = NamSinh,
                    NamHySinh = NamHySinh,
                    QueQuanTinhThanh = QueQuanTinhThanh,
                    QueQuanQuanHuyen = QueQuanQuanHuyen,
                    QueQuanXaPhuong = QueQuanXaPhuong,
                    NguyenQuanChiTiet = NguyenQuanChiTiet,
                    DonVi = DonVi,
                    LoMo = LoMo,
                    KhuMo = KhuMo,
                    IDNghiaTrang = IDNghiaTrang,
                    Map = Map
                };

                _context.Add(lietSi);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, return the view with the model
            return View();
        }




        // GET: LietSi/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lietSi = _context.LietSis
                                  .Include(sp => sp.NghiaTrang)
                                  .FirstOrDefault(sp => sp.Id == id);
            if (lietSi == null)
            {
                return NotFound();
            }
            ViewBag.NghiaTrangs = new SelectList(_context.NghiaTrangs, "Id", "TenNghiaTrang", lietSi.IDNghiaTrang);


            return View(lietSi);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, string HoTen, int NamSinh, int NamHySinh, string NguyenQuanChiTiet,string DonVi, string LoMo, string KhuMo, string Map, string QueQuanTinhThanh, string QueQuanQuanHuyen, string QueQuanXaPhuong, int IDNghiaTrang)
        {

            var ls = _context.LietSis.Find(Id);
            if (ls == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var lietSi = _context.LietSis.Find(Id);
                    if (lietSi == null)
                    {
                        return NotFound();
                    }

                    lietSi.HoTen = HoTen;
                    lietSi.NamSinh = NamSinh;
                    lietSi.NamHySinh = NamHySinh;
                    lietSi.NguyenQuanChiTiet = NguyenQuanChiTiet;
                    lietSi.DonVi = DonVi;
                    lietSi.LoMo = LoMo;
                    lietSi.KhuMo = KhuMo;
                    lietSi.Map = Map;
                    lietSi.QueQuanTinhThanh = QueQuanTinhThanh;
                    lietSi.QueQuanQuanHuyen = QueQuanQuanHuyen;
                    lietSi.QueQuanXaPhuong = QueQuanXaPhuong;
                    lietSi.IDNghiaTrang = IDNghiaTrang;

                    _context.Update(lietSi);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.LietSis.Any(e => e.Id == Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: LietSi/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lietSi = _context.LietSis
                .FirstOrDefault(m => m.Id == id);
            if (lietSi == null)
            {
                return NotFound();
            }

            return View(lietSi);
        }

        // POST: LietSi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var lietSi = _context.LietSis.Find(id);
            _context.LietSis.Remove(lietSi);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
