using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThongTinLietSi.Models;

namespace ThongTinLietSi.Controllers
{
    public class TinTucController : Controller
    {
        private readonly ApplicationDbContext dbContext;
		private readonly IWebHostEnvironment _webHostEnvironment;


		public TinTucController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            dbContext = context;
			_webHostEnvironment = webHostEnvironment;
        }

        // GET: TinTuc
        public async Task<IActionResult> Index()
        {
            var tinTucs = await dbContext.TinTucs.ToListAsync();
            return View(tinTucs);
        }

        // GET: TinTuc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await dbContext.TinTucs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // GET: TinTuc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TinTuc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] TinTuc tinTuc, IFormFile img)
        {
            if (ModelState.IsValid)
            {
				string fileName = Path.GetFileNameWithoutExtension(img.FileName);
				string extension = Path.GetExtension(img.FileName);
				fileName = fileName + "_" + Guid.NewGuid().ToString() + extension;

				string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await img.CopyToAsync(stream);
				}

				tinTuc.img = fileName;

				dbContext.Add(tinTuc);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinTuc);
        }

        // GET: TinTuc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await dbContext.TinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            return View(tinTuc);
        }

        // POST: TinTuc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] TinTuc tinTuc, IFormFile img)
        {
            if (id != tinTuc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					string fileName = Path.GetFileNameWithoutExtension(img.FileName);
					string extension = Path.GetExtension(img.FileName);
					fileName = fileName + "_" + Guid.NewGuid().ToString() + extension;

					string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						await img.CopyToAsync(stream);
					}

					tinTuc.img = fileName;

					dbContext.Update(tinTuc);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.Id))
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
            return View(tinTuc);
        }

        // GET: TinTuc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await dbContext.TinTucs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // POST: TinTuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinTuc = await dbContext.TinTucs.FindAsync(id);
            dbContext.TinTucs.Remove(tinTuc);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(int id)
        {
            return dbContext.TinTucs.Any(e => e.Id == id);
        }
    }
}
