using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sitoAutenticazioneFrau.Data;
using sitoAutenticazioneFrau.Models;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;

namespace sitoAutenticazioneFrau.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Checkout
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Identity/Account/Login");

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            return View(cartItems);
        }

        // POST: /Checkout/CreateCheckoutSession
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Identity/Account/Login");

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            if (!cartItems.Any()) return RedirectToAction("Index", "Cart");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = cartItems.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Quantity
                }).ToList(),
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Checkout", null, Request.Scheme),
                CancelUrl = Url.Action("Index", "Cart", null, Request.Scheme)
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url!);
        }

        // GET: /Checkout/Success
        public async Task<IActionResult> Success()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Identity/Account/Login");

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            if (!cartItems.Any()) return RedirectToAction("Index", "Cart");

            var order = new Order
            {
                UserId = user.Id,
                Total = cartItems.Sum(c => c.Product.Price * c.Quantity)
            };

            foreach (var item in cartItems)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return View(order);
        }
    }
}
