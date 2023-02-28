using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRVotacao;
using SignalRVotacao.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRCrud.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<SignalrServer> _signalrHub;
        public ProductsController(ApplicationDBContext context, IHubContext<SignalrServer> signalrHub)
        {
            _context = context;
            _signalrHub = signalrHub;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }


        //GET: Products

        [HttpGet]
        public IActionResult GetProducts()
        {
            var res = _context.Products.ToList();
            return Ok(res);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProdId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdId,ProdName,Category,UnitPrice,StockQty")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                await _signalrHub.Clients.All.SendAsync("LoadProducts");
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProdId, [Bind("ProdId,ProdName,Category,UnitPrice,StockQty")] Products products)
        {
            if (ProdId != products.ProdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                    await _signalrHub.Clients.All.SendAsync("LoadProducts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProdId))
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
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProdId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProdId)
        {
            var products = await _context.Products.FindAsync(ProdId);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            await _signalrHub.Clients.All.SendAsync("LoadProducts");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProdId == id);
        }
    }
}
