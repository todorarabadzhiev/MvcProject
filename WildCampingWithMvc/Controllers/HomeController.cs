using Ninject;
using Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WildCampingWithMvc.Models.Home;

namespace WildCampingWithMvc.Controllers
{
    public class HomeController : Controller
    {
        private const int CountOfLastPlaces = 6;
        private readonly ICampingPlaceDataProvider campPlaceDataProvider;


        public HomeController(ICampingPlaceDataProvider campPlaceDataProvider)
        {
            this.campPlaceDataProvider = campPlaceDataProvider;
        }

        public ActionResult Index(IndexViewModel model)
        {
            var places = this.campPlaceDataProvider.GetLatestCampingPlaces(CountOfLastPlaces);

            model.LastCampingPlaces = places;

            return View(model);
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}