using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThongTinLietSi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ThongTinLietSi.Controllers
{
    public class NghiaTrangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NghiaTrangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NghiaTrang
        public async Task<IActionResult> Index()
        {
            var nghiaTrangs = await _context.NghiaTrangs.ToListAsync();
            return View(nghiaTrangs);
        }

        // GET: NghiaTrang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NghiaTrang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TinhThanh,QuanHuyen,XaPhuong,TenNghiaTrang")] NghiaTrang nghiaTrang)
        {
            if (ModelState.IsValid)
            {
                nghiaTrang.TinhThanh = nghiaTrang.TinhThanh;
                nghiaTrang.QuanHuyen = nghiaTrang.QuanHuyen;
                nghiaTrang.XaPhuong = nghiaTrang.XaPhuong;

                _context.Add(nghiaTrang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nghiaTrang);
        }



        // GET: NghiaTrang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nghiaTrang = await _context.NghiaTrangs.FindAsync(id);
            if (nghiaTrang == null)
            {
                return NotFound();
            }
            return View(nghiaTrang);
        }

        // POST: NghiaTrang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TinhThanh,QuanHuyen,XaPhuong,TenNghiaTrang")] NghiaTrang nghiaTrang)
        {
            if (id != nghiaTrang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nghiaTrang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NghiaTrangExists(nghiaTrang.Id))
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
            return View(nghiaTrang);
        }

        // GET: NghiaTrang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nghiaTrang = await _context.NghiaTrangs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nghiaTrang == null)
            {
                return NotFound();
            }

            return View(nghiaTrang);
        }

        // POST: NghiaTrang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nghiaTrang = await _context.NghiaTrangs.FindAsync(id);
            _context.NghiaTrangs.Remove(nghiaTrang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NghiaTrangExists(int id)
        {
            return _context.NghiaTrangs.Any(e => e.Id == id);
        }
    }
}
