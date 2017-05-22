using CommonUtilities.Utilities;
using Services.DataProviders;
using Services.Models;
using System;
using System.Web.Mvc;
using WildCampingWithMvc.Models.CampingPlace;
using WildCampingWithMvc.Models.Sightseeing;

namespace WildCampingWithMvc.Controllers
{
    public class SightseeingController : Controller
    {
        protected readonly ISightseeingDataProvider sightseeingDataProvider;
        protected readonly ICampingPlaceDataProvider campingPlaceProvider;

        public SightseeingController(
            ISightseeingDataProvider sightseeingDataProvider, 
            ICampingPlaceDataProvider campingPlaceProvider)
        {
            if (sightseeingDataProvider == null)
            {
                throw new ArgumentNullException("SightseeingDataProvider");
            }

            if (campingPlaceProvider == null)
            {
                throw new ArgumentNullException("CampingPlaceProvider");
            }

            this.sightseeingDataProvider = sightseeingDataProvider;
            this.campingPlaceProvider = campingPlaceProvider;
        }

        // GET: Sightseeings
        public ActionResult Index()
        {
            var sightseeings = this.sightseeingDataProvider.GetAllSightseeings();
            SightseeingsViewModel model = new SightseeingsViewModel();
            model.Sightseeings = sightseeings;

            return View(model);
        }

        // GET: Deleted Sightseeings
        [Authorize(Roles ="Admin")]
        public ActionResult DeletedSightseeings()
        {
            var sightseeings = this.sightseeingDataProvider.GetDeletedSightseeings();
            SightseeingsViewModel model = new SightseeingsViewModel();
            model.Sightseeings = sightseeings;

            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult RecoverSightseeing(Guid id)
        {
            this.sightseeingDataProvider.RecoverDeletedSightseeingById(id);

            return RedirectToAction("SightseeingDetails", new { id = id});
        }

        // GET: SightseeingDetails
        [HttpGet]
        public ActionResult SightseeingDetails(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var sightseeing = this.sightseeingDataProvider.GetSightseeingById((Guid)id);
            if (sightseeing == null)
            {
                return this.RedirectToAction("Index");
            }

            SightseeingDetailsViewModel model = new SightseeingDetailsViewModel();
            model.Id = sightseeing.Id;
            model.Name = sightseeing.Name;
            model.Description = sightseeing.Description;
            var imageData = Utilities.ConvertToImage(sightseeing.Image);
            model.ImageData = imageData == Utilities.NoImage ? null : imageData;
            var sightseeingPlaces = this.campingPlaceProvider.GetSightseeingCampingPlaces(sightseeing.Name);
            model.Places = new MultipleCampingPlacesViewModel();
            model.Places.CampingPlaces = sightseeingPlaces;

            return View(model);
        }

        // GET: AddSightseeing
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult AddSightseeing()
        {
            return this.View();
        }

        // POST: AddSightseeing
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddSightseeing(AddSightseeingViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.AddSight(model);

            return RedirectToAction("Index");
        }

        // GET: EditSightseeing
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult EditSightseeing(Guid id)
        {
            AddSightseeingViewModel model = this.ConvertToAddFromISightseeing(id);

            return this.View(model);
        }

        // POST: EditSightseeing
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditSightseeing(AddSightseeingViewModel model, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.UpdateSightseeing(model, id);

            return RedirectToAction("SightseeingDetails", new { id = id });
        }

        [Authorize(Roles ="Admin")]
        public ActionResult DeleteSightseeing(Guid id)
        {
            this.sightseeingDataProvider.DeleteSightseeing(id);

            return RedirectToAction("Index");
        }

        private void UpdateSightseeing(AddSightseeingViewModel model, Guid id)
        {
            byte[] imageFileData = Utilities.ConvertFromImage(model.ImageFileData);
            this.sightseeingDataProvider.UpdateSightseeing(
                id, model.Name, model.Description, imageFileData);
        }

        private AddSightseeingViewModel ConvertToAddFromISightseeing(Guid id)
        {
            ISightseeing sightModel = this.sightseeingDataProvider.GetSightseeingById(id);
            AddSightseeingViewModel viewModel = new AddSightseeingViewModel();

            viewModel.Description = sightModel.Description;
            viewModel.Name = sightModel.Name;
            viewModel.ImageFileName = sightModel.Name;
            viewModel.ImageFileData = Utilities.ConvertToImage(sightModel.Image);

            return viewModel;
        }

        private void AddSight(AddSightseeingViewModel model)
        {
            byte[] imageFileData = Utilities.ConvertFromImage(model.ImageFileData);
            this.sightseeingDataProvider.AddSightseeing(
                model.Name, model.Description, imageFileData);
        }
    }
}