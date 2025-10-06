using Capco.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capco.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders.CountAsync();
        var products = await _context.Products.CountAsync();
        var customers = await _context.Customers.CountAsync();
        ViewData["Orders"] = orders;
        ViewData["Products"] = products;
        ViewData["Customers"] = customers;
        return View();
    }
}
