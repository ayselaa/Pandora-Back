using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Stocks
{
    public class Stock 
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        //public List<ProductStock> ProductStocks { get; set; }
    }
}
