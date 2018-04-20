using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Models
{
    [Bind(Exclude = "AlbumId")]
    public class Album
    {
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }

        [DisplayName("Genre")]
        public int GenreId { get; set; }

        [DisplayName("Artist")]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "An album Title is required")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "A Price is required")]
        [Range(0.01, 100.00,
            ErrorMessage = "The Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }

        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }

        //Added Virtual Property to Allow EF to Lazy-load Genre and Artist as Necessary
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }

    }
}