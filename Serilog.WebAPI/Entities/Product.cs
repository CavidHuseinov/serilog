﻿using Serilog.WebAPI.Abstraction;

namespace Serilog.WebAPI.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Price { get; set; }
    }
}
