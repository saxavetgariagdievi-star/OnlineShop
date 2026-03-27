namespace OnlineShoppingApi.Modules
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";

        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}