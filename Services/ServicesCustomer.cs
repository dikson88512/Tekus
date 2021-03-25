using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTekus.IServices;
using WebAppTekus.Models;
using WebAppTekus.Common;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace WebAppTekus.Services
{
    public class ServicesCustomer : IServicesCustomer
    {
        List<Customer> oCustomerList = new List<Customer>();
        Customer _oCustomer = new Customer();
        public async Task<string> DeleteCustomer(int idCustomer)
        {
            string message = ""; 

            
            
            try

            { 
                _oCustomer = new Customer()
                {
                    IDCustomer= idCustomer
                };
                using (SqlConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        await con.OpenAsync();
                    }
                    var oCustomers = con.Query<Customer>("SP_CUSTOMER",
                        this.SetParameters(_oCustomer, 3),
                        commandType: CommandType.StoredProcedure);

                    if (oCustomers != null && oCustomers.Count() > 0)
                    {
                        _oCustomer = oCustomers.FirstOrDefault();
                    }
                };
            }
            catch (Exception e)
            {

                message = e.Message.ToString();
            }

            return message;
        }

        public async Task<Customer> GetCustomer(int idCustomer)
        {
            Customer oCustomer = new Customer();

            //IDbConnection
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    await con.OpenAsync();
                }
                var _oCustomers = con.Query<Customer>("SELECT * FROM CUSTOMERS WHERE IDCustomer = " + idCustomer).ToList();

                if (_oCustomers != null && _oCustomers.Count() > 0)
                {
                    _oCustomer = _oCustomers.SingleOrDefault();
                }
            };

            return oCustomer;
        }

        public async Task<List<Customer>> GetListCustomer()
        {
            List<Customer>  oCustomers = new List<Customer>();
            
            //IDbConnection
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    await con.OpenAsync();
                }
                var _oCustomers = con.Query<Customer>("SELECT * FROM CUSTOMERS").ToList();
                oCustomers = _oCustomers;
                //if (_oCustomers != null && _oCustomers.Count() > 0)
                //{
                //    oCustomers = _oCustomers.FirstOrDefault();
                //}
                //else { oCustomers = _oCustomers; }

            };

           return oCustomers;
           
        }

        public async Task<Customer> SaveCustomer(Customer oCustomer)
        {
            _oCustomer = new Customer();
            try
            {
                int operationType = Convert.ToInt32(oCustomer.IDCustomer == 0 ? TypeOperation.Insert : TypeOperation.Update);
                //IDbConnection
                using (SqlConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                      await  con.OpenAsync();
                    }
                    var oCustomers = con.Query<Customer>("SP_CUSTOMER", 
                        this.SetParameters(oCustomer, operationType),
                        commandType: CommandType.StoredProcedure);

                    if ( oCustomers != null && oCustomers.Count()> 0)
                    {
                        _oCustomer = oCustomers.FirstOrDefault();
                    }
                };
            }
            catch (Exception e)
            {

                oCustomer.Message = e.Message.ToString();
            }
            return oCustomer;
        }

        private DynamicParameters SetParameters(Customer oCustomer, int operationType)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NIT", oCustomer.NIT);
                parameters.Add("@NameCustomer", oCustomer.NameCustomer);
                parameters.Add("@EmailCustomer", oCustomer.EmailCustomer);
                return parameters;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
