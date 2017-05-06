using Services.DataProviders;
using System;
using System.Web.Mvc;
using WildCampingWithMvc.Models.CampingPlace;
using WildCampingWithMvc.Models.SiteCategory;

namespace WildCampingWithMvc.Controllers
{
    public class SiteCategoryController : Controller
    {
        protected readonly ISiteCategoryDataProvider siteCategoryDataProvider;
        protected readonly ICampingPlaceDataProvider campingPlaceProvider;

        public SiteCategoryController(
            ISiteCategoryDataProvider siteCategoryDataProvider, 
            ICampingPlaceDataProvider campingPlaceProvider)
        {
            if (siteCategoryDataProvider == null)
            {
                throw new ArgumentNullException("SiteCategoryDataProvider");
            }

            if (campingPlaceProvider == null)
            {
                throw new ArgumentNullException("CampingPlaceProvider");
            }

            this.siteCategoryDataProvider = siteCategoryDataProvider;
            this.campingPlaceProvider = campingPlaceProvider;
        }

        // GET: Category
        public ActionResult Index()
        {
            var categories = this.siteCategoryDataProvider.GetAllSiteCategories();
            SiteCategoriesViewModel model = new SiteCategoriesViewModel();
            model.SiteCategories = categories;

            return View(model);
        }

        // GET: CampingPlace
        [HttpGet]
        public ActionResult SiteCategoriesDetails(string name)
        {
            var categoryPlaces = this.campingPlaceProvider.GetSiteCategoryCampingPlaces(name);
            MultipleCampingPlacesViewModel model = new MultipleCampingPlacesViewModel();
            model.CampingPlaces = categoryPlaces;
            ViewBag.CategoryName = name;

            return View(model);
        }
    }
}