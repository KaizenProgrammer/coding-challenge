using coding_challenge.Core.Domain.Customers;

namespace coding_challenge.Core.Business.Customers
{
    public interface IUpdateCustomerUseCase
    {
        Task<Customer> Execute(int id, string firstName, string lastName, string email);
    }
}