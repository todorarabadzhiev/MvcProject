using CommonUtilities.Utilities;
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

        // GET: SiteCategoryDetails
        [HttpGet]
        public ActionResult SiteCategoryDetails(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var category = this.siteCategoryDataProvider.GetSiteCategoryById((Guid)id);
            if (category == null)
            {
                return this.RedirectToAction("Index");
            }

            SiteCategoryDetailsViewModel model = new SiteCategoryDetailsViewModel();
            model.Name = category.Name;
            model.Description = category.Description;
            var imageData = Utilities.ConvertToImage(category.Image);
            model.ImageData = imageData == Utilities.NoImage ? null : imageData;
            var categoryPlaces = this.campingPlaceProvider.GetSiteCategoryCampingPlaces(category.Name);
            model.Places = new MultipleCampingPlacesViewModel();
            model.Places.CampingPlaces = categoryPlaces;

            return View(model);
        }

        // GET: AddSiteCategory
        [HttpGet]
        public ActionResult AddSiteCategory()
        {
            return this.View();
        }

        // POST: AddSiteCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSiteCategory(AddSiteCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.AddCategory(model);

            return RedirectToAction("Index");
        }

        private void AddCategory(AddSiteCategoryViewModel model)
        {
            byte[] imageFileData = Utilities.ConvertFromImage(model.ImageFileData);
            this.siteCategoryDataProvider.AddSiteCategory(
                model.Name, model.Description, imageFileData);
        }
    }
}