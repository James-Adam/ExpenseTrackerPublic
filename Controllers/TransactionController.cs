using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;

[Authorize]
public class TransactionController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Transaction
    public async Task<IActionResult> Index()
    {
        var applicationDbContext =
            _context.Transactions.Include(t => t.Category);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Transaction/AddOrEdit
    public IActionResult AddOrEdit(int id = 0)
    {
        PopulateCategories();
        return id == 0 ? View(new Transaction()) : (IActionResult)View(_context.Transactions.Find(id));
    }

    // POST: Transaction/AddOrEdit To protect from overposting attacks, enable the specific
    // properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit(
        [Bind("TransactionId,CategoryId,Amount,Note,Date")]
        Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            _ = transaction.TransactionId == 0 ? _context.Add(transaction) : _context.Update(transaction);

            _ = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopulateCategories();
        return View(transaction);
    }

    // POST: Transaction/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Transactions == null) return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");

        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null) _ = _context.Transactions.Remove(transaction);

        _ = await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [NonAction]
    public void PopulateCategories()
    {
        var CategoryCollection = _context.Categories.ToList();
        Category DefaultCategory = new() { CategoryId = 0, Title = "Choose a Category" };
        CategoryCollection.Insert(0, DefaultCategory);
        ViewBag.Categories = CategoryCollection;
    }
}