﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSLIS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult InternalServerError()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult NoAuthorization()
        {
            return View();
        }

    }
}