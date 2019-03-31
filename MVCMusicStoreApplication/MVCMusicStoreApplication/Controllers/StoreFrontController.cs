using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStoreApplication.Controllers
{
    public class StoreFrontController : Controller
    {

        MVCMusicStoreDB db = new MVCMusicStoreDB();

        // GET: StoreFront
        public ActionResult Index(int id){
            var genreAlbums = db.Albums.Where(a => a.GenreId == id).ToList();
            return View("Index", genreAlbums);
        }

        public ActionResult Browse() {
            var genres = db.Genres.ToList().OrderBy(g => g.Name);
            return View("Browse", genres);
        }

        public ActionResult Details(int id) {
            var album = db.Albums.FirstOrDefault(a => a.AlbumId == id);
            return View("Details", album);
        }

    }
}