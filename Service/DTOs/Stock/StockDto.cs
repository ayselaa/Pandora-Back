using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Stock
{
    public class StockDto
    {
       
        public string StockId { get; set; }
        public int Count { get; set; }
    }
}
