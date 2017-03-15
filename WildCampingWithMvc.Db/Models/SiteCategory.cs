using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WildCampingWithMvc.Db.Models
{
    public class DbSiteCategory
    {
        private ICollection<DbCampingPlace> campingPlaces;
        public DbSiteCategory()
        {
            this.campingPlaces = new HashSet<DbCampingPlace>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30), MinLength(2)]
        public string Name { get; set; }
        public virtual ICollection<DbCampingPlace> CampingPlaces
        {
            get
            {
                return this.campingPlaces;
            }
            set
            {
                this.campingPlaces = value;
            }
        }
    }
}
