using Navya.Domain.Entities;
using Navya.Services.Carts;
using Navya.Services.Orders;
using Navya.Services.Payments;
using Navya.Services.Pricing;
using Navya.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Navya.Web.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly IPricingService _pricingService;
    private readonly IStripePaymentService _stripePaymentService;

    public CheckoutController(ICartService cartService, IOrderService orderService, IPricingService pricingService, IStripePaymentService stripePaymentService)
    {
        _cartService = cartService;
        _orderService = orderService;
        _pricingService = pricingService;
        _stripePaymentService = stripePaymentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cart = await _cartService.GetOrCreateCartAsync(User.Identity?.Name, Request.Cookies["navya-cart"]);
        if (!cart.Items.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        var viewModel = new CheckoutViewModel
        {
            Email = User.Identity?.IsAuthenticated == true ? User.Identity!.Name ?? string.Empty : string.Empty
        };

        ViewData["Title"] = "Checkout";
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CheckoutViewModel model)
    {
        var cart = await _cartService.GetOrCreateCartAsync(User.Identity?.Name, Request.Cookies["navya-cart"]);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var shipping = new Address
        {
            Line1 = model.Shipping.Line1,
            Line2 = model.Shipping.Line2,
            City = model.Shipping.City,
            State = model.Shipping.State,
            Zip = model.Shipping.Zip,
            Country = model.Shipping.Country,
            Type = "Shipping"
        };

        Address? billing = null;
        if (model.UseDifferentBillingAddress)
        {
            billing = new Address
            {
                Line1 = model.Billing.Line1,
                Line2 = model.Billing.Line2,
                City = model.Billing.City,
                State = model.Billing.State,
                Zip = model.Billing.Zip,
                Country = model.Billing.Country,
                Type = "Billing"
            };
        }

        var order = await _orderService.CreateOrderFromCartAsync(cart, shipping, billing, model.Email);
        var intent = await _stripePaymentService.CreateOrUpdatePaymentIntentAsync(order);
        order.StripePaymentIntentId = intent.Id;
        await _orderService.UpdateStatusAsync(order, order.Status);

        TempData["OrderNumber"] = order.OrderNumber;
        return RedirectToAction(nameof(Confirmation), new { orderNumber = order.OrderNumber });
    }

    public IActionResult Confirmation(string orderNumber)
    {
        if (string.IsNullOrEmpty(orderNumber))
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["Title"] = "Order Confirmation";
        var order = new Order { OrderNumber = orderNumber, Total = 0m, Status = "pending", Email = User.Identity?.Name ?? "guest@navya.local", Subtotal = 0m, Shipping = 0m, Tax = 0m, CreatedAt = DateTime.UtcNow };
        var model = new OrderConfirmationViewModel
        {
            Order = order,
            Message = "A confirmation email is on the way."
        };
        return View(model);
    }
}
