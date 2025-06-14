using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CryptoGatewayApp.Data;
using CryptoGatewayApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoGatewayApp.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Payment> Payments { get; set; }

        public async Task OnGetAsync()
        {
            Payments = await _context.Payments.ToListAsync();
        }
    }
}
