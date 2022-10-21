﻿using StockApp.Core.Domain.Common;

namespace StockApp.Core.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string? ImagePath { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }     

        //Navigation Property
        public Category? Category { get; set; }

        public int UserId { get; set; }

        //Navigation Property
        public User? User { get; set; }

    }
}
