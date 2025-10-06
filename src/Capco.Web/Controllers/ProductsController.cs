using Capco.Domain.Entities;
using Capco.Services.Catalog;
using Capco.Services.Cart;
using Capco.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capco.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ICartService _cartService;

    public ProductsController(ICatalogService catalogService, ICartService cartService)
    {
        _catalogService = catalogService;
        _cartService = cartService;
    }

    [HttpGet("/products/{slug}")]
    public async Task<IActionResult> Details(string slug)
    {
        var product = await _catalogService.GetProductBySlugAsync(slug);
        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductDetailViewModel
        {
            Product = product,
            SelectedVariant = product.Variants.FirstOrDefault()
        };

        ViewData["Title"] = product.Name;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(string slug, int variantId, int quantity = 1)
    {
        var guestToken = EnsureGuestToken();
        var product = await _catalogService.GetProductBySlugAsync(slug);
        if (product == null)
        {
            return NotFound();
        }

        var variant = product.Variants.FirstOrDefault(v => v.Id == variantId);
        if (variant == null)
        {
            return NotFound();
        }

        var cart = await _cartService.GetOrCreateCartAsync(User.Identity?.Name, guestToken);
        await _cartService.AddItemAsync(cart, variant, quantity);
        TempData["Message"] = "Added to cart";
        return RedirectToAction("Index", "Cart");
    }
}

    private string EnsureGuestToken()
    {
        if (Request.Cookies.TryGetValue("capco-cart", out var token) && !string.IsNullOrEmpty(token))
        {
            return token;
        }

        token = Guid.NewGuid().ToString("N");
        Response.Cookies.Append("capco-cart", token, new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        });
        return token;
    }
