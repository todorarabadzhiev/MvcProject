using EFositories;
using Services.Models;
using System;
using System.Collections.Generic;
using WildCampingWithMvc.Db.Models;

namespace Services.DataProviders
{
    public class SightseeingDataProvider : ISightseeingDataProvider
    {
        protected readonly IWildCampingEFository repository;
        protected readonly Func<IUnitOfWork> unitOfWork;

        public SightseeingDataProvider(IWildCampingEFository repository, Func<IUnitOfWork> unitOfWork)
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

        public IEnumerable<ISightseeing> GetAllSightseeings()
        {
            IGenericEFository<DbSightseeing> sightseeingRepository =
                this.repository.GetSightseeingRepository();
            var dbSightseeings = sightseeingRepository.GetAll();
            if (dbSightseeings == null)
            {
                return null;
            }

            var sightseeings = new List<ISightseeing>();
            foreach (var s in dbSightseeings)
            {
                sightseeings.Add(ConvertToSightseeeing(s));
            }

            return sightseeings;
        }

        private ISightseeing ConvertToSightseeeing(DbSightseeing s)
        {
            ISightseeing sightseeing = new Sightseeing();
            sightseeing.Name = s.Name;
            sightseeing.Id = s.Id;

            return sightseeing;
        }
    }
}
