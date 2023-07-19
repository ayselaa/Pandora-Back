using Domain;
using Domain.Entities.Stocks;
using Service.DTOs.Stock;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StockService: IStockService
    {
        private readonly AppDbContext _context;
        public StockService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> CreateStock(List<PostStockDto> stock)
        {
           
          ArgumentNullException.ThrowIfNull(nameof(stock));

           var newStocks = stock.Select(stockDto => new Stock
              {
                 Id = stockDto.Id,
                 Name = stockDto.Name
              }).ToList();

              _context.Stocks.AddRange(newStocks);
              await _context.SaveChangesAsync();

             return newStocks;
        }

    }
}
