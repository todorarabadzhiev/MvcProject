using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WildCampingWithMvc.Areas.Admin.Models;

namespace WildCampingWithMvc.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly ICampingUserDataProvider campingUserDataProvider;
        public UserController(ICampingUserDataProvider campingUserDataProvider)
        {
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