﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoackApp.Core.Application.ViewModels.Products
{
    public class SaveProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del producto")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Debe colocar el precio del producto")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Debe colocar la categoria del producto")]
        public int CategoryId { get; set; }
    }
}
