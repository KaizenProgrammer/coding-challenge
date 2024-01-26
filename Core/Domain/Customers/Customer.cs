
namespace coding_challenge.Core.Domain.Customers
{
    public class Customer
    {
        public int Id { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Email { get; internal set; }
        public DateTime CreatedDateTime { get; internal set; }
        public DateTime UpdatedDateTime { get; internal set; }
    }
}
