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

            List<ProcessSummary> OneProcessSummary;
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
                    SummaryData.Add(new Process { ProcessId = summary.ProcessID, ProcessName = summary.ProcessName, LastAction = summary.LastMsg, MsgDate = (System.DateTime)summary.MsgDate, _ProcessState = summary.State.ToString() });
                }
            }
            else
            {
                
                OneProcessSummary = service.getProcessByName(process_name);
                foreach (ProcessSummary summary in OneProcessSummary)
                {
                    SummaryData.Add(new Process { ProcessId = summary._process.ProcessID, ProcessName = summary._process.ProcessName, LastAction = summary._message.Message1, MsgDate = summary._message.Date, _ProcessState = summary._process.State.ToString() });
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