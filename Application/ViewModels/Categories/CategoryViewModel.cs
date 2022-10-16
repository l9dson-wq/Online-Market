using StockApp.Core.Domain.Entities;

namespace StoackApp.Core.Application.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductQuantity { get; set; }

        //navigation property
        public ICollection<Product> Products { get; set; }
    }
}
