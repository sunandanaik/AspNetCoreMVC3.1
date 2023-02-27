using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using WebGentle_BookStore.Models;

namespace WebGentle_BookStore.Controllers
{
    public class HomeController : Controller
    {
        //Using ViewData Attribute.
        [ViewData]
        public string CustomProperty { get; set; }
        public ViewResult Index()
        {
            //Anonymous Type data passed in ViewBag.
            dynamic data = new ExpandoObject();
            data.Id = 1;
            data.Name = "Sunanda Naik";
            ViewBag.Data = data;

            //Also can pass Model object data in ViewBag.
            ViewBag.Type = new BookModel() { Id = 1, Author = "Dennis" };

            ViewData["prop1"] = "Sunanda";

            //Passing Complex data type in ViewData.
            ViewData["book"] = new BookModel()
            {
                Author = "James",
                Language = "English"
            };

            CustomProperty = "Custom Value";

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
