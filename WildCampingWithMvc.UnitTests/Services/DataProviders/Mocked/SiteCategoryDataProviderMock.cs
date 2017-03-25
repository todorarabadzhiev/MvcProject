using EFositories;
using Services.DataProviders;
using System;

namespace CampingWebForms.Tests.Mocked
{
    public class SiteCategoryDataProviderMock : SiteCategoryDataProvider
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

        public SiteCategoryDataProviderMock(IWildCampingEFository repository, Func<IUnitOfWork> unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}
