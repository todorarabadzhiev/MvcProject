using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WildCampingWithMvc.Db.Models
{
    public class DbCampingPlace
    {
        private ICollection<DbSiteCategory> siteCategories;
        private ICollection<DbSightseeing> sightseeings;
        private ICollection<DbImageFile> imageFiles;

        public DbCampingPlace()
        {
            this.siteCategories = new HashSet<DbSiteCategory>();
            this.sightseeings = new HashSet<DbSightseeing>();
            this.imageFiles = new HashSet<DbImageFile>();
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
        public virtual DbCampingUser AddedBy { get; set; }

        public virtual ICollection<DbSiteCategory> SiteCategories
        {
            get
            {
                return this.siteCategories;
            }
            set
            {
                this.siteCategories = value;
            }
        }

        public virtual ICollection<DbSightseeing> Sightseeings
        {
            get
            {
                return this.sightseeings;
            }
            set
            {
                this.sightseeings = value;
            }
        }

        public virtual ICollection<DbImageFile> ImageFiles
        {
            get
            {
                return this.imageFiles;
            }
            set
            {
                this.imageFiles = value;
            }
        }
    }
}
