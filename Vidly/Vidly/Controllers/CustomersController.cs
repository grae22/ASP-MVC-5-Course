using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
  public class CustomersController : Controller
  {

    ApplicationDbContext _context;

    public CustomersController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose( bool disposing )
    {
      _context.Dispose();
    }

    [Route( "Customers/New" )]
    public ActionResult New()
    {
      var membershipTypes = _context.MembershipTypes.ToList();

      var viewModel = new NewCustomerViewModel
      {
        MembershipTypes = membershipTypes,
      };

      return View( viewModel );
    }

    [HttpPost]
    public ActionResult Create( Customer customer )
    {
      return View();
    }

    public ViewResult Index()
    {
      var customers =
        _context
          .Customers
          .Include( c => c.MembershipType )
          .ToList();

      return View( customers );
    }

    [Route( "Customers/Details/{id}" )]
    public ActionResult Details( int id )
    {
      Customer customer =
        _context
          .Customers
          .Include( c => c.MembershipType )
          .FirstOrDefault( m => m.Id == id );

      if( customer == null )
      {
        return HttpNotFound();
      }
      
      return View( customer );
    }
  }
}