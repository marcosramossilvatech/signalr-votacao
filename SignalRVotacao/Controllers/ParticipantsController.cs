using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRVotacao.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRVotacao.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ParticipantsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Participants
        public async Task<IActionResult> Index()
        {
              return View(await _context.Participants.ToListAsync());
        }

        // GET: Participants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Participants == null)
            {
                return NotFound();
            }

            var participants = await _context.Participants
                .FirstOrDefaultAsync(m => m.ParticId == id);
            if (participants == null)
            {
                return NotFound();
            }

            return View(participants);
        }

        // GET: Participants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticId,ParticName,Url,TotalVoto")] Participants participants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participants);
        }

        // GET: Participants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Participants == null)
            {
                return NotFound();
            }

            var participants = await _context.Participants.FindAsync(id);
            if (participants == null)
            {
                return NotFound();
            }
            return View(participants);
        }

        // POST: Participants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParticId,ParticName,Url,TotalVoto")] Participants participants)
        {
            if (id != participants.ParticId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantsExists(participants.ParticId))
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
            return View(participants);
        }

        // GET: Participants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Participants == null)
            {
                return NotFound();
            }

            var participants = await _context.Participants
                .FirstOrDefaultAsync(m => m.ParticId == id);
            if (participants == null)
            {
                return NotFound();
            }

            return View(participants);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Participants == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Participants'  is null.");
            }
            var participants = await _context.Participants.FindAsync(id);
            if (participants != null)
            {
                _context.Participants.Remove(participants);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantsExists(int id)
        {
          return _context.Participants.Any(e => e.ParticId == id);
        }
    }
}
