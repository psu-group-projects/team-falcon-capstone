/*
Author   : Chinh T Cao
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeregrineUI_2.Models;
using PeregrineUI_2.Models.Repository;

namespace PeregrineUI_2.Controllers
{
    public class HomeController : Controller
    {
        //
        // Specify how many entries for one page
        // Will be added into config file in the future
        //
        public const int PageSize = 10;

        [HttpGet]
        public ActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public ActionResult ProcessList(int page)
        {
            var pagingContext = SummaryRepository.GetAllProcessesData(page, 1, "", PageSize);
            return PartialView("ProcessList", pagingContext); 
        }

        [HttpGet]
        public ActionResult AjaxSearch(string searchpattern)
        {
            var pagingContext = SummaryRepository.GetAllProcessesData(1, 1, searchpattern, PageSize);
            return PartialView("ProcessList", pagingContext);
        }

    }
}