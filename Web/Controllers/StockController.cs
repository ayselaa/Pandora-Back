using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Stock;
using Service.Interfaces;

namespace Web.Controllers
{
    public class StockController : BaseController
    {
        private readonly IStockService _stock;
        public StockController(IStockService stock)
        {
            _stock = stock;
        }

        [HttpPost]
        public async Task<IActionResult> PostStock(List<PostStockDto> stock)
        {
            try
            {
                await _stock.CreateStock(stock);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {

                return BadRequest(ex.Message);
            }

        }

    }
}
