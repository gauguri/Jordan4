using Navya.Data;
using Navya.Domain.Entities;
using Navya.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Navya.Tests;

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
