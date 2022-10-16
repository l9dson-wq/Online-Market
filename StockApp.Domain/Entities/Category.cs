using StockApp.Core.Domain.Common;

namespace StockApp.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //Navigation Property
        public ICollection<Product> Products { get; set; }
    }

}
