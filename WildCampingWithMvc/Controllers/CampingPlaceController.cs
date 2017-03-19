using Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            this.sightseeingProvider = sightseeingProvider;
            this.siteCategoryProvider = siteCategoryProvider;
        }

        // POST: AddCampingPlace
        public ActionResult AddCampingPlace(AddCampingPlaceViewModel model)
        {
            return View();
        }

        // GET: CampingPlace
        public ActionResult Index()
        {
            return View();
        }

        //private void View_AddCampingPlaceClick(object sender, AddCampingPlaceClickEventArgs e)
        //{
        //    this.campingPlaceProvider.AddCampingPlace(e.Name, e.AddedBy, e.Description, e.GoogleMapsUrl,
        //        e.HasWater, e.SightseeingNames, e.SiteCategoryNames,
        //        e.ImageFileNames, e.ImageFilesData);
        //}

        //private void View_AddCampingPlaceLoad(object sender, EventArgs e)
        //{
        //    this.View.Model.SiteCategories = this.siteCategoryProvider.GetAllSiteCategories();
        //    this.View.Model.Sightseeings = this.sightseeingProvider.GetAllSightseeings();
        //}
    }
}