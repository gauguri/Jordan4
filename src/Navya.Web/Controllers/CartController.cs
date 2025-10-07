using Navya.Domain.Entities;
using Navya.Services.Carts;
using Navya.Services.Pricing;
using Navya.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Navya.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IPricingService _pricingService;

    public CartController(ICartService cartService, IPricingService pricingService)
    {
        _cartService = cartService;
        _pricingService = pricingService;
    }

    public async Task<IActionResult> Index(string? promoCode)
    {
        var cart = await _cartService.GetOrCreateCartAsync(User.Identity?.Name, EnsureGuestToken());
        var subtotal = _pricingService.CalculateSubtotal(cart.Items);
        var total = subtotal;
        string? message = null;
        if (!string.IsNullOrEmpty(promoCode))
        {
            total = _pricingService.ApplyDiscount(subtotal, promoCode, out message);
        }

        var viewModel = new CartViewModel
        {
            Cart = cart,
            Subtotal = subtotal,
            Total = total,
            PromoMessage = message
        };

        ViewData["Title"] = "Your Cart";
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateQuantity(int id, int quantity)
    {
        var cart = await _cartService.GetOrCreateCartAsync(User.Identity?.Name, EnsureGuestToken());
        await _cartService.UpdateQuantityAsync(cart, id, quantity);
        return RedirectToAction(nameof(Index));
    }

    private string EnsureGuestToken()
    {
        if (Request.Cookies.TryGetValue("navya-cart", out var token) && !string.IsNullOrEmpty(token))
        {
            return token;
        }

        token = Guid.NewGuid().ToString("N");
        Response.Cookies.Append("navya-cart", token, new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        });
        return token;
    }
}
