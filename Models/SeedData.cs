using Microsoft.EntityFrameworkCore;
using MvcSocialMedia.Data;

namespace MvcSocialMedia.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcSocialMediaContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcSocialMediaContext>>()))
        {}
    }
}