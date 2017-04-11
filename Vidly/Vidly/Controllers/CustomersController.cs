using System.Web.Mvc;
using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.Controllers
{
  public class CustomersController : Controller
  {
    // GET: Customers
    public ViewResult Index()
    {
      var customers = new List<Customer>()
      {
        new Customer() { Name = "John Smith" },
        new Customer() { Name = "Mary Williams" }
      };

      return View( customers );
    }
  }
}