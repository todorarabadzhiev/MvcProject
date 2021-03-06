﻿using Services.DataProviders;
using System;
using System.Web.Mvc;
using WildCampingWithMvc.Models.CampingPlace;

namespace WildCampingWithMvc.Controllers
{
    public class HomeController : Controller
    {
        private const int CountOfLastPlaces = 6;
        protected readonly ICampingPlaceDataProvider campPlaceDataProvider;

        public HomeController(ICampingPlaceDataProvider campPlaceDataProvider)
        {
            if (campPlaceDataProvider == null)
            {
                throw new ArgumentNullException("CampingPlaceDataProvider");
            }

            this.campPlaceDataProvider = campPlaceDataProvider;
        }

        public ActionResult Index()
        {
            var places = this.campPlaceDataProvider.GetLatestCampingPlaces(CountOfLastPlaces);
            MultipleCampingPlacesViewModel model = new MultipleCampingPlacesViewModel();
            model.CampingPlaces = places;

            return View(model);
        }

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