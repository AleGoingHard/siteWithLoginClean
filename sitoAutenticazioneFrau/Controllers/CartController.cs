using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sitoAutenticazioneFrau.Data;
using sitoAutenticazioneFrau.Models;

namespace sitoAutenticazioneFrau.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var items = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            return View(items);
        }

        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Identity/Account/Login");

            var existing = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = user.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }

            // Log
            _context.UserActionLogs.Add(new UserActionLog
            {
                UserId = user.Id,
                Action = $"Added product {productId} x{quantity} to cart"
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var item = await _context.CartItems.FindAsync(id);
            if (item != null && item.UserId == user.Id)
            {
                _context.CartItems.Remove(item);

                _context.UserActionLogs.Add(new UserActionLog
                {
                    UserId = user.Id,
                    Action = $"Removed product {item.ProductId} from cart"
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Json(0);

            var count = await _context.CartItems
                .Where(c => c.UserId == user.Id)
                .SumAsync(c => c.Quantity);

            return Json(count);
        }

    }
}
