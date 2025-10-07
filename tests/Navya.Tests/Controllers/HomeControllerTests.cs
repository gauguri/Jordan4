using Navya.Data;
using Navya.Domain.Entities;
using Navya.Services.Catalog;
using Navya.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Navya.Tests.Controllers;

public class HomeControllerTests
{
    [Fact]
    public async Task Index_ReturnsViewWithProducts()
    {
        var products = new List<Product>();
        var catalog = new Mock<ICatalogService>();
        catalog.Setup(c => c.GetProductsAsync(null, null, null, "Pastel", null, 1, 4, default))
            .ReturnsAsync((products, 0));

        var context = TestDbContextFactory.CreateContext(nameof(Index_ReturnsViewWithProducts));
        var controller = new HomeController(context, catalog.Object);

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(products, viewResult.Model);
    }
}
