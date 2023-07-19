using Domain.Entities;
using Service.DTOs.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Product
{
    public class ProductDto
    {
        public string ItemCode { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Artikul { get; set; }
        public string Manufacturer { get; set; }

        [DefaultValue("false")]
        public bool Package { get; set; }

        public int Stock { get; set; }
        public string Name { get; set; }

        //public List<StockDto> StockIds { get; set; }


    }
}
