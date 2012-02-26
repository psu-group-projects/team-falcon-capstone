using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    public class MsgInquiryRepo
    {
        public static PageData<Message> GetMessages(int page, int sortColumm, int sort_type, int searchpriority, string searchprocess, int SU_SD_msg, int pagesize)
        {
            List<Message> SummaryData = new List<Message>();
            var pagingContext = new PageData<Message>();

            PeregrineService service = new PeregrineService();

            List<GetPageOfMessageSummaryResult> MessageSummaryData;

            SortBy sort;
            switch (sortColumm)
            {
                case 0:
                    sort = SortBy.MESSAGE_CONTENT;
                    break;
                case 1:
                    sort = SortBy.PROCESS_NAME;
                    break;
                case 2:
                    sort = SortBy.MESSAGE_PRIORITY;
                    break;
                case 3:
                    sort = SortBy.MESSAGE_DATE;
                    break;
                default:
                    sort = SortBy.MESSAGE_DATE;
                    break;
            }

            SortDirection sortd;
            switch (sort_type)
            {
                case 0:
                    sortd = SortDirection.ASSENDING;
                    break;
                case 1:
                    sortd = SortDirection.DESENDING;
                    break;
                default:
                    sortd = SortDirection.DESENDING;
                    break;
            }

            MessageSummaryData = service.getMessagesForMessageInq(searchprocess, page * pagesize, searchpriority, SU_SD_msg, sort, sortd);

            foreach (GetPageOfMessageSummaryResult summary in MessageSummaryData)
            {
                String msgType = getMessageTypeString((Category)summary.Category);
                String state = getProcessStateString((int)summary.ProcState);
                SummaryData.Add(new Message
                {
                    MessageID = summary.MessageID,
                    ProcessID = (int)summary.ProcID,
                    ProcessName = summary.ProcName,
                    ProcessState = state,
                    Category = summary.Category,
                    Content = summary.Message,
                    Date = summary.Date,
                    Priority = summary.Priority,
                    MsgType = msgType
                });
            }

            /*
            Random random = new Random();

            // Populate the SummaryData
            for (int i = 0; i < 200; i++)
            {
                Message element = new Message
                {
                    MessageID = i,
                    ProcessID = i,
                    ProcessState = (random.Next(1,3) == 2) ? "red" : "green",
                    ProcessName = "Falcon" + i,
                    Content = Path.GetRandomFileName(),
                    Priority = random.Next(1, 10),
                    Category = random.Next(1, 5),
                    MsgType = (random.Next(1, 3) == 1) ? "Normal Message" : "Startup and Shutdown Message",
                    Date = DateTime.Now
                };
                SummaryData.Add(element);
            }

            

            // Filtering if searching is actived
            if (SU_SD_msg == 1)
            {
                SummaryData = SummaryData.Where(p => p.MsgType == "Startup and Shutdown Message").ToList();   
            }
            
            // Filtering if searching is actived
            if (searchprocess != "")
                SummaryData = SummaryData.Where(p => p.ProcessName.Contains(searchprocess)).ToList();
            if (searchpriority != 0)
                SummaryData = SummaryData.Where(p => p.Priority == searchpriority).ToList();

            int totalpage = Convert.ToInt32(Math.Ceiling((double)SummaryData.Count() / pagesize));

            // Handle the case when user want to fetch a page that < 1 or > total page
            if (page > totalpage)
                page = totalpage;
            else if (page < 1)
                page = 1;

            // Do the sorting
            switch (sortColumm)
            {
                case 0:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.MessageID).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.MessageID).ToList();
                    }
                    break;
                case 1:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.Content).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.Content).ToList();
                    }
                    break;
                case 2:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.ProcessName).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.ProcessName).ToList();
                    }         
                    break;               
                case 3:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.Priority).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.Priority).ToList();
                    }     
                    break;
                case 4:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.Date).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.Date).ToList();
                    } 
                    break;             
                default:
                    break;
            }
            */
            // Fill out the info of PageData var type
            //pagingContext.Data = SummaryData.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            pagingContext.Data = SummaryData;
            //pagingContext.NumberOfPages = totalpage;
            pagingContext.CurrentPage = page;
            pagingContext.SortingType = (sortColumm * 2) + sort_type;

            return pagingContext;
        }

        private static String getMessageTypeString(Category c){
            switch (c)
            {
                case Category.START:
                    return "Start Up";
                case Category.STOP:
                    return "Shut Down";
                case Category.INFORMATION:
                    return "Information";
                case Category.STATE_CHANGE:
                    return "State Change";
                case Category.PROGRESS:
                    return "Job Progress";
                case Category.ERROR:
                    return "ERROR";
                default:
                    return "Unknown";
            }
        }

        private static String getProcessStateString(int state){
            if (state == 0) {
                return "green";
            }else if (state == 1) {
                return "yellow";
            }else {
                return "red";
            }
        }
    }
}