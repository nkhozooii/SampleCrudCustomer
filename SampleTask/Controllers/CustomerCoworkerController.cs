using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SampleTask.Data;
using SampleTask.Models;
using SampleTask.ViewModel;
using System.Linq;

namespace SampleTask.Controllers
{
    public class CustomerCoworkerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerCoworkerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index(int id)
        {
            List<CoworkerListViewModel> coworkerListViewModelList = new List<CoworkerListViewModel>();
            var customerList = _context.Customers.Where(c => c.Id != id);
            var customer = _context.Customers.Find(id);
            ViewBag.customer = customer;
            if (customerList != null)
            {
                foreach (var item in customerList)
                {
                    CoworkerListViewModel coworkerListViewModel = new CoworkerListViewModel();
                    coworkerListViewModel.Id = item.Id;
                    coworkerListViewModel.Name = item.Name;
                    if (_context.CustomerCoworkers.Where(c => c.FkCustomerId == id && c.FkCoworkerId == item.Id).ToList().Count != 0)
                        coworkerListViewModel.IscoWorker = true;
                    else
                        coworkerListViewModel.IscoWorker = false;

                    coworkerListViewModelList.Add(coworkerListViewModel);

                }
            }
            return View(coworkerListViewModelList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitCoworkers(List<CoworkerListViewModel> Customers,int id)
        {
            foreach (CoworkerListViewModel customer in Customers)
            {                
                var coworkerRecord = _context.CustomerCoworkers.ToList().Find(p => p.FkCoworkerId == customer.Id && p.FkCustomerId == id);
                if (customer.IscoWorker)
                {
                    if(coworkerRecord == null)
                    {
                        var coworker = new CustomerCoworker()
                        {
                           FkCustomerId=id,
                           FkCoworkerId=customer.Id
                        };
                        _context.CustomerCoworkers.Add(coworker);
                        _context.SaveChanges();

                        ////
                        var Siblingcoworker = new CustomerCoworker()
                        {
                            FkCustomerId = customer.Id,
                            FkCoworkerId = id
                        };
                        _context.CustomerCoworkers.Add(Siblingcoworker);
                        _context.SaveChanges();

                    }
                }
                else if (!customer.IscoWorker)
                {
                    if (coworkerRecord != null)
                    {
                        if (coworkerRecord == null)
                        {
                            return NotFound();
                        }
                        _context.CustomerCoworkers.Remove(coworkerRecord);
                        _context.SaveChanges();
                        ///
                        var Siblingcoworker = _context.CustomerCoworkers.ToList().Find(p => p.FkCoworkerId ==id  && p.FkCustomerId == customer.Id);
                        if (Siblingcoworker != null)
                        {
                            _context.CustomerCoworkers.Remove(Siblingcoworker);
                            _context.SaveChanges();
                        }

                    }
                }
                
            }
            TempData["SuccessMsg"] = "Coworkers for  (" + _context.Customers.Find(id).Name + ") added successfully.";
            return RedirectToAction("Index","Customer");

        }
    }
}
