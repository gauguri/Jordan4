using Navya.Data;
using Navya.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Navya.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ContentBlocksController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContentBlocksController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var blocks = await _context.ContentBlocks.ToListAsync();
        return View(blocks);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var block = await _context.ContentBlocks.FindAsync(id);
        if (block == null)
        {
            return NotFound();
        }
        return View(block);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContentBlock block)
    {
        if (id != block.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(block);
        }

        _context.ContentBlocks.Update(block);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
