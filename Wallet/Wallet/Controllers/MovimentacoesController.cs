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
    public class MovimentacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movimentacoes.Include(m => m.Carteira);
            return View(await applicationDbContext.ToListAsync());
        }
      
        public async Task<IActionResult> TelaIni(Guid Id, string nome)
        {
            var applicationDbContext = _context.Movimentacoes.Where(a=>a.CarteiraId==Id);
            ViewData["Nome"] = nome;
            ViewData["Valor"] = _context.Carteiras.Find(Id).Valor;
            ViewData["Id"] = Id;
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(Guid? id,Guid id2, string nome)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Carteira)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            ViewData["Id"] = id2;
            ViewData["Nome"] = nome;
            return View(movimentacao);
        }

        // GET: Movimentacoes/Create
        public IActionResult Create(Guid Id,string nome)
        {
            ViewData["CarteiraId"] = new SelectList(_context.Carteiras, "Id", "Nome");
            ViewData["Id"] = Id;
            ViewData["Nome"] = nome;
            return View();
        }

        // POST: Movimentacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                var carteira = _context.Carteiras.Find(movimentacao.CarteiraId);
                carteira.Valor += movimentacao.Valor;
                movimentacao.Data = DateTime.Now;
                movimentacao.Id = Guid.NewGuid();
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(TelaIni), new { id = carteira.Id, nome = carteira.Nome });

            }
            ViewData["CarteiraId"] = new SelectList(_context.Carteiras, "Id", "Nome", movimentacao.CarteiraId);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Guid id2, string nome)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacoes.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            ViewData["CarteiraId"] = new SelectList(_context.Carteiras, "Id", "Nome", movimentacao.CarteiraId);
            ViewData["Id"] = id2;
            ViewData["Nome"] = nome;
            return View(movimentacao);
        }

        // POST: Movimentacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarteiraId,Data,Descrip,Valor,Id")] Movimentacao movimentacao, decimal Valor)
        {
            if (id != movimentacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var carteira = _context.Carteiras.Find(movimentacao.CarteiraId);
                    var mantiga = _context.Movimentacoes.Find(id);
                    carteira.Valor += -mantiga.Valor;
                    carteira.Valor += movimentacao.Valor;
                    _context.Entry(mantiga).State = EntityState.Detached;
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var nome = _context.Carteiras.Find(movimentacao.CarteiraId).Nome;
                return RedirectToAction(nameof(TelaIni),new {id=movimentacao.CarteiraId, nome=nome});
            }
            ViewData["CarteiraId"] = new SelectList(_context.Carteiras, "Id", "Nome", movimentacao.CarteiraId);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id, Guid id2, string nome)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Carteira)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            ViewData["Id"] = id2;
            ViewData["Nome"] = nome;
            return View(movimentacao);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);
            var carteira = _context.Carteiras.Find(movimentacao.CarteiraId);
            carteira.Valor += -movimentacao.Valor; 
            _context.Movimentacoes.Remove(movimentacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(TelaIni), new { id = carteira.Id, nome = carteira.Nome });
        }

        private bool MovimentacaoExists(Guid id)
        {
            return _context.Movimentacoes.Any(e => e.Id == id);
        }
        
    }
}
