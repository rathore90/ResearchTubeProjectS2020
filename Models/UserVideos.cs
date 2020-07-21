using ResearchTube.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTube.Models
{
    public class UserVideos
    {
        public string AspNetUsersId { get; set; }
        public ResearchTubeUser ResearchTubeUser { get; set; }

        public string VideoId { get; set; }
        public Video Video { get; set; }
    }
}
