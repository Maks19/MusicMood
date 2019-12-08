using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace MusicMood.Models
{
    public class UploadModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string Album { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string Gener { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string Color { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public HttpPostedFileBase SoundObj { get; set; }
        public HttpPostedFileBase SoundImg { get; set; }
    }
}