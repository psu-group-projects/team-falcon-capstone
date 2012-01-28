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
        public ViewResult Index()
        {            
            return View();
        }

        [HttpGet]
        public ActionResult MainPageAjaxUpdate(int page, int SortingType, string SearchPattern)
        {
            var pagingContext = SummaryRepository.GetAllSummaryData(page, SortingType, SearchPattern, PageSize);
            return PartialView("ProcessList", pagingContext);
        }


        [HttpGet]
        public ActionResult ProcessMsgUpdate(int page, string processName)
        {
            var pagingContext = MessageRepository.GetMessageByProcess(page, PageSize, processName);
            return PartialView("Message", pagingContext);
        }

        [HttpGet]
        public ActionResult ProcessJobUpdate(int page, string processName)
        {
            var pagingContext = JobRepository.GetJobByProcess(page, PageSize, processName);
            return PartialView("Job", pagingContext);
        }

        [HttpGet]
        public ViewResult MsgInquiry()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MsgInquiryUpdate(   string page_number,
                                                string sort_option,
                                                string msg_priority,
                                                string process_name, 
                                                string SU_SD_msg)
        {
            int input_msg_priority;

            // Parsing process
            if (msg_priority == "")
                input_msg_priority = 0;
            else
                input_msg_priority = Convert.ToInt32(msg_priority);


            var pagingContext = MsgInquiryRepo.GetMessages( Convert.ToInt32(page_number),
                                                            Convert.ToInt32(sort_option),
                                                            input_msg_priority,
                                                            process_name,
                                                            Convert.ToInt32(SU_SD_msg),
                                                            PageSize);
            return PartialView("MessageList", pagingContext);
        }
    }
}