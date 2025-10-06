using Capco.Data;
using Capco.Domain.Entities;
using Capco.Services.Email;
using Capco.Services.Inventory;
using Capco.Services.Pricing;
using Microsoft.EntityFrameworkCore;

namespace Capco.Services.Orders;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly IInventoryService _inventoryService;
    private readonly IPricingService _pricingService;
    private readonly IEmailSender _emailSender;

    public OrderService(ApplicationDbContext context, IInventoryService inventoryService, IPricingService pricingService, IEmailSender emailSender)
    {
        _context = context;
        _inventoryService = inventoryService;
        _pricingService = pricingService;
        _emailSender = emailSender;
    }

    public async Task<Order> CreateOrderFromCartAsync(Cart cart, Address shipping, Address? billing, string email, CancellationToken cancellationToken = default)
    {
        if (!cart.Items.Any())
        {
            throw new InvalidOperationException("Cart is empty");
        }

        shipping.Type = "Shipping";
        if (billing != null)
        {
            billing.Type = "Billing";
        }

        var subtotal = _pricingService.CalculateSubtotal(cart.Items);
        var order = new Order
        {
            OrderNumber = GenerateOrderNumber(),
            Email = email,
            Status = "pending",
            Subtotal = subtotal,
            Shipping = 9.95m,
            Tax = Math.Round(subtotal * 0.06625m, 2),
            Total = subtotal + 9.95m + Math.Round(subtotal * 0.06625m, 2),
            CreatedAt = DateTime.UtcNow,
            ShippingAddress = shipping,
            BillingAddress = billing,
            Items = cart.Items.Select(i => new OrderItem
            {
                ProductVariantId = i.ProductVariantId,
                NameSnapshot = i.Variant.Product.Name,
                ColorSnapshot = i.Variant.Color,
                SizeSnapshot = i.Variant.SizeLabel,
                Qty = i.Qty,
                UnitPriceSnapshot = i.UnitPriceSnapshot
            }).ToList()
        };

        foreach (var item in cart.Items)
        {
            await _inventoryService.ReserveAsync(item.Variant, item.Qty, cancellationToken);
        }

        _context.Orders.Add(order);
        _context.CartItems.RemoveRange(cart.Items);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task MarkPaidAsync(Order order, string paymentIntentId, CancellationToken cancellationToken = default)
    {
        order.Status = "paid";
        order.StripePaymentIntentId = paymentIntentId;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
        await _emailSender.SendOrderConfirmationAsync(order, cancellationToken);
    }

    public async Task UpdateStatusAsync(Order order, string status, CancellationToken cancellationToken = default)
    {
        order.Status = status;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static string GenerateOrderNumber() => $"CAP-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
}
