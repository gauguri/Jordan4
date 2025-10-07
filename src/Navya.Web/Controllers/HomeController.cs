using Navya.Data;
using Navya.Services.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Navya.Web.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICatalogService _catalogService;

    public HomeController(ApplicationDbContext context, ICatalogService catalogService)
    {
        _context = context;
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index()
    {
        var featured = await _catalogService.GetProductsAsync(null, null, null, "Pastel", null, 1, 4);
        ViewData["Title"] = "Classic Jordan Almonds";
        return View(featured.Products);
    }

    public IActionResult Occasions()
    {
        ViewData["Title"] = "Shop by Occasion";
        return View();
    }
}
