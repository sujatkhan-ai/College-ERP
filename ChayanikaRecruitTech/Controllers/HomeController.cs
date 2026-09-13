using ChayanikaRecruitTech.Models;
using CRT_Core.Interface;
using CRT_Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ChayanikaRecruitTech.Controllers
{
     
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreRecords _CoreRecords;
        private SKContextFile _SKContextFile { get; set; }

        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public HomeController(ILogger<HomeController> logger, ICoreRecords coreRecords, SKContextFile sKContextFile, IHostingEnvironment environment)
        {
            _logger = logger;
            _CoreRecords = coreRecords;
            _SKContextFile = sKContextFile;
            _environment = environment;
        }
       
        public IActionResult Index()
        {
            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            {


                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        } 
        //public async Task<IActionResult> Menus(int id)
        //{
        //    var UserRId = Convert.ToInt32(HttpContext.Session.GetString("UserRId"));
        //    dynamic menumode = new ExpandoObject();
        //    menumode = await _CoreRecords.GetDynamiMenu(UserRId);
        //    return View(menumode);
        //}
        //ActiveAccount

        public IActionResult Privacy()
        {
            var sessionchk = HttpContext.Session.GetString("UserRId");
            if (sessionchk != null)
            {


                return View();

            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
         

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
