﻿using CommonUtilities.Utilities;
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

        public ActionResult CampingPlaceDetails(CampingPlaceDetailsViewModel model, Guid id)
        {
            ICampingPlace cpModel = this.campingPlaceProvider.GetCampingPlaceById(id).First();
            model = this.ConvertFromICampingPlace(cpModel);
            
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

            //this.AddCampPlace(model);

            return RedirectToAction("Index", "Home");
        }

        // GET: CampingPlace
        public ActionResult Index()
        {
            return this.View();
        }


        private CampingPlaceDetailsViewModel ConvertFromICampingPlace(ICampingPlace cpModel)
        {
            CampingPlaceDetailsViewModel viewModel = new CampingPlaceDetailsViewModel();

            viewModel.AddedBy = cpModel.AddedBy;
            viewModel.AddedOn = cpModel.AddedOn;
            viewModel.Description = cpModel.Description;
            viewModel.GoogleMapsUrl = cpModel.GoogleMapsUrl;
            viewModel.HasWater = cpModel.HasWater;
            viewModel.Name = cpModel.Name;

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
                model.HasWater, model.SightseeingNames, model.SiteCategoryNames,
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