using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchTube.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }

        [Required]
        public string UserId { get; set; }

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
    }
}
