using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGentle_BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            // return "WebGentle";
            return View();
            //if you want to return different view
            //return View("About");
            //if you want to pass model obj with view
            //var obj = new { ID = 1, Name = "Sunanda" };
            // return View("About", obj);

            //if to return view from other location
            //return View("TempView/SunTemp.cshtml"); //by passing full path of View.
            //or by passign relative path of View.
            //return View("../../TempView/SunTemp.cshtml");
        }

        public ViewResult About()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }
    }
}
