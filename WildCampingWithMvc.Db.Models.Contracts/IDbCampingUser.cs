using System;
using System.Collections.Generic;

namespace DbModelsContracts
{
    public interface IDbCampingUser
    {
        Guid Id { get; }

        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime RegisteredOn { get; set; }
    }
}