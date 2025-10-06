using Capco.Data;
using Capco.Domain.Entities;
using Capco.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Capco.Tests;

public static class TestDbContextFactory
{
    public static ApplicationDbContext CreateContext(string name)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(name));
        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        var provider = services.BuildServiceProvider();
        return provider.GetRequiredService<ApplicationDbContext>();
    }
}
