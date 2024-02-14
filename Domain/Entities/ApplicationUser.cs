using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{

    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        // Credits for bike rentals
        public int Credits { get; set; }

        // Optional: Link to rental history if needed
        // This could be a navigation property in a fully relational design
        // public virtual ICollection<BikeRental> RentalHistory { get; set; }

        // Optional: Link to reported issues
        // This could be useful for service personnel to track issues reported by this user
        // public virtual ICollection<BikeIssueReport> ReportedIssues { get; set; }

        // Constructor to initialize collections
        public ApplicationUser()
        {
            // Initialize collections if using navigation properties
            // RentalHistory = new HashSet<BikeRental>();
            // ReportedIssues = new HashSet<BikeIssueReport>();
        }
    }


}
