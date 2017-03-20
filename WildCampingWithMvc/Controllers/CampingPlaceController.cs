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

        // GET: CampingPlace
        public ActionResult Index()
        {
            return this.View();
        }

        private void AddCampPlace(AddCampingPlaceViewModel model)
        {
            string bas = "base64,";
            IList<string> imageFileNames = model.ImageFileNames;
            IList<byte[]> imageFilesData = new List<byte[]>();
            foreach (var stringItem in model.ImageFilesData)
            {
                string strItem = stringItem.Substring(stringItem.IndexOf(bas) + bas.Length);
                byte[] dataItem = Convert.FromBase64String(strItem);
                imageFilesData.Add(dataItem);
            }

            string addedBy = this.User.Identity.Name;
            this.campingPlaceProvider.AddCampingPlace(
                model.Name, addedBy, model.Description, model.GoogleMapsUrl,
                model.HasWater, model.SightseeingNames, model.SiteCategoryNames,
                imageFileNames, imageFilesData);
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