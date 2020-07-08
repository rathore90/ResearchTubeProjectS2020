using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ResearchTube.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ResearchTubeUser class
    public class ResearchTubeUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Position { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Interest { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string University { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Country { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Province { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
    }
}
