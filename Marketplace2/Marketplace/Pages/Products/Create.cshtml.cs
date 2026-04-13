using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Marketplace.Data;
using Marketplace.Models;

namespace Marketplace.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Простая валидация
            if (string.IsNullOrWhiteSpace(Product.Name) || Product.Price <= 0)
            {
                ModelState.AddModelError("", "Название и цена обязательны");
                return Page();
            }

            Product.CreatedAt = DateTime.Now;
            Product.Id = 0; // Сбросим на всякий случай

            if (string.IsNullOrEmpty(Product.ImageUrl))
            {
                Product.ImageUrl = "";  // или "default.jpg"
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}