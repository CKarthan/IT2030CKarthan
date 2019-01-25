using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallisMVCBookstore.Controllers {
    public class ProductController : Controller {
        // GET: Product
        public ActionResult Index() {
            ViewBag.Message = "Product/Index is displayed";

            return View();
        }

        //GET Browse
        public string Browse() {
            string browseDisplay = "Browse displayed";

            return browseDisplay;
        }

        //GET Details with ID in URL
        public string Details(int id) {
            string productDetails = "Details displayed for id = " + id;

            return productDetails;
        }

        public string Location(int zip) {
            string locationValue = HttpUtility.HtmlEncode("Location displayed for zip = " + zip);

            return locationValue;
        }
    }
}