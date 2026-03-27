using Microsoft.VisualBasic;

namespace OnlineShoppingApi.Modules
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

    }
}