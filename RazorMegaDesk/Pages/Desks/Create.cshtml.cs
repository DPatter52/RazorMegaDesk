﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorMegaDesk.Models;

namespace RazorMegaDesk.Pages.Desks
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        private readonly RazorMegaDesk.Data.RazorMegaDeskContext _context;

        private int[,] rushOrderPrices = new int[3, 3];

        public CreateModel(RazorMegaDesk.Data.RazorMegaDeskContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Desk Desk { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            LoadRushOrderPrices();

            Desk.Price = GetPrice();

            _context.Desk.Add(Desk);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public decimal GetSurfaceArea()
        {
            return Desk.Width * Desk.Depth;
        }


        public decimal GetPrice()
        {
            decimal baseCost = 200;
            var drawerCost = 50 * Desk.Drawers;
            var surfaceArea = GetSurfaceArea();
            var surfaceAreaCost = surfaceArea > 1000 ? surfaceArea - 1000 : 0;
            var materialCost = GetMaterialCost();
            var rushCost = GetRushCost(surfaceArea);

            return baseCost + drawerCost + surfaceAreaCost + materialCost + rushCost;
        }

        public decimal GetMaterialCost()
        {
            var materialPrice = new Dictionary<DesktopMaterial, decimal>
            {
                { DesktopMaterial.Laminate, 100 },
                { DesktopMaterial.Oak, 200 },
                { DesktopMaterial.Rosewood, 300 },
                { DesktopMaterial.Veneer, 125 },
                { DesktopMaterial.Pine, 50 }
            };

            if (materialPrice.TryGetValue(Desk.Material, out var materialCost)) return materialCost;
            return 0;
        }

        public void LoadRushOrderPrices()
        {
            try
            {
                string filePath = Path.Combine(_env.ContentRootPath, "rushOrderPrices.txt");

                string[] lines = System.IO.File.ReadAllLines(filePath);

                rushOrderPrices = new int[3, 3];
                int index = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        rushOrderPrices[i, j] = int.Parse(lines[index].Trim());
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading rush order prices: " + ex.Message);
            }
        }



        public decimal GetRushCost(decimal surfaceArea)
        {
            int rushIndex;
            if (Desk.RushDays == RushOrder.ThreeDay) rushIndex = 0;
            else if (Desk.RushDays == RushOrder.FiveDay) rushIndex = 1;
            else if (Desk.RushDays == RushOrder.SevenDay) rushIndex = 2;
            else return 0m;

            int sizeIndex;
            if (surfaceArea < 1000) sizeIndex = 0;
            else if (surfaceArea <= 2000) sizeIndex = 1;
            else sizeIndex = 2;

            return rushOrderPrices[rushIndex, sizeIndex];
        }
    }
}
