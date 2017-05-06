using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WildCampingWithMvc.Db.Models
{
    public class DbSiteCategory
    {
        private ICollection<DbCampingPlace> dbCampingPlaces;
        public DbSiteCategory()
        {
            this.dbCampingPlaces = new HashSet<DbCampingPlace>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30), MinLength(2)]
        public string Name { get; set; }

        [MaxLength(500), MinLength(5)]
        public string Description { get; set; }

        public byte[] Image { get; set; }
        public virtual ICollection<DbCampingPlace> DbCampingPlaces
        {
            get
            {
                return this.dbCampingPlaces;
            }
            set
            {
                this.dbCampingPlaces = value;
            }
        }
    }
}
