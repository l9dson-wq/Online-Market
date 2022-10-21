using StockApp.Core.Domain.Common;

namespace StockApp.Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string Username { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
