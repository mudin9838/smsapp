using sms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
//using Syncfusion.JavaScript;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Needed for the SetString and GetString extension methods
using Newtonsoft.Json;
//using Syncfusion.JavaScript.DataSources;
using System.Collections;
namespace sms.Controllers
{
    public class HomeController : Controller
    {
    
        public IActionResult Index()
        {

      
            return View();
        }

     
   
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
    //public class HomeController : Controller
    //{



    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //}
}
