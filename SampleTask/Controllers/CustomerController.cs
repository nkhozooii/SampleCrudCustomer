using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleTask.Data;
using SampleTask.Models;
using SampleTask.ViewModel;

namespace SampleTask.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CustomerListViewModel> customerListViewModelList = new List<CustomerListViewModel>();
            var customerList = _context.Customers;
            if (customerList != null)
            {
                foreach (var item in customerList)
                {
                    CustomerListViewModel customerListViewModel = new CustomerListViewModel();
                    customerListViewModel.Id = item.Id;
                    customerListViewModel.Name = item.Name;
                    customerListViewModel.Phone = item.Phone;
                    customerListViewModel.Fax = item.Fax;
                    customerListViewModel.CityId = item.CityId;
                    customerListViewModel.Address = item.Address;
                    customerListViewModel.CityName = _context.Cities.Where(c => c.Id == item.CityId).Select(c => c.CityName).FirstOrDefault();
                    customerListViewModelList.Add(customerListViewModel);
                }
            }
            return View(customerListViewModelList);           
        }

        public IActionResult Create()
        {
            CustomerViewModel customerCreateViewModel = new CustomerViewModel();
            customerCreateViewModel.City = (IEnumerable<SelectListItem>)_context.Cities.Select(c => new SelectListItem()
            {
                Text = c.CityName,
                Value = c.Id.ToString()
            });

            return View(customerCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerViewModel customerCreateViewModel)
        {
            try
            {
                customerCreateViewModel.City = (IEnumerable<SelectListItem>)_context.Cities.Select(c => new SelectListItem()
                {
                    Text = c.CityName,
                    Value = c.Id.ToString()
                });
                var customer = new Customer()
                {
                    Name = customerCreateViewModel.Name,
                    Phone = customerCreateViewModel.Phone,
                    Fax = customerCreateViewModel.Fax,
                    Address = customerCreateViewModel.Address,
                    CityId = customerCreateViewModel.CityId
                };
                ModelState.Remove("City");
                if (ModelState.IsValid)
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    TempData["SuccessMsg"] = "customer (" + customer.Name + ") added successfully.";
                    return RedirectToAction("Index", "Customer");
                }
            }
            catch (Exception)
            {
                TempData["ErrorMsg"] = "An error occurred while adding a user.";
                return RedirectToAction("Index", "Customer");
            }
            

            return View(customerCreateViewModel);
        }

        public IActionResult Edit(int? id)
        {
            var customerToEdit = _context.Customers.Find(id);
            if (customerToEdit != null)
            {
                var customerViewModel = new CustomerViewModel()
                {
                    Id = customerToEdit.Id,
                    Name = customerToEdit.Name,
                    Address = customerToEdit.Address,
                    Phone = customerToEdit.Phone,
                    CityId = customerToEdit.CityId,
                    Fax = customerToEdit.Fax,
                    City = (IEnumerable<SelectListItem>)_context.Cities.Select(c => new SelectListItem()
                    {
                        Text = c.CityName,
                        Value = c.Id.ToString()                        
                    })
                };
                return View(customerViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerViewModel customerViewModel)
        {
            customerViewModel.City = (IEnumerable<SelectListItem>)_context.Cities.Select(c => new SelectListItem()
            {
                Text = c.CityName,
                Value = c.Id.ToString()
            });
            var customer = new Customer()
            {
                Id = customerViewModel.Id,
                Name = customerViewModel.Name,
                Address = customerViewModel.Address,
                Phone = customerViewModel.Phone,
                Fax = customerViewModel.Fax,
                CityId = customerViewModel.CityId,
            };
            try
            {                
                ModelState.Remove("City");
                if (ModelState.IsValid)
                {
                    _context.Customers.Update(customer);
                    _context.SaveChanges();
                    TempData["SuccessMsg"] = "customer (" + customer.Name + ") updated successfully !";
                    return RedirectToAction("Index", "Customer");
                }
            }
            catch (Exception)
            {

                TempData["ErrorMsg"] = "An error occurred while updating user (" + customer.Name + ").";
                return RedirectToAction("Index", "Customer");
            }

            return View(customerViewModel);
        }
        public IActionResult Delete(int? id)
        {
            var customerToEdit = _context.Customers.Find(id);
            if (customerToEdit != null)
            {
                var customerViewModel = new CustomerViewModel()
                {
                    Id = customerToEdit.Id,
                    Name = customerToEdit.Name,
                    Address = customerToEdit.Address,
                    Phone = customerToEdit.Phone,
                    CityId = customerToEdit.CityId,
                    Fax = customerToEdit.Fax,
                    City = (IEnumerable<SelectListItem>)_context.Cities.Select(c => new SelectListItem()
                    {
                        Text = c.CityName,
                        Value = c.Id.ToString()
                    })
                };
                return View(customerViewModel);
            }
            else {
                return RedirectToAction("Index", "Customer");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletecustomer(int? id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound();
                }
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "customer (" + customer.Name + ") deleted successfully.";
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception)
            {

                TempData["ErrorMsg"] = "An error occurred while deleting user .";
                return RedirectToAction("Index", "Customer");
            }
           
        }
    }
}
