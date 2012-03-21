/*
Author   : Chinh T Cao
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011-2012 All right reserved
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
        private int PageSize;
        private int Refresh_Rate;
        private int Msg_Length_Threshold;

        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// Read the value of Page_Size in websettings
        /// </summary>
        public HomeController()
        {
            PageSize = Properties.Settings.Default.Page_Size;
        }


        /// <summary>
        /// This is the entry point when users start the peregrine web app.
        /// Read the value of Refresh_Rate in websettings, bring it to Index.cshtml and start up the 
        /// Index.cshtm.
        /// </summary>
        /// <returns> ViewResult type </returns>
        [HttpGet]
        public ViewResult Index()
        {
            Refresh_Rate = Properties.Settings.Default.Refresh_Rate;
            ViewBag.Refresh_Rate = Refresh_Rate;
            return View();
        }


        /// <summary>
        /// This function is called when :
        ///     * Users wants to search for a particular process with a sorting way
        ///     * Automatic update
        /// This function will request the information from the DB using SOAP API, then
        /// it will bring these received info from DB to the partial view ProcessList.
        /// </summary>
        /// <param name="page">page [int]</param>
        /// <param name="sort_input">sort_input [int]</param>
        /// <param name="process_name">process_name [string]</param>
        /// <returns> PartialView </returns>
        [HttpPost]
        public ActionResult MainPageAjaxUpdate(int page, int sort_input, string process_name)
        {
            int sort_columm, sort_type;

            sort_columm = sort_input / 2;
            sort_type = sort_input % 2;  // sort_type = [0 for accending and 1 for descending]

            var pagingContext = SummaryRepository.GetSummaryDataByPage(page, sort_columm, sort_type, process_name, PageSize);
            Msg_Length_Threshold = Properties.Settings.Default.Message_Length;
            ViewBag.Msg_Length_Threshold = Msg_Length_Threshold;
            return PartialView("ProcessList", pagingContext);
        }


        /// <summary>
        /// Processes the MSG update.
        /// </summary>
        /// <param name="page">page [int]</param>
        /// <param name="processName">processName [int]</param>
        /// <returns> ActionResult </returns>
        [HttpPost]
        public ActionResult ProcessMsgUpdate(int page, int processID)
        {
            var pagingContext = MessageRepository.GetMessageByProcess(page, PageSize, processID);
            Msg_Length_Threshold = Properties.Settings.Default.Message_Length;
            ViewBag.Msg_Length_Threshold = Msg_Length_Threshold;
            return PartialView("Message", pagingContext);
        }


        /// <summary>
        /// Processes the job update.
        /// </summary>
        /// <param name="page">page [int]</param>
        /// <param name="processName">processName [int]</param>
        /// <returns> ActionResult </returns>
        [HttpPost]
        public ActionResult ProcessJobUpdate(int page, int processID)
        {
            var pagingContext = JobRepository.GetJobByProcess(page, PageSize, processID);
            return PartialView("Job", pagingContext);
        }


        /// <summary>
        /// Start the message inquiry page.
        /// The message inquiry page is used when user want to do message oriented search
        /// </summary>
        /// <returns> ViewResult </returns>
        [HttpGet]
        public ViewResult MsgInquiry()
        {
            Refresh_Rate = Properties.Settings.Default.Refresh_Rate;
            ViewBag.Refresh_Rate = Refresh_Rate;
            return View();
        }


        /// <summary>
        /// This function is called when :
        ///     * Users wants to search for a particular process with a priority value and a sorting way
        ///     * Automatic update
        /// This function will request the information from the DB using SOAP API, then
        /// it will bring these received info from DB to the partial view MessageList.
        /// </summary>
        /// <param name="page_number">page_number [string]</param>
        /// <param name="sort_option">sort_option [string]</param>
        /// <param name="msg_priority">msg_priority [string]</param>
        /// <param name="process_name">process_name [string]</param>
        /// <param name="SU_SD_msg">SU_SD_msg [string]</param>
        /// <returns> ActionResult </returns>
        [HttpPost]
        public ActionResult MsgInquiryUpdate(   string page_number,
                                                string sort_option,
                                                string msg_priority,
                                                string process_name, 
                                                string SU_SD_msg)
        {
            int input_msg_priority, sort_option_input, sort_columm, sort_type;

            // Parsing process
            if (msg_priority == "")
                input_msg_priority = 0;
            else
                input_msg_priority = Convert.ToInt32(msg_priority);

            // Parsing the sort_option info
            sort_option_input   = Convert.ToInt32(sort_option);
            sort_columm         = sort_option_input / 2;
            sort_type           = sort_option_input % 2;  // sort_type = [0 for accending and 1 for descending]

            var pagingContext = MsgInquiryRepo.GetMessages( Convert.ToInt32(page_number),
                                                            sort_columm,
                                                            sort_type,
                                                            input_msg_priority,
                                                            process_name,
                                                            Convert.ToInt32(SU_SD_msg),
                                                            PageSize);

            Msg_Length_Threshold = Properties.Settings.Default.Message_Length;
            ViewBag.Msg_Length_Threshold = Msg_Length_Threshold;
            return PartialView("MessageList", pagingContext);
        }


        /// <summary>
        /// This function is used to get the full detail version of a long message using msg_id
        /// </summary>
        /// <param name="msg_id">msg_id [string]</param>
        /// <returns> string </returns>
        [HttpPost]
        public string MsgInq_getfulldetail(string msg_id)
        {
            string result = MsgContentRepository.GetMessageContent(msg_id);
            return result;
        }


        /// <summary>
        /// This function is used to get the list for autocomplete search features
        /// </summary>
        /// <param name="search_string">search_string [string]</param>
        /// <returns> string </returns>
        [HttpPost]
        public string AutoCompleteUpdate( string search_string)
        {
            string autoCompleteResults = AutoCompleteRepository.GetAutoCompleteList(search_string);
            return autoCompleteResults;
        }
    }
}
