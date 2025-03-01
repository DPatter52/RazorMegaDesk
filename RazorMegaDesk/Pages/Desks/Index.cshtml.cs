using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorMegaDesk.Data;
using RazorMegaDesk.Models;

namespace RazorMegaDesk.Pages.Desks
{
    public class IndexModel : PageModel
    {
        private readonly RazorMegaDesk.Data.RazorMegaDeskContext _context;

        public IndexModel(RazorMegaDesk.Data.RazorMegaDeskContext context)
        {
            _context = context;
        }

        public IList<Desk> Desk { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Sorts { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        public async Task OnGetAsync()
        {
            var desks = from m in _context.Desk select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                desks = desks.Where(d => d.CustomerName.Contains(SearchString));
            }


            if (!string.IsNullOrEmpty(SortBy))
            {
                if (SortBy == "Customer Name")
                {
                    desks = desks.OrderBy(item => item.CustomerName);
                }
                else if (SortBy == "Date Added")
                {
                    desks = desks.OrderBy(item => item.QuoteDate);
                }
            }
            else 
            {
                desks = desks.OrderBy(item => item.QuoteDate);
            }


            Sorts = new SelectList(new List<string> { "Date Added", "Customer Name" });
            Desk = await desks.ToListAsync();
        }
    }
}
