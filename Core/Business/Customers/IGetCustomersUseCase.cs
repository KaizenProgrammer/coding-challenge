using coding_challenge.Core.Domain.Customers;

namespace coding_challenge.Core.Business.Customers
{
    public interface IGetCustomersUseCase
    {
        Task<List<Customer>> Execute();
    }
}