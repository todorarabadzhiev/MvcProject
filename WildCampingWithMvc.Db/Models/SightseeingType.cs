using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingDB.Models
{
    public class SightseeingType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [MaxLength(30), MinLength(2)]
        public string Name { get; set; }
    }
}
