using Navya.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Navya.Web.Controllers;

public class AboutController : Controller
{
    private readonly ApplicationDbContext _context;

    public AboutController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var content = await _context.ContentBlocks.FirstOrDefaultAsync(c => c.Key == "about") ?? new()
        {
            Html = "<p>Navya Enterprises Inc. has been handcrafting Jordan Almonds in East Hanover, NJ since 1972.</p>"
        };
        ViewData["Title"] = "About Navya Confectionery";
        return View(model: content.Html);
    }
}
