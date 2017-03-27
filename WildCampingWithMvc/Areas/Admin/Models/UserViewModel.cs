using System;

namespace WildCampingWithMvc.Areas.Admin.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}