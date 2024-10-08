using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;

namespace SalesAndPurchaseManagement.Data 
{ 
public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SAPManagementContext(
            serviceProvider.GetRequiredService<DbContextOptions<SAPManagementContext>>()))
            if (!context.Database.EnsureCreated())
            {
                Console.WriteLine("Database already exists.");
                    return;
            }
        }
    }
}
