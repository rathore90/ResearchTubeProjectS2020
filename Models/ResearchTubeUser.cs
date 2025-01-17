﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ResearchTube.Models;

namespace ResearchTube.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ResearchTubeUser class
    public class ResearchTubeUser : IdentityUser
    {
#nullable enable
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(max)")]
        public string? UploadImage { get; set; }

#nullable disable
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Position { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Interest { get; set; }

        public Payment Payment { get; set; }
    }
}
