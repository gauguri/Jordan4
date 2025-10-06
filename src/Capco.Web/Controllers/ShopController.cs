using Capco.Services.Catalog;
using Capco.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capco.Web.Controllers;

public class ShopController : Controller
{
    private readonly ICatalogService _catalogService;

    public ShopController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index(string? search, string? color, string? size, string? collection, string? sort, int page = 1, int pageSize = 12)
    {
        var result = await _catalogService.GetProductsAsync(search, color, size, collection, sort, page, pageSize);
        var viewModel = new CatalogViewModel
        {
            Products = result.Products,
            TotalCount = result.TotalCount,
            Search = search,
            Color = color,
            Size = size,
            Collection = collection,
            Sort = sort,
            Page = page,
            PageSize = pageSize,
            Colors = new[] { "White", "Pink", "Blue", "Yellow", "Green" },
            Sizes = new[] { "1 lb pouch", "5 lb box", "10 lb box" },
            Collections = new[] { "Classic", "Pastel", "Metallic" }
        };

        ViewData["Title"] = "Shop Jordan Almonds";
        return View(viewModel);
    }
}
