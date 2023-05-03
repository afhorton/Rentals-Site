using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalsData;
using RentalsMVC.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace RentalsMVC.Controllers
{
    public class RentalsController : Controller
    {
        // GET: RentalsController
        public ActionResult Index()
        {
            List<RentalProperty> rentals = RentalsManager.GetAll();
            List<RentalViewModel> viewModels = rentals.Select(r => new RentalViewModel { 
                Address = r.Address,
                City = r.City,
                Province = r.Province,
                Rent = r.Rent.ToString("c"),
                PropertyType= r.PropertyType.Style,
                Owner = r.Owner.Name
            }).ToList();
            return View(viewModels);
        }

        // GET: RentalsController/List
        [Authorize(Roles="Owner")]
        public ActionResult List()
        {
            // filter rentals by current owner
            List<RentalProperty> rentals = null;
            // get the current owner id from the session state
            int? ownerId = HttpContext.Session.GetInt32("CurrentOwner");
            if(ownerId == null)
            {
                ownerId = 0;
            }

            rentals = RentalsManager.GetPropertiesByOwner((int)ownerId);
            return View(rentals);
        }

        // GET: RentalsController/Create
        [Authorize(Roles ="Owner")]
        public ActionResult Create()
        {
            int? ownerId = HttpContext.Session.GetInt32("CurrentOwner");
            if(ownerId != null)
            {
                ViewBag.OwnerId = (int)ownerId;
            }

            ViewBag.PropertyTypes = GetPropertyTypes(); // property types to populate drop down
            return View(new RentalProperty());
        }

        // POST: RentalsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult Create(RentalProperty rental) // data collected from the form that is coming // comes as iform collection interface
        {
            if (ModelState.IsValid) // if data from the form passed validation
                                    // from RentalProperty
            {
                try
                {
                    RentalsManager.Add(rental);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(); // stay on Create page
                } 
            }
            else
            {
                return View(); // stay on Create page // Create page always returns empty view.
            }
        }   

        // auxilliary method for Create 
        protected IEnumerable GetPropertyTypes()
        {
            // call property types manager to get key/value pairs for property types drop down list 
            var types = PropertyTypesManager.GetAllAsKeyValuePairs();
            // convert it to a form that drop down list can use, and add to the bag
            var styles = new SelectList(types, "Value", "Text");
            var list = styles.ToList(); // to be able to use Insert method
            list.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "All Styles"
            });
            return list;
        }
    }
}
