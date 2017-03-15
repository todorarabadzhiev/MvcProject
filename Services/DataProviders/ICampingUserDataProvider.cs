using Services.Models;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public interface ICampingUserDataProvider
    {
        IEnumerable<ICampingUser> GetAllCampingUsers();
        void AddCampingUser(string appUserId,
            string firstName, string lastName, string userName);
    }
}
