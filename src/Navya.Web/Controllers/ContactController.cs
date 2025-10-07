using Navya.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Navya.Web.Controllers;

public class ContactController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var content = await _context.ContentBlocks.FirstOrDefaultAsync(c => c.Key == "contact");
        ViewData["Title"] = "Contact Navya";
        ViewBag.Html = content?.Html;
        return View();
    }
}
