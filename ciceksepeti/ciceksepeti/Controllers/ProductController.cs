using ciceksepeti.Data;
using ciceksepeti.Entities;
using ciceksepeti.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ciceksepeti.Controllers
{
    public class ProductController : Controller
    {

        private readonly ProductRepository _repository;

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context, ProductRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public IActionResult UpdatePrice()
        {
            var updatedProducts = _context.UpdatedProducts
                              .OrderByDescending(p => p.UpdatedAt)  // Güncellenme tarihine göre sıralama
                              .GroupBy(up => up.Id)  // Ürün ID'lerine göre grupla
                              .Select(g => g.FirstOrDefault())  // Her gruptan ilk elemanı al (en son güncellenmiş ürün)
                              .ToList();
            var model = new UpdatedProductViewModel
            {
                Yields = _context.Yields.ToList(),
                UpdatedProducts = _context.UpdatedProducts.ToList()
            };

            return View(model);
        }
        [ResponseCache(Duration = 0, NoStore = true)]

        // Güncellenen Ürünlerin Listesi
        [HttpPost]
        public IActionResult UpdatePrice(UpdatedProductViewModel model)
        {
            var product = _context.Yields.FirstOrDefault(p => p.ProductID == model.Id);
            if (product != null)
            {
                // Eski fiyatı sakla
                var oldPrice = product.Price;

                // Fiyatı güncelle
                product.Price = model.NewPrice;
                _context.SaveChanges();

                var existingProduct = _context.UpdatedProducts
                                              .FirstOrDefault(up => up.Id == product.ProductID);

                // Eğer ürün daha önce eklenmişse, eski kaydı sil
                if (existingProduct != null)
                {
                    _context.UpdatedProducts.Remove(existingProduct);
                    _context.SaveChanges();
                }

                // UpdatedProducts tablosuna ekle
                var updatedProduct = new UpdatedProduct
                {
                    Id = product.ProductID,
                    ProductName = product.Name,
                    OldPrice = oldPrice,
                    NewPrice = model.NewPrice,
                    UpdatedAt = System.DateTime.Now
                };

                _repository.AddUpdatedProduct(updatedProduct);
            }
            var updatedProducts = _context.UpdatedProducts
                                .OrderByDescending(p => p.UpdatedAt)  // Güncellenme tarihine göre sıralama
                                .GroupBy(up => up.Id)  // Ürün ID'lerine göre grupla
                                .Select(g => g.FirstOrDefault())  // Her gruptan ilk elemanı al (en son güncellenmiş ürün)
                                .ToList();


            model.Yields = _context.Yields.ToList(); // Güncel ürünleri model'e ekle

            // Modeli güncelleyerek tekrar sayfaya gönder
            model.UpdatedProducts = _context.UpdatedProducts.ToList();
            return View(model);
        }
        public async Task<IActionResult> SalesSummary()
        {
            // Veritabanındaki tüm ürün satış özetlerini çekiyoruz
            var productSalesSummary = await _context.ProductSalesSummary.ToListAsync();

            // Ürünlerin toplam satış gelirini hesaplıyoruz
            return View(productSalesSummary);
        }


       
    }
}

