using Microsoft.AspNetCore.Mvc;
using SampleTask.Data;
using SampleTask.Models;

namespace SampleTask.Controllers
{
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<City> cityList = _context.Cities;
            return View(cityList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Cities.Add(city);
                    _context.SaveChanges();
                    TempData["SuccessMsg"] = "City (" + city.CityName + ") added successfully.";
                    return RedirectToAction("Index", "City");
                }
            }
            catch (Exception)
            {

                TempData["ErrorMsg"] = "City (" + city.CityName + ") could not be added.";
                return RedirectToAction("Index", "City");
            }
           
            return View(city);
        }

        public IActionResult Edit(int? cityId)
        {            
            var city = _context.Cities.Find(cityId);

            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Cities.Update(city);
                    _context.SaveChanges();
                    TempData["SuccessMsg"] = "City (" + city.CityName + ") updated successfully.";
                    return RedirectToAction("Index", "City");
                }
            }
            catch (Exception)
            {

                TempData["ErrorMsg"] = "City (" + city.CityName + ") could not be updated.";
                return RedirectToAction("Index", "City");
            }
            
            return View(city);
        }

        public IActionResult Delete(int? cityId)
        {
            var city = _context.Cities.Find(cityId);

            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCity(int? Id)
        {
            try
            {
                var city = _context.Cities.Find(Id);
                if (city == null)
                {
                    return NotFound();
                }
                _context.Cities.Remove(city);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "City (" + city.CityName + ") deleted successfully.";
                return RedirectToAction("Index", "City");
            }
            catch (Exception)
            {

                TempData["ErrorMsg"] = "This city could not be deleted.";
                return RedirectToAction("Index", "City");
            }
            
        }
    }
}
