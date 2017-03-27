using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WildCampingWithMvc.App_GlobalResources;
using WildCampingWithMvc.Areas.Admin.Models;

namespace WildCampingWithMvc.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        protected readonly ICampingUserDataProvider campingUserDataProvider;
        public UserController(ICampingUserDataProvider campingUserDataProvider)
        {
            if (campingUserDataProvider == null)
            {
                throw new ArgumentNullException("CampingUserDataProvider");
            }

            this.campingUserDataProvider = campingUserDataProvider;
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            IEnumerable<ICampingUser> users = this.campingUserDataProvider.GetAllCampingUsers();
            ICollection<UserViewModel> usersModel = this.GetUsersModelFromICampingUser(users);

            return Json(usersModel, JsonRequestBehavior.AllowGet);
        }

        public string UpdateUser(UserViewModel userModel)
        {
            string message;
            if (ModelState.IsValid)
            {
                int updateResult = this.campingUserDataProvider.UpdateCampingUser(
                    userModel.Id, userModel.FirstName, userModel.LastName);
                if (updateResult > 0)
                {
                    message = GlobalResources.SuccessUpdate;
                }
                else
                {
                    message = GlobalResources.ErrDbUpdate;
                }
            }
            else
            {
                message = GlobalResources.ErrValidation;
            }

            return message;
        }

        public string DeleteUser(UserViewModel userModel)
        {
            string message;
            if (ModelState.IsValid)
            {
                int deleteResult = this.campingUserDataProvider.DeleteCampingUser(userModel.Id);
                if (deleteResult > 0)
                {
                    message = GlobalResources.SuccessDelete;
                }
                else
                {
                    message = GlobalResources.ErrDbUpdate;
                }
            }
            else
            {
                message = GlobalResources.ErrValidation;
            }

            return message;
        }

        private ICollection<UserViewModel> GetUsersModelFromICampingUser(IEnumerable<ICampingUser> users)
        {
            ICollection<UserViewModel> modelUsers = new List<UserViewModel>();
            foreach (var user in users)
            {
                UserViewModel modelUser = new UserViewModel();
                modelUser.Id = user.Id;
                modelUser.FirstName = user.FirstName;
                modelUser.LastName = user.LastName;
                modelUser.UserName = user.UserName;
                modelUser.RegisteredOn = user.RegisteredOn;

                modelUsers.Add(modelUser);
            }

            return modelUsers;
        }
    }
}