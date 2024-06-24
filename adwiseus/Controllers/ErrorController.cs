using adwiseus.Models;
using System;
using System.Web.Mvc;

namespace adwiseus.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Guid.NewGuid().ToString()
            };

            return View(errorViewModel);
        }
    }
}
