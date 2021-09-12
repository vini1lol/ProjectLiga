using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wallet.Data;
using Wallet.Models;

namespace Wallet.Controllers
{
    [Authorize]
    public class CarteirasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarteirasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carteiras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carteiras.ToListAsync());
        }

        // GET: Carteiras/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carteira = await _context.Carteiras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carteira == null)
            {
                return NotFound();
            }

            return View(carteira);
        }

        // GET: Carteiras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carteiras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Valor,Ativo,Id")] Carteira carteira)
        {
            if (ModelState.IsValid)
            {
                carteira.Id = Guid.NewGuid();
                _context.Add(carteira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carteira);
        }

        // GET: Carteiras/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carteira = await _context.Carteiras.FindAsync(id);
            if (carteira == null)
            {
                return NotFound();
            }
            return View(carteira);
        }

        // POST: Carteiras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Valor,Ativo,Id")] Carteira carteira)
        {
            if (id != carteira.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carteira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarteiraExists(carteira.Id))
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
            return View(carteira);
        }

        // GET: Carteiras/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carteira = await _context.Carteiras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carteira == null)
            {
                return NotFound();
            }

            return View(carteira);
        }

        // POST: Carteiras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carteira = await _context.Carteiras.FindAsync(id);
            _context.Carteiras.Remove(carteira);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarteiraExists(Guid id)
        {
            return _context.Carteiras.Any(e => e.Id == id);
        }
    }
}
