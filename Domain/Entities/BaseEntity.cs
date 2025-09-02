using SecurityDriven;

namespace ECommerceApi.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = FastGuid.NewGuid();
        public bool IsDeleted { get; set; }
    }
}