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
using PeregrineUI_2.Properties;

namespace PeregrineUI_2.Controllers
{
    public class HomeController : Controller
    {
        //
        // Specify how many entries for one page
        // Will be added into config file in the future
        //
        private int PageSize;
        private int Refresh_Rate;

        public HomeController()
        {
            PageSize = Properties.Settings.Default.Page_Size;
        }
        
        [HttpGet]
        public ViewResult Index()
        {
            Refresh_Rate = Properties.Settings.Default.Refresh_Rate;
            ViewBag.Refresh_Rate = Refresh_Rate;
            return View();
        }

        [HttpPost]
        public ActionResult MainPageAjaxUpdate(int page, int sort_input, string SearchPattern)
        {
            int sort_columm = sort_input / 2;
            int sort_type = sort_input % 2;  // sort_type = [0 for accending and 1 for descending]

            var pagingContext = SummaryRepository.GetSummaryDataByPage(page, sort_columm, sort_type, SearchPattern, PageSize);
            return PartialView("ProcessList", pagingContext);
        }

        [HttpPost]
        public ActionResult ProcessMsgUpdate(int page, string processName)
        {
            var pagingContext = MessageRepository.GetMessageByProcess(page, PageSize, processName);
            return PartialView("Message", pagingContext);
        }

        [HttpPost]
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

        // Message inquriy page
        [HttpPost]
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

            // Parsing the sort_option info
            int sort_option_input = Convert.ToInt32(sort_option);
            int sort_columm = sort_option_input / 2;
            int sort_type = sort_option_input % 2;  // sort_type = [0 for accending and 1 for descending]

            var pagingContext = MsgInquiryRepo.GetMessages( Convert.ToInt32(page_number),
                                                            sort_columm,
                                                            sort_type,
                                                            input_msg_priority,
                                                            process_name,
                                                            Convert.ToInt32(SU_SD_msg),
                                                            PageSize);

            return PartialView("MessageList", pagingContext);
        }

        [HttpPost]
        public string MsgInq_getfulldetail(string msg_id)
        {
            string result = MsgContentRepository.GetMessageContent(msg_id);
            return result;
        }


        [HttpPost]
        public string AutoCompleteUpdate( string search_string)
        {
            string autoCompleteResults = AutoCompleteRepository.GetAutoCompleteList(search_string);
            return autoCompleteResults;
        }
    }
}

//padding: 3px 18px 3px 10px;