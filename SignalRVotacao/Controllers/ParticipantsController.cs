using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRVotacao.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRVotacao.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ParticipantsController(ApplicationDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
              return View(await _context.Participants.ToListAsync());
        }

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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticId,ParticName,Url,TotalVoto,Foto")] Participants participants)
        {
            if (ModelState.IsValid)
            {
                string nomeArquivo = UploadedFile(participants.Foto);
                participants.Url = nomeArquivo;
                _context.Add(participants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participants);
        }

        private string UploadedFile(Microsoft.AspNetCore.Http.IFormFile foto)
        {
            string nomeUnicoArquivo = null;

            if (foto!= null)
            {
                string pastaFotos = Path.Combine(webHostEnvironment.WebRootPath, "Imagens");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + foto.FileName;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    foto.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParticId,ParticName,Url,TotalVoto,Foto")] Participants participants)
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
