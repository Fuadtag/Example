using Domain.Common;

namespace Domain.Entities
{
    public class Product:AuditingEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal Price{ get; set; }
    }
}