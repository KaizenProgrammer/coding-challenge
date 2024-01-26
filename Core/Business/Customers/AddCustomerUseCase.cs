using coding_challenge.Core.Domain.Customers;
using coding_challenge.Core.Infrastructure.DB;

namespace coding_challenge.Core.Business.Customers
{
    public class AddCustomerUseCase : IAddCustomerUseCase
    {
        private DataContext _context { get; set; }
        public AddCustomerUseCase(DataContext context)
        {
            _context = context;
        }

        public async Task<Customer> Execute(string firstName, string lastName, string email)
        {
            if (_context.Customers.Any(c => c.Email == email))
            {
                // Someone other than the updated customer has this email claimed.
                throw new ArgumentException($"{email} is already used.");
            }
            var customer = new CustomerEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                CreatedDateTime = customer.CreatedDateTime,
                UpdatedDateTime = customer.UpdatedDateTime
            };

        }

    }
}
