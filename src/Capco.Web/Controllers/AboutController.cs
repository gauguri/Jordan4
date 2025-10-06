using Capco.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capco.Web.Controllers;

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
            Html = "<p>Capco Enterprises Inc. has been handcrafting Jordan Almonds in East Hanover, NJ since 1972.</p>"
        };
        ViewData["Title"] = "About Capco Confectionery";
        return View(model: content.Html);
    }
}
