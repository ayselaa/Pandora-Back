using Domain;
using Domain.Entities;
using Domain.Entities.Products;
using Domain.Entities.Stocks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Product;
using Service.DTOs.Stock;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> PostProduct(List<ProductDto> prod)
        {
        
            ArgumentNullException.ThrowIfNull(nameof(prod));

            var products = prod.Select(pdto => new Product
            {
                ItemCode = pdto.ItemCode,
                Name = pdto.Name,
                Artikul = pdto.Artikul,
                Manufacturer = pdto.Manufacturer,
                Brand = pdto.Brand,
                Model = pdto.Model,
                Price = pdto.Price,
                Package = pdto.Package,
                Stock = pdto.Stock
            }).ToList();

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            return products;

        }



        //public async Task<List<Product>> PostProduct(List<ProductDto> prod)
        //{
        //    if (prod is null)
        //    {
        //        throw new ArgumentNullException(nameof(prod));
        //    }

        //    var products = new List<Product>();

        //    foreach (var pdto in prod)
        //    {
        //        var model = pdto.StockIds.Select(item => new ProductStock
        //        {
        //            StockId = item.StockId,
        //            Count = item.Count
        //        }).ToList();

        //        var product = new Product
        //        {
        //            ProductStocks = model,
        //            Artikul = pdto.Artikul,
        //            Name = pdto.Name,
        //            Manufacturer = pdto.Manufacturer,
        //            Brand = pdto.Brand,
        //            ProductCode = pdto.ProductCode,
        //            Model = pdto.Model,
        //            Price = pdto.Price
        //        };

        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();

        //        products.Add(product);
        //    }

        //    return products;
        //}


    }
}

    


