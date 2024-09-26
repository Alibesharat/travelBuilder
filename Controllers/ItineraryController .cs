using Microsoft.AspNetCore.Mvc;
using TravelBuilder.DAL.DbContexts;

namespace travelBuilder.Controllers
{
    [Route("[controller]")]
    public class ItineraryController : Controller
    {
        private readonly ILogger<ItineraryController> _logger;

        private readonly TravelDbContext _context;

        public ItineraryController(TravelDbContext context, ILogger<ItineraryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Itinerary
        public async Task<IActionResult> Index()
        {
            var itineraries = await _context.Itineraries
                .Include(i => i.Agency) // Including the related Agency for display purposes
                .ToListAsync();
            return View(itineraries);
        }

        // GET: Itinerary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = await _context.Itineraries
                .Include(i => i.Agency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // GET: Itinerary/Create
        public IActionResult Create()
        {
            // Here, you might want to pass a list of agencies for selection in the view
            ViewData["Agencies"] = _context.Agencies.ToList();
            return View();
        }

        // POST: Itinerary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,StartDate,EndDate,Description,AgencyId")] Itinerary itinerary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itinerary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itinerary);
        }

        // GET: Itinerary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = await _context.Itineraries.FindAsync(id);
            if (itinerary == null)
            {
                return NotFound();
            }

            ViewData["Agencies"] = _context.Agencies.ToList(); // For selecting a different agency in the edit view
            return View(itinerary);
        }

        // POST: Itinerary/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,StartDate,EndDate,Description,AgencyId")] Itinerary itinerary)
        {
            if (id != itinerary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itinerary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItineraryExists(itinerary.Id))
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
            return View(itinerary);
        }

        // GET: Itinerary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = await _context.Itineraries
                .Include(i => i.Agency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // POST: Itinerary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itinerary = await _context.Itineraries.FindAsync(id);
            _context.Itineraries.Remove(itinerary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryExists(int id)
        {
            return _context.Itineraries.Any(e => e.Id == id);
        }
    }
}