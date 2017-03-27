using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ICampingUserDataProvider
    {
        IEnumerable<ICampingUser> GetAllCampingUsers();
        ICampingUser GetCampingUserById(Guid id);
        int UpdateCampingUser(Guid id, string firstName, string lastName);
        int DeleteCampingUser(Guid id);
        void AddCampingUser(string appUserId,
            string firstName, string lastName, string userName);
    }
}
