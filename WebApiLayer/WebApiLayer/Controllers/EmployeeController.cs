using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiLayer.Controllers
{
    public class EmployeeController : ApiController
    {
        //http://localhost:9111/api/Employee

        List<Employee> employees;
        public EmployeeController()
        {
            employees = new List<Employee>();
            employees.Add(new Employee
            {
                Id = 1,
                Name = "Metin",
                ContactNumber = 6546455,
                Address = "Aydın"
            });
            employees.Add(new Employee
            {
                Id = 2,
                Name = "Servet",
                ContactNumber = 2135666,
                Address = "İzmir"
            });
        }
        // GET api/<controller>
        [Filters.CustomAuthentication]
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        // GET api/<controller>/5
        //http://localhost:9111/api/Employee/1
        [Filters.CustomAuthentication]
        public Employee Get(int id)
        {
            return employees.FirstOrDefault<Employee>(x=>x.Id.Equals(id));
        }

    }
}