using coding_challenge.Core.Domain.Customers;
using coding_challenge.Core.Infrastructure.DB;

namespace coding_challenge.Core.Business.Customers
{
    public class GetCustomersUseCase : IGetCustomersUseCase
    {
        private DataContext _context { get; set; }
        public GetCustomersUseCase(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> Execute()
        {
            return _context.Customers.Select(customer => new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                CreatedDateTime = customer.CreatedDateTime,
                UpdatedDateTime = customer.UpdatedDateTime
            }).ToList();
        }

    }
}
