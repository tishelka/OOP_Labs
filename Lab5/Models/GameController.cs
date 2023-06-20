using Microsoft.CodeAnalysis.Operations;

namespace OOP_ICT.Fifth.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using OOP_ICT.Fifth.Models;
using OOP_ICT.Fifth.Entity;

public class GameController : Controller
{
    private readonly PokerDbContext _context;

    public GameController(PokerDbContext context)
    {
        _context = context;
    }

   
    public async Task<IActionResult> Index()
    {
        var games = await _context.Games.ToListAsync();
        return View(games);
    }

    
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Games
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

   
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Pot")] GameEntity game)
    {
        if (ModelState.IsValid)
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(game);
    }

    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Games
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

   
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var game = await _context.Games.FindAsync(id);
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
