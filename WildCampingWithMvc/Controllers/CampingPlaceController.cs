using CommonUtilities.Utilities;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using WildCampingWithMvc.Models.CampingPlace;

namespace WildCampingWithMvc.Controllers
{
    [Authorize]
    public class CampingPlaceController : Controller
    {
        protected readonly ICampingPlaceDataProvider campingPlaceProvider;
        protected readonly ISightseeingDataProvider sightseeingProvider;
        protected readonly ISiteCategoryDataProvider siteCategoryProvider;

        public CampingPlaceController(
            ICampingPlaceDataProvider campingPlaceProvider,
            ISightseeingDataProvider sightseeingProvider,
            ISiteCategoryDataProvider siteCategoryProvider)
        {
            if (campingPlaceProvider == null)
            {
                throw new ArgumentNullException("CampingPlaceProvider");
            }

            if (siteCategoryProvider == null)
            {
                throw new ArgumentNullException("SiteCategoryProvider");
            }

            if (sightseeingProvider == null)
            {
                throw new ArgumentNullException("SightseeingProvider");
            }

            this.campingPlaceProvider = campingPlaceProvider;
            this.siteCategoryProvider = siteCategoryProvider;
            this.sightseeingProvider = sightseeingProvider;
        }

        //public ActionResult CampingPlaceDetails(CampingPlace model, Guid id)
        //{
        //    model = (CampingPlace)this.campingPlaceProvider.GetCampingPlaceById(id).First();

        //    return this.View(model);
        //}

        public ActionResult CampingPlaceDetails(CampingPlaceDetailsViewModel model, Guid id)
        {
            model = this.ConvertToDetailsFromICampingPlace(id);

            return this.View(model);
        }

        // GET: AddCampingPlace
        [HttpGet]
        public ActionResult AddCampingPlace()
        {
            this.CacheSiteCategoriesAndSightseeings();

            return this.View();
        }

        // POST: AddCampingPlace
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCampingPlace(AddCampingPlaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.AddCampPlace(model);

            return RedirectToAction("Index", "Home");
        }

        // GET: EditCampingPlace
        [HttpGet]
        public ActionResult EditCampingPlace(AddCampingPlaceViewModel model, Guid id)
        {
            this.CacheSiteCategoriesAndSightseeings();
            model = this.ConvertToAddFromICampingPlace(id);
            ModelState.Clear();
            TempData["Id"] = id;

            return this.View(model);
        }

        // POST: EditCampingPlace
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCampingPlace(AddCampingPlaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            Guid id = (Guid)TempData["Id"];

            this.UpdateCampPlace(model, id);

            return RedirectToAction("Index", "Home");
        }

        // GET: CampingPlace
        public ActionResult Index()
        {
            return this.View();
        }

        private void UpdateCampPlace(AddCampingPlaceViewModel model, Guid id)
        {
            IList<byte[]> imageFilesData = this.ConvertImageData(model.ImageFilesData);
            this.campingPlaceProvider.UpdateCampingPlace(
                id, model.Name, model.Description, model.GoogleMapsUrl,
                model.HasWater, model.SightseeingNames, model.SiteCategoriesNames,
                model.ImageFileNames, imageFilesData);
        }

        private AddCampingPlaceViewModel ConvertToAddFromICampingPlace(Guid id)
        {
            ICampingPlace cpModel = this.campingPlaceProvider.GetCampingPlaceById(id).First();
            AddCampingPlaceViewModel viewModel = new AddCampingPlaceViewModel();

            viewModel.Description = cpModel.Description;
            viewModel.GoogleMapsUrl = cpModel.GoogleMapsUrl;
            viewModel.HasWater = cpModel.HasWater;
            viewModel.Name = cpModel.Name;

            IList<string> sightseeingNames = new List<string>();
            foreach (var sightseeingId in cpModel.SightseeingIds)
            {
                ISightseeing sightseeing = this.sightseeingProvider.GetSightseeingById(sightseeingId);
                sightseeingNames.Add(sightseeing.Name);
            }

            viewModel.SightseeingNames = sightseeingNames;

            IList<string> siteCategoryNames = new List<string>();
            foreach (var siteCategoryId in cpModel.SiteCategoriesIds)
            {
                ISiteCategory siteCategory = this.siteCategoryProvider.GetSiteCategoryById(siteCategoryId);
                siteCategoryNames.Add(siteCategory.Name);
            }

            viewModel.SiteCategoriesNames = siteCategoryNames;

            IList<string> imageFileNames = new List<string>();
            IList<string> imageFilesData = new List<string>();
            foreach (var image in cpModel.ImageFiles)
            {
                string imageFileName = image.FileName;
                imageFileNames.Add(imageFileName);

                byte[] imageFileData = image.Data;
                imageFilesData.Add(Utilities.ConvertToImage(imageFileData));
            }

            viewModel.ImageFileNames = imageFileNames;
            viewModel.ImageFilesData = imageFilesData;

            return viewModel;
        }

        private CampingPlaceDetailsViewModel ConvertToDetailsFromICampingPlace(Guid id)
        {
            ICampingPlace cpModel = this.campingPlaceProvider.GetCampingPlaceById(id).First();
            CampingPlaceDetailsViewModel viewModel = new CampingPlaceDetailsViewModel();

            viewModel.AddedBy = cpModel.AddedBy;
            viewModel.AddedOn = cpModel.AddedOn;
            viewModel.Description = cpModel.Description;
            viewModel.GoogleMapsUrl = cpModel.GoogleMapsUrl;
            viewModel.HasWater = cpModel.HasWater;
            viewModel.Name = cpModel.Name;
            viewModel.Id = cpModel.Id;

            IList<ISightseeing> sightseeings = new List<ISightseeing>();
            foreach (var sightseeingId in cpModel.SightseeingIds)
            {
                ISightseeing sightseeing = this.sightseeingProvider.GetSightseeingById(sightseeingId);
                sightseeings.Add(sightseeing);
            }

            viewModel.Sightseeings = sightseeings;

            IList<ISiteCategory> siteCategories = new List<ISiteCategory>();
            foreach (var siteCategoryId in cpModel.SiteCategoriesIds)
            {
                ISiteCategory siteCategory = this.siteCategoryProvider.GetSiteCategoryById(siteCategoryId);
                siteCategories.Add(siteCategory);
            }

            viewModel.SiteCategories = siteCategories;

            IList<string> imageFileNames = new List<string>();
            IList<byte[]> imageFilesData = new List<byte[]>();
            foreach (var image in cpModel.ImageFiles)
            {
                string imageFileName = image.FileName;
                imageFileNames.Add(imageFileName);

                byte[] imageFileData = image.Data;
                imageFilesData.Add(imageFileData);
            }

            viewModel.ImageFileNames = imageFileNames;
            viewModel.ImageFilesData = imageFilesData;

            return viewModel;
        }

        private void AddCampPlace(AddCampingPlaceViewModel model)
        {
            IList<byte[]> imageFilesData = this.ConvertImageData(model.ImageFilesData);
            string addedBy = this.User.Identity.Name;
            this.campingPlaceProvider.AddCampingPlace(
                model.Name, addedBy, model.Description, model.GoogleMapsUrl,
                model.HasWater, model.SightseeingNames, model.SiteCategoriesNames,
                model.ImageFileNames, imageFilesData);
        }

        private IList<byte[]> ConvertImageData(IList<string> imageBase64StrDataList)
        {
            IList<byte[]> imageFilesData = new List<byte[]>();
            foreach (var stringItem in imageBase64StrDataList)
            {
                var imgData = Utilities.ConvertFromImage(stringItem);
                imageFilesData.Add(imgData);
            }

            return imageFilesData;
        }

        private void CacheSiteCategoriesAndSightseeings()
        {
            IList<ISiteCategory> allSiteCategories = (IList<ISiteCategory>)this.HttpContext.Cache["AllSiteCategories"];
            if (allSiteCategories == null)
            {
                allSiteCategories = (IList<ISiteCategory>)this.siteCategoryProvider.GetAllSiteCategories();
                this.HttpContext.Cache["AllSiteCategories"] = allSiteCategories;

            }

            IList<ISightseeing> allSightseeings = (IList<ISightseeing>)this.HttpContext.Cache["AllSightseeings"];
            if (allSightseeings == null)
            {
                allSightseeings = (IList<ISightseeing>)this.sightseeingProvider.GetAllSightseeings();
                this.HttpContext.Cache["AllSightseeings"] = allSightseeings;

            }
        }
    }
}