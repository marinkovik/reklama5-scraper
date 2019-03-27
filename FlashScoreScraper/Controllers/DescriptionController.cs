using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashScoreScraper.Controllers
{
    public class DescriptionController : Controller
    {
        // GET: Description
        public ActionResult Index(string id)
        {
            var newUrl = id.Replace("XXX", "?");
            return Redirect("http://reklama5.mk/" + newUrl);
        }
    }
}