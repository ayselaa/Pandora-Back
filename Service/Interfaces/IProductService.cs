﻿using Domain.Entities.Products;
using Service.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> PostProduct(List<ProductDto> prod);
    }
}
