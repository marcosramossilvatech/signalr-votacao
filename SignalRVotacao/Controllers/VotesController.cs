using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignalRVotacao.Models;

namespace SignalRVotacao.Controllers
{
    public class VotesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public VotesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Votes
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Vote.Include(v => v.Participants).Include(v => v.Participants2).Include(v => v.Participants3);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vote == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote
                .Include(v => v.Participants)
                .Include(v => v.Participants2)
                .Include(v => v.Participants3)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // GET: Votes/Create
        public IActionResult Create()
        {
            ViewData["Participant1Id"] = new SelectList(_context.Participants, "ParticId", "ParticId");
            ViewData["Participant1Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName");
            ViewData["Participant2Id"] = new SelectList(_context.Participants, "ParticId", "ParticId");
            ViewData["Participant2Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName");
            ViewData["Participant3Id"] = new SelectList(_context.Participants, "ParticId", "ParticId");
            ViewData["Participant3Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName");
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoteId,Periodo,DataInicio,DataFim,Participant1Id,Participant1Total,Participant2Id,Participant2Total,Participant3Id,Participant3Total,")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Participant1Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant1Id);
            ViewData["Participant1Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant1Id);
            ViewData["Participant2Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant2Id);
            ViewData["Participant2Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant2Id);
            ViewData["Participant3Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant3Id);
            ViewData["Participant3Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant3Id);
            return View(vote);
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vote == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["Participant1Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant1Id);
            ViewData["Participant1Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant1Id); 
            ViewData["Participant2Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant2Id);
            ViewData["Participant2Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant2Id);
            ViewData["Participant3Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant3Id);
            ViewData["Participant3Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant3Id);
            return View(vote);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoteId,Periodo,DataInicio,DataFim,Participant1Id,Participant1Total,Participant2Id,Participant2Total,Participant3Id,Participant3Total")] Vote vote)
        {
            if (id != vote.VoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.VoteId))
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
            ViewData["Participant1Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant1Id);
            ViewData["Participant1Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant1Id);
            ViewData["Participant2Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant2Id);
            ViewData["Participant2Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant2Id);
            ViewData["Participant3Id"] = new SelectList(_context.Participants, "ParticId", "ParticId", vote.Participant3Id);
            ViewData["Participant3Nome"] = new SelectList(_context.Participants, "ParticId", "ParticName", vote.Participant3Id);
            return View(vote);
        }

        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vote == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote
                .Include(v => v.Participants)
                .Include(v => v.Participants2)
                .Include(v => v.Participants3)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vote == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Vote'  is null.");
            }
            var vote = await _context.Vote.FindAsync(id);
            if (vote != null)
            {
                _context.Vote.Remove(vote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteExists(int id)
        {
          return _context.Vote.Any(e => e.VoteId == id);
        }
    }
}
