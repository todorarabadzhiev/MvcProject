using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WildCampingWithMvc.Db.Models
{
    public class DbCampingUser
    {
        private ICollection<DbCampingPlace> favoriteCampingPlaces;

        public DbCampingUser()
        {
            this.favoriteCampingPlaces = new HashSet<DbCampingPlace>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }//take from Account

        [Required]
        [MaxLength(30), MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30), MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30), MinLength(2)]
        public string UserName { get; set; }
        public DateTime RegisteredOn { get; set; }

        public virtual ICollection<DbCampingPlace> FavoriteCampingPlaces
        {
            get
            {
                return this.favoriteCampingPlaces;
            }
            set
            {
                this.favoriteCampingPlaces = value;
            }
        }
    }
}
