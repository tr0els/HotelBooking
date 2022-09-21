using System.Collections.Generic;
using HotelBooking.Core;
using Microsoft.AspNetCore.Mvc;


namespace HotelBooking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> repository;

        public CustomersController(IRepository<Customer> repos)
        {
            repository = repos;
        }

        // GET: customers
        [HttpGet(Name = "GetCustomers")]
        public IEnumerable<Customer> Get()
        {
            return repository.GetAll();
        }

        // GET customers/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST customers
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            repository.Add(customer);
            return CreatedAtRoute("GetCustomers", null);
        }


        // DELETE customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                repository.Remove(id);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
