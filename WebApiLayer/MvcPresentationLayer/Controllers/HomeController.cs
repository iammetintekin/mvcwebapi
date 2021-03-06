using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Employee> employees = GetEmployee();
            return View(employees);
        }

        public ActionResult Get(int id)
        {
            Employee employee = GetEmployeeByID(id);
            List<Employee> employees = new List<Employee>();
            employees.Add(employee);
            

            return View("Index",employees);
        }

        Employee GetEmployeeByID(int id)
        {
            Employee employee = null;
            using(HttpClient client = new HttpClient())
            {
                string url = "http://localhost:9111/api/Employee?id="+id;

                string input = "test:123";
                byte[] array = Encoding.ASCII.GetBytes(input);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Convert.ToBase64String(array));


                Task<HttpResponseMessage> result = client.GetAsync(url);
                if (result.Result.IsSuccessStatusCode)
                {
                    Task<string> serializedResult = result.Result.Content.ReadAsStringAsync();
                    employee = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(serializedResult.Result);
                }
            }
            return employee;
        }

        IEnumerable<Employee> GetEmployee()
        {
            IEnumerable<Employee> employees = null;
            using(HttpClient client = new HttpClient())
            {
                string url = "http://localhost:9111/api/Employee";
                Uri uri = new Uri(url);

                string input = "test:123";
                byte[] array = Encoding.ASCII.GetBytes(input);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Convert.ToBase64String(array));

                Task<HttpResponseMessage> result = client.GetAsync(url);
                if (result.Result.IsSuccessStatusCode)
                {
                    Task<string> response = result.Result.Content.ReadAsStringAsync();
                    employees = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Employee>>(response.Result);
                }
            }
            return employees;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}