using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        //Dependency Injection - Contructor Injection of a new StoreDB based on Music Store Etities shown below
        MusicStoreEntities storeDB = new MusicStoreEntities();

        // GET: Store
        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();

            //What an idiot!! lol
            //foreach (var genre in storeDB.Genres)
            //{
            //    //var Genre = genre.Name.ToString();
            //}
            //Hardcoded List of Genres REMOVE
            //var genres = new List<Genre>
            //{
            //    new Genre {Name = "Disco"},
            //    new Genre {Name = "Jazz"},
            //    new Genre {Name = "Rock"},
            //};
            //return "Hello from Store.Index";
            return View(genres);
        }
        // GET: Store/Browse
        public ActionResult Browse(string genre)
        {
            //Retrieve Genre and its Associated Albums from database
            var genreModel = storeDB.Genres.Include("Albums")
                .Single(g => g.Name == genre);

            return View(genreModel);


            //OLD -return a ViewModel with a "Genre parameter" the "same value" as you pass into action
            //var genreModel = new Genre { Name = genre };
            //return View(genreModel);

            //string message = HttpUtility.HtmlEncode("Store.Browse, Genre = " + genre);
            //return message;
        }

        // GET: Store/Details
        public ActionResult Details(int id)
        {
            var album = storeDB.Albums.Find(id);

            return View(album);

            //Nick Attlee's way - My way
            //var album = storeDB.Albums.Where(a => a.AlbumId == id).FirstOrDefault();
            //return View(album);

            //OlD - Return an Album with a Hardcoded String plus the Id pAlbumsassed into the action
            //var album = new Album { Title = "Album " + id };
            //return View(album);
            
        }

        //
        //GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres.ToList();

            return PartialView(genres);
        }
    }
}