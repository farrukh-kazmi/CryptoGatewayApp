using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CryptoGatewayApp.Data;
using CryptoGatewayApp.Models;
using System.Threading.Tasks;

namespace CryptoGatewayApp.Pages.Payments
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Payment Payment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);

            if (Payment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Payment == null)
            {
                return NotFound();
            }

            var paymentToDelete = await _context.Payments.FindAsync(Payment.Id);

            if (paymentToDelete != null)
            {
                _context.Payments.Remove(paymentToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
