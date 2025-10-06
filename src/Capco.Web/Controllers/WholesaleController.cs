using Capco.Data;
using Capco.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capco.Web.Controllers;

public class WholesaleController : Controller
{
    private readonly ApplicationDbContext _context;

    public WholesaleController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var content = await _context.ContentBlocks.FirstOrDefaultAsync(c => c.Key == "wholesale");
        ViewData["Title"] = "Wholesale";
        ViewBag.Content = content?.Html;
        return View(new WholesaleInquiryViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(WholesaleInquiryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Wholesale";
            return View(model);
        }

        TempData["Message"] = "Thank you for your wholesale inquiry. Our team will respond within one business day.";
        return RedirectToAction(nameof(Index));
    }
}
