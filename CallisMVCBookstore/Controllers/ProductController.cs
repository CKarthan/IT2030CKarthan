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

        //GET Details with ID param
        public string Details(int productId) {
            string productDetails = "Details displayed for Id: " + productId;

            return productDetails;
        }

        public string Location(int zipcode) {
            string locationValue = HttpUtility.HtmlEncode("Location displayed for zip: " + zipcode);

            return locationValue;
        }
    }
}