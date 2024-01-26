using coding_challenge.Core.Domain.Customers;

namespace coding_challenge.Core.Business.Customers
{
    public interface IAddCustomerUseCase
    {
        Task<Customer> Execute(string firstName, string lastName, string email);
    }
}