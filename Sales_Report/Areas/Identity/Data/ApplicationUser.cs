using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sales_Report.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        
        [EmailAddress]
        public string LoginID { get; set; }
        public string name { get; set; }
        public string reporting_Manager { get; set; }

        public string userRole { get; set; }

        //Define that One to Many
        public virtual ICollection<SalesTransaction> SalesTransaction { get; set; }
    }
}
