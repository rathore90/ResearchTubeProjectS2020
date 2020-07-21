using ResearchTube.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchTube.Models
{
    public class Video
    {
        [Key]
        public string VideoId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Author { get; set; }

        [Column(TypeName = "date")]
        public DateTime UploadDate { get; set; }
        public bool IsFree { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string VideoPath { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        public Category VideoCategory { get; set; }

        public List<UserVideos> UserVideos {get; set;}
    }
}
