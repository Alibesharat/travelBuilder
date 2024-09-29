using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBuilder.DAL.DbContexts;
using TravelBuilder.DAL.Models;

namespace travelBuilder.Controllers
{
    [Route("[controller]")]
    public class TravelPackageController : Controller
    {
        private readonly ILogger<TravelPackageController> _logger;

        private readonly TravelDbContext _context;

        public TravelPackageController(TravelDbContext context, ILogger<TravelPackageController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: TravelPackage
        public async Task<IActionResult> Index()
        {
            var travelPackages = await _context.TravelPackages
                .Include(tp => tp.Itinerary) // Include itinerary information
                .ToListAsync();
            return View(travelPackages);
        }

        // GET: TravelPackage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackages
                .Include(tp => tp.Itinerary)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // GET: TravelPackage/Create
        public IActionResult Create()
        {
            // You might want to pass a list of itineraries for selection in the view
            ViewData["Itineraries"] = _context.Itineraries.ToList();
            return View();
        }

        // POST: TravelPackage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PackageName,Destination,Price,ItineraryId")] TravelPackage travelPackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackage);
        }

        // GET: TravelPackage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackages.FindAsync(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            ViewData["Itineraries"] = _context.Itineraries.ToList(); // For selecting a different itinerary in the edit view
            return View(travelPackage);
        }

        // POST: TravelPackage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PackageName,Destination,Price,ItineraryId")] TravelPackage travelPackage)
        {
            if (id != travelPackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackageExists(travelPackage.Id))
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
            return View(travelPackage);
        }

        // GET: TravelPackage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackages
                .Include(tp => tp.Itinerary)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelPackage = await _context.TravelPackages.FindAsync(id);
            _context.TravelPackages.Remove(travelPackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelPackageExists(int id)
        {
            return _context.TravelPackages.Any(e => e.Id == id);
        }
    }
}