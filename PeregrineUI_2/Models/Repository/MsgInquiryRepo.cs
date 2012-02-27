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
                    Content = summary.Message.Substring(0, Math.Min(60, summary.Message.Length)),
                    MessageLength = summary.Message.Length,
                    Date = summary.Date,
                    Priority = summary.Priority,
                    MsgType = msgType
                });
            }

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