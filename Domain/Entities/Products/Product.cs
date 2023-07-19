using Domain.Entities.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Products
{
    public class Product
    {
        [Key]
        public string ItemCode { get; set; }
        public string Artikul { get; set; }

        [DefaultValue("false")]
        public bool Package { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Stock { get; set; }
        public string ?Name { get; set; }


        //public List<ProductStock> ProductStocks { get; set; }


    }
}
