using System.Linq;
using System.Net;
using System.Web.Mvc;
using REERP.Models;
using REERP.Sales.Services;

namespace REERP.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(_customerService.GetAllCustomers().ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.FindById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,FirstName,MiddleName,LastName,Address,TelephoneNo,TIN,VATNumber,Trusted")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var exists = _customerService.Get(t => t.FirstName == customer.FirstName && t.LastName == customer.LastName).Count();
                if (exists > 0)
                {
                    ViewBag.Message = "Customer with the same First and Last name already exists.";
                    return RedirectToAction("Create");
                }
                _customerService.AddCustomer(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.FindById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,MiddleName,LastName,Address,TelephoneNo,TIN,VATNumber,Trusted")] Customer customer)
        {
            if (ModelState.IsValid)
            {                   
                _customerService.EditCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var customer = _customerService.FindById(id);
            _customerService.DeleteCustomer(customer);
            return RedirectToAction("Index", "Customers");
        }

        // POST: Customers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = _customerService.FindById(id);
            _customerService.DeleteCustomer(customer);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
