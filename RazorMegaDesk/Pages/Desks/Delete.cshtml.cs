using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorMegaDesk.Data;
using RazorMegaDesk.Models;

namespace RazorMegaDesk.Pages.Desks
{
    public class DeleteModel : PageModel
    {
        private readonly RazorMegaDesk.Data.RazorMegaDeskContext _context;

        public DeleteModel(RazorMegaDesk.Data.RazorMegaDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Desk Desk { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desk = await _context.Desk.FirstOrDefaultAsync(m => m.Id == id);

            if (desk == null)
            {
                return NotFound();
            }
            else
            {
                Desk = desk;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desk = await _context.Desk.FindAsync(id);
            if (desk != null)
            {
                Desk = desk;
                _context.Desk.Remove(Desk);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
