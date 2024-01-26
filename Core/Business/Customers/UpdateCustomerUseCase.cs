using coding_challenge.Core.Domain.Customers;
using coding_challenge.Core.Infrastructure.DB;

namespace coding_challenge.Core.Business.Customers
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private DataContext _context { get; set; }
        public UpdateCustomerUseCase(DataContext context)
        {
            _context = context;
        }

        public async Task<Customer> Execute(int id, string firstName, string lastName, string email)
        {
            var customer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();
            if (customer == null)
                throw new ArgumentNullException("Could not find the customer", nameof(customer));

            if (_context.Customers.Any(c => c.Email == email && c.Id != id))
            {
                // Someone other than the updated customer has this email claimed.
                throw new ArgumentException($"{email} is already used.");
            }

            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Email = email;
            customer.UpdatedDateTime = DateTime.Now;
            _context.Customers.Update(customer);
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
