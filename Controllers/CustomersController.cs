using coding_challenge.Core.Business.Customers;
using coding_challenge.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace coding_challenge.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Get Customers")]
        public async Task<IActionResult> Get([FromServices] IGetCustomersUseCase getCustomers)
        {
            var results = await getCustomers.Execute();
            return Ok(results.Select(result => new AddCustomerResponse
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                CreatedDate = result.CreatedDateTime,
                UpdatedDate = result.UpdatedDateTime
            }).ToList());
        }

        [HttpPost(Name = "Add Customer")]
        public async Task<IActionResult> Post([FromServices] IAddCustomerUseCase addCustomer,
        [FromBody] AddCustomerRequest customer)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            try
            {
                var result = await addCustomer.Execute(customer.FirstName, customer.LastName, customer.Email);

                return Created($"/customer/{result.Id}", new AddCustomerResponse
                {
                    Id = result.Id,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    CreatedDate = result.CreatedDateTime,
                    UpdatedDate = result.UpdatedDateTime
                });

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("email", ex.Message);
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPut("{customerId}", Name = "Update Customer")]
        public async Task<IActionResult> Put([FromServices] IUpdateCustomerUseCase updateCustomer, [FromRoute] int customerId,
        [FromBody] UpdateCustomerRequest customer)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            try
            {
                var result = await updateCustomer.Execute(customerId, customer.FirstName, customer.LastName, customer.Email);

                return Ok(new AddCustomerResponse
                {
                    Id = result.Id,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    CreatedDate = result.CreatedDateTime,
                    UpdatedDate = result.UpdatedDateTime
                });

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("email", ex.Message);
                return UnprocessableEntity(ModelState);
            }
        }
    }
}
