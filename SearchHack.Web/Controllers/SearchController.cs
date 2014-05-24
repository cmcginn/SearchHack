using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchHack.Web.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Settings/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchSettings()
        {
            return View();
        }

        public ActionResult SearchUrls()
        {
            return View();
        }
	}
}