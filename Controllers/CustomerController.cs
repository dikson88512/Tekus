using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppTekus.IServices;
using WebAppTekus.Models;

namespace WebAppTekus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private  IServicesCustomer _IServicesCustomer;

        public CustomerController(IServicesCustomer IServicesCustomer)
        {
            _IServicesCustomer = IServicesCustomer;

        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<Customer>> Get()
        {
            return await _IServicesCustomer.GetListCustomer();
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Customer> Get(int id)
        {
            return await _IServicesCustomer.GetCustomer(id);
        }


        // POST: api/Default
        [HttpPost]
        public async Task<Customer> Post([FromBody] Customer oCustumer)
        {
            if (ModelState.IsValid) return await _IServicesCustomer.SaveCustomer(oCustumer);
            return null;
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Customer oCustumer)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await _IServicesCustomer.DeleteCustomer(id);
        }
    }
}
