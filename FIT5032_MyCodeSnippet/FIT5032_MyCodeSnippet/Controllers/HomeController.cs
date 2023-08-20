using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIT5032_MyCodeSnippet.Models.Hello_World;

namespace FIT5032_MyCodeSnippet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            // create instance of the Hello object
            Hello hello = new Hello();

            // assigned the ViewBag.Message to the result
            ViewBag.Message = hello.GetHello();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}