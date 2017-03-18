using EFositories;
using Services.Models;
using System;
using System.Collections.Generic;
using WildCampingWithMvc.Db.Models;

namespace Services.DataProviders
{
    public class CampingUserDataProvider : ICampingUserDataProvider
    {
        protected readonly IWildCampingEFository repository;
        protected readonly Func<IUnitOfWork> unitOfWork;

        public CampingUserDataProvider(IWildCampingEFository repository, Func<IUnitOfWork> unitOfWork)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("WildCampingEFository");
            }
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork");
            }

            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void AddCampingUser(string appUserId, string firstName,
            string lastName, string userName)
        {
            if (firstName == null)
            {
                throw new ArgumentNullException("FirstName");
            }

            if (lastName == null)
            {
                throw new ArgumentNullException("LastName");
            }

            if (userName == null)
            {
                throw new ArgumentNullException("UserName");
            }

            if (appUserId == null)
            {
                throw new ArgumentNullException("ApplicationUserId");
            }

            IGenericEFository<DbCampingUser> capmingUserRepository =
                this.repository.GetCampingUserRepository();
            ICampingUser newCampingUser = new CampingUser();
            newCampingUser.ApplicationUserId = appUserId;
            newCampingUser.FirstName = firstName;
            newCampingUser.LastName = lastName;
            newCampingUser.UserName = userName;

            using (var uw = this.unitOfWork())
            {
                DbCampingUser dbCampingUser = ConvertFromUser(newCampingUser);
                capmingUserRepository.Add(dbCampingUser);
                uw.Commit();
            }
        }

        public IEnumerable<ICampingUser> GetAllCampingUsers()
        {
            IGenericEFository<DbCampingUser> capmingUserRepository =
                this.repository.GetCampingUserRepository();
            var dbUsers = capmingUserRepository.GetAll();
            if (dbUsers == null)
            {
                return null;
            }

            IList<ICampingUser> users = new List<ICampingUser>();
            foreach (var dbUser in dbUsers)
            {
                ICampingUser user = ConvertToUser(dbUser);
                users.Add(user);
            }

            return users;
        }

        private ICampingUser ConvertToUser(DbCampingUser dbUser)
        {
            ICampingUser user = new CampingUser();
            user.ApplicationUserId = dbUser.ApplicationUserId;
            user.FirstName = dbUser.FirstName;
            user.LastName = dbUser.LastName;
            user.RegisteredOn = dbUser.RegisteredOn;
            user.Id = dbUser.Id;
            user.UserName = dbUser.UserName;

            //user.MyCampingPlaces = dbUser.AddedCampingPlaces;

            return user;
        }

        private DbCampingUser ConvertFromUser(ICampingUser campUser)
        {
            DbCampingUser user = new DbCampingUser();
            user.ApplicationUserId = campUser.ApplicationUserId;
            user.FirstName = campUser.FirstName;
            user.LastName = campUser.LastName;
            user.UserName = campUser.UserName;
            user.RegisteredOn = DateTime.Now;

            return user;
        }
    }
}
