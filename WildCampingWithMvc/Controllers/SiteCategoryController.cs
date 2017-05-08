using CommonUtilities.Utilities;
using Services.DataProviders;
using Services.Models;
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
            model.Id = category.Id;
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
        [Authorize(Roles ="Admin")]
        public ActionResult AddSiteCategory()
        {
            return this.View();
        }

        // POST: AddSiteCategory
        [HttpPost]
        [Authorize(Roles ="Admin")]
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

        // GET: EditSiteCategory
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult EditSiteCategory(Guid id)
        {
            AddSiteCategoryViewModel model = this.ConvertToAddFromISiteCategory(id);

            return this.View(model);
        }

        // POST: EditSiteCategory
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditSiteCategory(AddSiteCategoryViewModel model, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.UpdateSiteCategory(model, id);

            return RedirectToAction("SiteCategoryDetails", new { id = id });
        }

        [Authorize(Roles ="Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteSiteCategory(Guid id)
        {
            this.siteCategoryDataProvider.DeleteSiteCategory(id);

            return RedirectToAction("Index");
        }

        private void UpdateSiteCategory(AddSiteCategoryViewModel model, Guid id)
        {
            byte[] imageFileData = Utilities.ConvertFromImage(model.ImageFileData);
            this.siteCategoryDataProvider.UpdateSiteCategory(
                id, model.Name, model.Description, imageFileData);
        }

        private AddSiteCategoryViewModel ConvertToAddFromISiteCategory(Guid id)
        {
            ISiteCategory catModel = this.siteCategoryDataProvider.GetSiteCategoryById(id);
            AddSiteCategoryViewModel viewModel = new AddSiteCategoryViewModel();

            viewModel.Description = catModel.Description;
            viewModel.Name = catModel.Name;
            viewModel.ImageFileName = catModel.Name;
            viewModel.ImageFileData = Utilities.ConvertToImage(catModel.Image);

            return viewModel;
        }

        private void AddCategory(AddSiteCategoryViewModel model)
        {
            byte[] imageFileData = Utilities.ConvertFromImage(model.ImageFileData);
            this.siteCategoryDataProvider.AddSiteCategory(
                model.Name, model.Description, imageFileData);
        }
    }
}