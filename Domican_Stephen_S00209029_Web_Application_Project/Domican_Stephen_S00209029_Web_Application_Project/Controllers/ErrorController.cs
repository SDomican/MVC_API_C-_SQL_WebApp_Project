using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domican_Stephen_S00209029_Web_Application_Project.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Http400()
        {
            Response.StatusCode = 400;
            return View();
        }

        // GET: Error
        public ActionResult Http404()
        {
            Response.StatusCode = 404;
            return View();
        }

    }
}