using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Vidly.Models;

namespace Vidly.Controllers
{
  public class CustomersController : Controller
  {
    static List<Customer> Customers = new List<Customer>()
    {
      new Customer() { Id = 1, Name = "John Smith" },
      new Customer() { Id = 2, Name = "Mary Williams" }
    };

    public ViewResult Index()
    {
      return View( Customers );
    }

    [Route( "Customers/Details/{id}" )]
    public ActionResult Details( int id )
    {
      Customer customer = Customers.FirstOrDefault( m => m.Id == id );

      if( customer == null )
      {
        return HttpNotFound();
      }
      
      return View( customer );
    }
  }
}