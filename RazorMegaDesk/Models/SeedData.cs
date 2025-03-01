using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using RazorMegaDesk.Data;

namespace RazorMegaDesk.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorMegaDeskContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RazorMegaDeskContext>>()))
            {
                if (context == null || context.Desk == null)
                {
                    throw new ArgumentNullException("Null RazorMegaDeskContext");
                }
                if (context.Desk.Any())
                {
                    return;
                }

                context.Desk.AddRange(
                    new Desk
                    {
                        Width = 10,
                        Depth = 1,
                        CustomerName = "Devin Patterson",
                        Drawers = 1,
                        Material = DesktopMaterial.Laminate,
                        RushDays = 1,
                        Price = 1.50M
                    },
                      new Desk
                      {
                          Width = 24,
                          Depth = 6,
                          CustomerName = "Teagan Patterson",
                          Drawers = 0,
                          Material = DesktopMaterial.Veneer,
                          RushDays = 7,
                          Price = 5.50M
                      }
                    );
                context.SaveChanges();
            }
        }

    }
}
