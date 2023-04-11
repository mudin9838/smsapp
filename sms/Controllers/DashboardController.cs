using sms.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Controllers
{
  [Authorize]
    public class DashboardController : Controller
    {
        //private EserviceContext _context;

        //public DashboardController(EserviceContext Context)
        //{
        //    this._context = Context;
        //}

        public IActionResult Index()
        {
           
            return View();
        }
    }
}
