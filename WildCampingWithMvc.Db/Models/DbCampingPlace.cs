using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WildCampingWithMvc.Db.Models
{
    public class DbCampingPlace
    {
        private ICollection<DbCampingUser> favoredBy;
        private ICollection<DbSiteCategory> dbSiteCategories;
        private ICollection<DbSightseeing> dbSightseeings;
        private ICollection<DbImageFile> dBImageFiles;

        public DbCampingPlace()
        {
            this.favoredBy = new HashSet<DbCampingUser>();
            this.dbSiteCategories = new HashSet<DbSiteCategory>();
            this.dbSightseeings = new HashSet<DbSightseeing>();
            this.dBImageFiles = new HashSet<DbImageFile>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30), MinLength(2)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string GoogleMapsUrl { get; set; }

        [Required]
        public bool WaterOnSite { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddedOn { get; set; }

        [ForeignKey("AddedBy")]
        public Guid AddedById { get; set; }
        public virtual DbCampingUser AddedBy { get; set; }

        [InverseProperty("FavoriteCampingPlaces")]
        public virtual ICollection<DbCampingUser> FavoredBy
        {
            get
            {
                return this.favoredBy;
            }
            set
            {
                this.favoredBy = value;
            }
        }

        public virtual ICollection<DbSiteCategory> DbSiteCategories
        {
            get
            {
                return this.dbSiteCategories;
            }
            set
            {
                this.dbSiteCategories = value;
            }
        }

        public virtual ICollection<DbSightseeing> DbSightseeings
        {
            get
            {
                return this.dbSightseeings;
            }
            set
            {
                this.dbSightseeings = value;
            }
        }

        public virtual ICollection<DbImageFile> DbImageFiles
        {
            get
            {
                return this.dBImageFiles;
            }
            set
            {
                this.dBImageFiles = value;
            }
        }
    }
}
