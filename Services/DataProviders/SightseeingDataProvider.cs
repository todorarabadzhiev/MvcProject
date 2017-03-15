using Repositories;
using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public class SightseeingDataProvider : ISightseeingDataProvider
    {
        protected readonly ICampingDBRepository repository;
        protected readonly Func<IUnitOfWork> unitOfWork;

        public SightseeingDataProvider(ICampingDBRepository repository, Func<IUnitOfWork> unitOfWork)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("CampingDBRepository");
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
            IGenericRepository<CampingDB.Models.Sightseeing> sightseeingRepository =
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

        private ISightseeing ConvertToSightseeeing(CampingDB.Models.Sightseeing s)
        {
            ISightseeing sightseeing = new Sightseeing();
            sightseeing.Name = s.Name;
            sightseeing.Id = s.Id;

            return sightseeing;
        }
    }
}
