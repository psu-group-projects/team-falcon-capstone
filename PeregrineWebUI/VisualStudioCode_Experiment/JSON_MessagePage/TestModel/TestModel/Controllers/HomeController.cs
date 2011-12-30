using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestModel.Models;
using TestModel.Models.Repository;

namespace TestModel.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Message/

        public ActionResult Index()
        {            
            return View();
        }


        // data GET service
        public JsonResult getMessage()
        {
              var pageData = MessageRepository.GetMessageDatas(1, 1, "", 5);
              var messages = pageData.Data;
 
              return Json(messages,JsonRequestBehavior.AllowGet);
        }

        
    }
}
