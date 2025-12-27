using ciceksepeti.Entities;
using ciceksepeti.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ciceksepeti.Data
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // Örnek ürünler
        private static List<Product> _products = new List<Product>
        {
            new Product { ProductID = 1, Name = "Cam Akvaryumda Kokina Çiçeği ", Price = 699, ImageUrl = "/images/kokina/a.jpg", Category = "Kokina" },
            new Product { ProductID = 2, Name = "Akvaryum Vazoda Kokina Çiçeği", Price = 599, ImageUrl = "/images/kokina/b.jpg", Category = "Kokina" },
            new Product { ProductID = 3, Name = "Kraft Buket Kağıdında Kokina Çiçeği", Price = 599, ImageUrl = "/images/kokina/c.jpg", Category = "Kokina" },
            new Product { ProductID = 4, Name = "Yeşil Kahve Bukette Kokina Çiçeği", Price = 549, ImageUrl = "/images/kokina/d.jpg", Category = "Kokina" },
            new Product { ProductID = 5, Name = "Jüt Bukette Kokina Çiçeği", Price = 649, ImageUrl = "/images/kokina/ç.jpg", Category = "Kokina" },
            new Product { ProductID = 6, Name = "Kokina Yılbaşı Çiçeği", Price = 599, ImageUrl = "/images/kokina/e.jpg", Category = "Kokina" },
            new Product { ProductID = 7, Name = "Kokina Çiçeği Ve Kırmızı Güller", Price = 599, ImageUrl = "/images/kokina/f.jpg", Category = "Kokina" },
            new Product { ProductID = 8, Name = "Bukette Kırmızı Beyaz Güller ve Kokina Çiçeği", Price = 749, ImageUrl = "/images/kokina/g.jpg", Category = "Kokina" },
            new Product { ProductID = 9, Name = "Paşabahçe Cam Vazoda Kokina Çiçeği", Price = 549, ImageUrl = "/images/kokina/ğ.jpg", Category = "Kokina" },
            new Product { ProductID = 10, Name = "Altın Saksıda Kokina Çiçeği", Price = 749, ImageUrl = "/images/kokina/h.jpg", Category = "Kokina" },

            new Product { ProductID = 11, Name = "Saksıda Beyaz Spatifilyum", Price = 499, ImageUrl = "/images/saksı/a1.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 12, Name = "Kırmızı Antoryum Saksı Çiçeği", Price = 599, ImageUrl = "/images/saksı/a2.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 13, Name = "İki Dal Beyaz Orkide Çiçeği", Price = 999, ImageUrl = "/images/saksı/a3.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 14, Name = "Ring Saksıda Beyaz Kalanchoe Teraryum", Price = 559, ImageUrl = "/images/saksı/a4.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 15, Name = "Altın Renkli Saksıda Mor Çiftli Orkide ", Price = 1049, ImageUrl = "/images/saksı/a5.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 16, Name = "Gazete Desenli Buket Süslemeli Saksıda Spatifilyum", Price = 549, ImageUrl = "/images/saksı/a6.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 17, Name = "Beyaz Beton Saksıda Çiftli Mor Orkide ", Price = 1049, ImageUrl = "/images/saksı/a7.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 18, Name = "Beyaz Beton Saksıda Çiftli Beyaz Phalanopsis Orkide", Price = 999, ImageUrl = "/images/saksı/a8.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 19, Name = "Phalanopsis Tek Dal Orkide Çiçeği", Price = 899, ImageUrl = "/images/saksı/a9.jpg", Category = "Saksı Çiçekleri" },
            new Product { ProductID = 20, Name = "Dekor Saksıda Kırmızı Kalanchoe Teraryum", Price = 529, ImageUrl = "/images/saksı/a10.jpg", Category = "Saksı Çiçekleri" },

            new Product { ProductID = 21, Name = "9'lu Kırmızı Gül Çiçek Buketi", Price = 569, ImageUrl = "/images/doğum/b1.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 22, Name = "Seni Seviyorum 5'li Kırmızı Gül Buketi ", Price = 439, ImageUrl = "/images/doğum/b2.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 23, Name = "Paşabahçe Akvaryum Vazoda 7 Kırmızı Gül", Price = 569, ImageUrl = "/images/doğum/b3.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 24, Name = "Beyaz Papatya Çiçek Buketi", Price = 499, ImageUrl = "/images/doğum/b4.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 25, Name = "Çizgili Cam Vazoda Kırmızı Ve Beyaz Güller", Price = 579, ImageUrl = "/images/doğum/b5.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 26, Name = "Gül Şekilli Kırmızı Ve Vanilya Aromalı Kekler", Price = 359, ImageUrl = "/images/doğum/b6.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 27, Name = "Siyah Çizgili Vazoda Beyaz Papatyalar", Price = 449, ImageUrl = "/images/doğum/b7.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 28, Name = "Bej Vazoda Beyaz Papatyalar Ve Pembe Gerbaralar", Price = 429, ImageUrl = "/images/doğum/b8.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 29, Name = "Paşabahçe Vazoda 9 Kırmızı Gül", Price = 629, ImageUrl = "/images/doğum/b9.jpg", Category = "Doğum Günü Çiçekleri" },
            new Product { ProductID = 30, Name = "Bej Vazoda Beyaz Papatyalar Ve Kırmızı Güller", Price = 499, ImageUrl = "/images/doğum/b10.jpg", Category = "Doğum Günü Çiçekleri" },

            new Product { ProductID = 31, Name = "Kırmızı Antoryum Saksı Çiçeği", Price = 599, ImageUrl = "/images/yıl/c1.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 32, Name = "Beyaz Papatyalar Çiçek Buketi", Price = 549, ImageUrl = "/images/yıl/c2.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 33, Name = "Siyah Buket Kağıdında 35'li Kırmızı Gül Buketi Mizu", Price = 1699, ImageUrl = "/images/yıl/c3.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 34, Name = "Akvaryumda 7 Kırmızı Gül Ve Kalpli Gurme Lezzetler", Price = 619, ImageUrl = "/images/yıl/c4.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 35, Name = "Paşabahçe Vazoda 20'li KIrmızı Gül", Price = 1099, ImageUrl = "/images/yıl/c5.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 36, Name = "Seni Seviyorum Mesajlı Papatya Aranjmaı", Price = 599, ImageUrl = "/images/yıl/c6.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 37, Name = "Kraft Buket Kağıdında 25'li Kırmızı Gül Buketi", Price = 1299, ImageUrl = "/images/yıl/c7.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 38, Name = "Kırmızı Gerbera Çiçek Buketi", Price = 319, ImageUrl = "/images/yıl/c8.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 39, Name = "Kırmızı Guzmania Çiçeği", Price = 699, ImageUrl = "/images/yıl/c9.jpg", Category = "Yıldönümü Çiçekleri" },
            new Product { ProductID = 40, Name = "Aşkın Adı 5 Beyaz Gül ", Price = 499, ImageUrl = "/images/yıl/c10.jpg", Category = "Yıldönümü Çiçekleri" },

        };

        // Kategoriye göre ürünleri almak için metod
        public List<Product> GetProductsByCategory(string category)
        {
            return _products.Where(p => p.Category == category).ToList();
        }

        // Tüm ürünleri döndürme (İhtiyaç duyulursa ekleyebilirsiniz)
        public List<Product> GetAllProducts()
        {
            return _products;
        }
        public void AddUpdatedProduct(UpdatedProduct updatedProduct)
        {
            // Yeni ürünü UpdatedProducts tablosuna ekle
            _context.UpdatedProducts.Add(updatedProduct);

            // Veritabanına kaydet
            _context.SaveChanges();

        }
    }
}
