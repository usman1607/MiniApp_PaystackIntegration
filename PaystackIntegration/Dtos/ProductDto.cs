﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }

    public class CreateProductRequestModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public IFormFile file { get; set; }

        public string Description { get; set; }
    }
}
