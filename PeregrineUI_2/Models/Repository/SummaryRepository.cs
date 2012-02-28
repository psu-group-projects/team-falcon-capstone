using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    public class SummaryRepository
    {
        public static PageData<Process> GetSummaryDataByPage(int page, int sortColumm, int sort_type, string process_name, int pagesize)
        {
            List<Process> SummaryData = new List<Process>();
         
            // API call will be here
            PeregrineService service = new PeregrineService();

            List<GetProcessSummaryByNameResult> OneProcessSummary;
            List<GetPageOfProcessSummaryResult> ProcessSummaryData;

            if (process_name == "")
            {
                SortBy sort;
                switch (sortColumm)
                {
                    case 0:
                        sort = SortBy.PROCESS_NAME;
                        break;
                    case 1:
                        sort = SortBy.MESSAGE_CONTENT;
                        break;
                    case 2:
                        sort = SortBy.MESSAGE_DATE;
                        break;
                    case 3:
                        sort = SortBy.PROCESS_STATE;
                        break;
                    default:
                        sort = SortBy.PROCESS_STATE;
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


                ProcessSummaryData = service.getSummaryByPage(1, pagesize * page, sort, sortd);
                foreach (GetPageOfProcessSummaryResult summary in ProcessSummaryData)
                {
                    int percent;
                    if(summary.Percentage != null){
                        percent = (int)summary.Percentage;
                    }else{
                        percent = 0;
                    }
                    SummaryData.Add(new Process { ProcessId = summary.ProcessID, ProcessName = summary.ProcessName, LastAction = summary.LastMsg, MsgDate = (System.DateTime)summary.MsgDate, _ProcessState = summary.State.ToString(), MessageType = (int)summary.MsgType, JobPercentage = percent });
                }
            }
            else
            {
                
                OneProcessSummary = service.getProcessByName(process_name);
                foreach (GetProcessSummaryByNameResult summary in OneProcessSummary)
                {
                    int percent;
                    if (summary.Percentage != null)
                    {
                        percent = (int)summary.Percentage;
                    }else{
                        percent = 0;
                    }
                    SummaryData.Add(new Process { ProcessId = summary.ProcessID, ProcessName = summary.ProcessName, LastAction = summary.LastMsg, MsgDate = (System.DateTime)summary.MsgDate, _ProcessState = summary.State.ToString(), MessageType = (int)summary.MsgType, JobPercentage = percent });
                }
            }

            
            
            var pagingContext = new PageData<Process>();

           
            // Fill out the info of PageData var type
            pagingContext.Data = SummaryData;
            pagingContext.CurrentPage = page;
            pagingContext.SortingType = (sortColumm * 2) + sort_type;

            return pagingContext;

        }
    }
}