using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Category
    public async Task<IActionResult> Index()
    {
        return _context.Categories != null
            ? View(await _context.Categories.ToListAsync())
            : Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
    }

    // GET: Category/AddOrEdit
    public IActionResult AddOrEdit(int id = 0)
    {
        return id == 0 ? View(new Category()) : (IActionResult)View(_context.Categories.Find(id));
    }

    // POST: Category/AddOrEdit To protect from overposting attacks, enable the specific
    // properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
    {
        if (ModelState.IsValid)
        {
            _ = category.CategoryId == 0 ? _context.Add(category) : _context.Update(category);

            _ = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    // POST: Category/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Categories == null) return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");

        var category = await _context.Categories.FindAsync(id);
        if (category != null) _ = _context.Categories.Remove(category);

        _ = await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}