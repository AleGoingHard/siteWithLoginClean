using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sitoAutenticazioneFrau.Data;
using sitoAutenticazioneFrau.Models;

namespace sitoAutenticazioneFrau.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;  // 👈 aggiungi questo

        public ProductsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;  // 👈 inizializza qui
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("/Identity/Account/Login");

            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == user.Id);

            if (existingItem != null)
                existingItem.Quantity++;
            else
                _context.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    UserId = user.Id,
                    Quantity = 1
                });

            await _context.SaveChangesAsync();

            return Ok(); // Ajax riceve successo        }

        }
    }
}
