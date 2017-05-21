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

        public void AddSightseeing(string name, string description, byte[] imageFileData)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Sightseeing Name");
            }

            ISightseeing newSightseeing = new Sightseeing();
            newSightseeing.Name = name;
            newSightseeing.Description = description;
            newSightseeing.Image = imageFileData;
            DbSightseeing dbSightseeing = this.ConvertFromSightseeing(newSightseeing);

            using (var uw = this.unitOfWork())
            {
                IGenericEFository<DbSightseeing> sightseeingRepository =
                    this.repository.GetSightseeingRepository();
                sightseeingRepository.Add(dbSightseeing);
                uw.Commit();
            }
        }

        public void DeleteSightseeing(Guid id)
        {
            this.ChangeIsDeletedPropertyTo(id, true);
        }

        public IEnumerable<ISightseeing> GetAllSightseeings()
        {
            return this.GetSightseeingsByIsDeletedProperty(false);
        }

        public IEnumerable<ISightseeing> GetDeletedSightseeings()
        {
            return this.GetSightseeingsByIsDeletedProperty(true);
        }

        public ISightseeing GetSightseeingById(Guid id)
        {
            IGenericEFository<DbSightseeing> sightseeingRepository =
                this.repository.GetSightseeingRepository();
            DbSightseeing dbSightseeing = sightseeingRepository.GetById(id);
            if (dbSightseeing == null)
            {
                return null;
            }

            ISightseeing sightseeing = ConvertToSightseeeing(dbSightseeing);

            return sightseeing;
        }

        public void RecoverDeletedSightseeingById(Guid id)
        {
            this.ChangeIsDeletedPropertyTo(id, false);
        }

        public void UpdateSightseeing(Guid id, string name, string description, byte[] imageFileData)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Sightseeing Name");
            }

            IGenericEFository<DbSightseeing> sightseeingRepository =
                    this.repository.GetSightseeingRepository();
            DbSightseeing dbSightseeing = sightseeingRepository.GetById(id);
            if (dbSightseeing == null)
            {
                throw new ArgumentException("Invalid Sightseeing Id");
            }

            ISightseeing updatedSightseeing = new Sightseeing();
            updatedSightseeing.Id = id;
            updatedSightseeing.Name = name;
            updatedSightseeing.Description = description;
            updatedSightseeing.Image = imageFileData;
            using (var uw = this.unitOfWork())
            {
                this.UpdateFromSightseeing(updatedSightseeing, dbSightseeing);

                sightseeingRepository.Update(dbSightseeing);
                uw.Commit();
            }
        }

        private void UpdateFromSightseeing(ISightseeing sightseeing, DbSightseeing dbSightseeing)
        {
            dbSightseeing.Name = sightseeing.Name;
            dbSightseeing.Description = sightseeing.Description;
            dbSightseeing.Image = sightseeing.Image;
        }

        private ISightseeing ConvertToSightseeeing(DbSightseeing s)
        {
            ISightseeing sightseeing = new Sightseeing();
            sightseeing.Name = s.Name;
            sightseeing.Id = s.Id;

            return sightseeing;
        }

        private DbSightseeing ConvertFromSightseeing(ISightseeing sightseeing)
        {
            DbSightseeing s = new DbSightseeing();
            s.Name = sightseeing.Name;
            s.Description = sightseeing.Description;
            s.IsDeleted = sightseeing.IsDeleted;
            s.Image = sightseeing.Image;

            return s;
        }

        private void ChangeIsDeletedPropertyTo(Guid id, bool isDeleted)
        {
            IGenericEFository<DbSightseeing> sightseeingRepository =
                this.repository.GetSightseeingRepository();
            var dbSightseeing = sightseeingRepository.GetById(id);
            if (dbSightseeing != null)
            {
                using (var uw = this.unitOfWork())
                {
                    dbSightseeing.IsDeleted = isDeleted;
                    uw.Commit();
                }
            }
        }

        private IEnumerable<ISightseeing> GetSightseeingsByIsDeletedProperty(bool isDeleted)
        {
            IGenericEFository<DbSightseeing> sightseeingRepository =
                this.repository.GetSightseeingRepository();
            var dbSightseeings = sightseeingRepository.GetAll(c => c.IsDeleted == isDeleted);
            if (dbSightseeings == null)
            {
                return null;
            }

            var sightseeings = new List<ISightseeing>();
            foreach (var s in dbSightseeings)
            {
                sightseeings.Add(this.ConvertToSightseeeing(s));
            }

            return sightseeings;
        }
    }
}
