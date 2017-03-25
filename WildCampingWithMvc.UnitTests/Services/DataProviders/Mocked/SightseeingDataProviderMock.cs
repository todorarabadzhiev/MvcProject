using EFositories;
using Services.DataProviders;
using System;

namespace CampingWebForms.Tests.Mocked
{
    public class SightseeingDataProviderMock : SightseeingDataProvider
    {
        public IWildCampingEFository Repository
        {
            get
            {
                return this.repository;
            }
        }

        public Func<IUnitOfWork> UnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }
        }

        public SightseeingDataProviderMock(IWildCampingEFository repository, Func<IUnitOfWork> unitOfWork) 
            : base(repository, unitOfWork)
        {
        }
    }
}
