using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    /// <summary>
    /// Class SummaryRepository
    /// </summary>
    public class SummaryRepository
    {
        /// <summary>
        /// The function is used to get the list of process from DB throught Perergrine API 
        /// and supply the return list to home controller which will be populated into UI page : ProcessList.cshtml
        /// </summary>
        /// <param name="page">page [int]</param>
        /// <param name="sortColumm">sortColumm [int]</param>
        /// <param name="sort_type">sort_type [int]</param>
        /// <param name="process_name">process_name [int]</param>
        /// <param name="pagesize">pagesize [int]</param>
        /// <returns> PageData[Process] </returns>
        public static PageData<Process> GetSummaryDataByPage(int page, int sortColumm, int sort_type, string process_name, int pagesize)
        {
            List<Process> SummaryData   = new List<Process>();      
            PeregrineService service    = new PeregrineService();
            var pagingContext           = new PageData<Process>();
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
                    int percent, MsgType;

                    if(summary.Percentage != null){
                        percent = (int)summary.Percentage;
                    }else{
                        percent = 0;
                    }

                    if (summary.MsgType != null)
                    {
                        MsgType = (int)summary.MsgType;
                    }
                    else
                    {
                        MsgType = 0;
                    }

                    SummaryData.Add(new Process { ProcessId = summary.ProcessID, ProcessName = summary.ProcessName, LastAction = summary.LastMsg, MsgDate = (System.DateTime)summary.MsgDate, _ProcessState = summary.State.ToString(), MessageType = MsgType, JobPercentage = percent });
                }
            }
            else
            {
                
                OneProcessSummary = service.getProcessByName(process_name);
                foreach (GetProcessSummaryByNameResult summary in OneProcessSummary)
                {
                    SummaryData.Add(new Process { ProcessId = summary.ProcessID, ProcessName = summary.ProcessName, LastAction = summary.LastMsg, MsgDate = (System.DateTime)summary.MsgDate, _ProcessState = summary.State.ToString(), MessageType = (int)summary.MsgType, JobPercentage = (int)summary.Percentage });
                }
            }
    
            // Fill out the info of PageData var type
            pagingContext.Data = SummaryData;
            pagingContext.CurrentPage = page;
            pagingContext.SortingType = (sortColumm * 2) + sort_type;

            return pagingContext;

        }
    }
}