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
                        Width = 36,
                        Depth = 14,
                        CustomerName = "Devin Patterson",
                        Drawers = 1,
                        Material = DesktopMaterial.Laminate,
                        RushDays = RushOrder.Standard,
                        Price = 350.00M,
                        QuoteDate = DateTime.Parse("2018-6-1")
                    },
                      new Desk
                      {
                          Width = 24,
                          Depth = 12,
                          CustomerName = "Teagan Patterson",
                          Drawers = 0,
                          Material = DesktopMaterial.Veneer,
                          RushDays = RushOrder.ThreeDay,
                          Price = 385.00M,
                          QuoteDate = DateTime.Parse("2012-4-8")
                      },
                      new Desk
                      {
                          Width = 28,
                          Depth = 24,
                          CustomerName = "Jordan Peterson",
                          Drawers = 7,
                          Material = DesktopMaterial.Rosewood,
                          RushDays = RushOrder.Standard,
                          Price = 850.00M,
                          QuoteDate = DateTime.Parse("2012-10-30")
                      },
                      new Desk
                      {
                          Width = 72,
                          Depth = 48,
                          CustomerName = "Mike Tyson",
                          Drawers = 3,
                          Material = DesktopMaterial.Oak,
                          RushDays = RushOrder.FiveDay,
                          Price = 3066.00M,
                          QuoteDate = DateTime.Parse("2008-12-2")
                      }
                    );
                context.SaveChanges();
            }
        }

    }
}
