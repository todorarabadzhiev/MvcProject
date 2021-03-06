﻿using CommonUtilities.Utilities;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using WildCampingWithMvc.Models.CampingPlace;
using WildCampingWithMvc.Utils;

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

        [HttpPost]
        [AjaxOnly]
        public ActionResult FilteredCampingPlaces(string searchTerm)
        {
            MultipleCampingPlacesViewModel model = new MultipleCampingPlacesViewModel();
            IEnumerable<ICampingPlace> foundCampingPlaces;
            if (string.IsNullOrEmpty(searchTerm))
            {
                foundCampingPlaces = this.campingPlaceProvider.GetAllCampingPlaces();
            }
            else
            {
                foundCampingPlaces = this.campingPlaceProvider.GetCampingPlacesBySearchName(searchTerm);
            }

            model.CampingPlaces = foundCampingPlaces;
            return this.PartialView("_MultipleCampingPlacesPartial", model);
        }

        public ActionResult CampingPlaceDetails(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewBag.NoPlaceFound = false;
            CampingPlaceDetailsViewModel model = this.ConvertToDetailsFromICampingPlace((Guid)id);
            if (model == null)
            {
                this.ViewBag.NoPlaceFound = true;
            }

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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.AddCampPlace(model);

            return RedirectToAction("Index", "Home");
        }

        // GET: EditCampingPlace
        [HttpGet]
        public ActionResult EditCampingPlace(Guid id)
        {
            this.CacheSiteCategoriesAndSightseeings();
            bool isAuthorized = (bool)TempData["isAuthorized"];
            if (!isAuthorized)
            {
                return this.RedirectToAction("CampingPlaceDetails", new { id = id });
            }

            AddCampingPlaceViewModel model = this.ConvertToAddFromICampingPlace(id);

            return this.View(model);
        }

        // POST: EditCampingPlace
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCampingPlace(AddCampingPlaceViewModel model, Guid id)
        {
            bool isAuthorized = (bool)TempData["isAuthorized"];
            if (!isAuthorized)
            {
                return this.RedirectToAction("CampingPlaceDetails", new { id = id });
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.UpdateCampPlace(model, id);

            return RedirectToAction("CampingPlaceDetails", new { id = id });
        }

        public ActionResult DeleteCampingPlace(Guid id)
        {
            bool isAuthorized = (bool)TempData["isAuthorized"];
            if (isAuthorized)
            {
                this.campingPlaceProvider.DeleteCampingPlace(id);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RecoverCampingPlace(Guid id)
        {
            this.campingPlaceProvider.RecoverDeletedCampingPlaceById(id);

            return RedirectToAction("CampingPlaceDetails", new { id = id });
        }
        
        public ActionResult Index()
        {
            return RedirectToAction("AllCampingPlaces");
        }

        // GET: CampingPlace
        [HttpGet]
        public ActionResult AllCampingPlaces()
        {
            var places = this.campingPlaceProvider.GetAllCampingPlaces();
            MultipleCampingPlacesViewModel model = new MultipleCampingPlacesViewModel();
            model.CampingPlaces = places;

            return View(model);
        }

        // GET: DeletedCampingPlace
        [HttpGet]
        public ActionResult DeletedCampingPlaces()
        {
            var places = this.campingPlaceProvider.GetDeletedCampingPlaces();
            MultipleCampingPlacesViewModel model = new MultipleCampingPlacesViewModel();
            model.CampingPlaces = places;

            return View(model);
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
            IEnumerable<ICampingPlace> cpsFound = this.campingPlaceProvider.GetCampingPlaceById(id);
            if (cpsFound == null)
            {
                return null;
            }

            ICampingPlace cpModel = cpsFound.First();
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
            IList<ISiteCategory> allSiteCategories =
                (IList<ISiteCategory>)this.ControllerContext.HttpContext.Cache["AllSiteCategories"];
            if (allSiteCategories == null)
            {
                allSiteCategories = (IList<ISiteCategory>)this.siteCategoryProvider.GetAllSiteCategories();
                this.ControllerContext.HttpContext.Cache.Add("AllSiteCategories", allSiteCategories, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.Normal, null);
            }

            IList<ISightseeing> allSightseeings =
                (IList<ISightseeing>)this.ControllerContext.HttpContext.Cache["AllSightseeings"];
            if (allSightseeings == null)
            {
                allSightseeings = (IList<ISightseeing>)this.sightseeingProvider.GetAllSightseeings();
                this.ControllerContext.HttpContext.Cache.Add("AllSightseeings", allSightseeings, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), CacheItemPriority.Normal, null);
            }
        }
    }
}