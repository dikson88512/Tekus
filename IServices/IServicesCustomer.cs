using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTekus.Models;

namespace WebAppTekus.IServices
{
    public interface IServicesCustomer
    {
        Task<Customer> SaveCustomer(Customer oCustomer);

        Task<List<Customer>> GetListCustomer();
        Task<Customer> GetCustomer(int idCustomer);

        Task<string> DeleteCustomer(int idCustomer);

    }
}
