// TODO: adjust namespaces/types
using Blog.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace BloggingSystem.Tests.Infrastructure;

public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType.FullName?.Contains("DbContextOptions") == true);
            if (descriptor != null) services.Remove(descriptor);

            // TODO: replace BlogDbContext with your context type
            services.AddDbContext<BlogDbContext>(o => o.UseInMemoryDatabase("api-tests"));
        });
    }
}
