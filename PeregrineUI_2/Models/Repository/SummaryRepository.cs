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

            List<ProcessSummary> ProcessSummaryData;

            if (process_name == "")
            {
                ProcessSummaryData = service.getSummaryByPage(1, pagesize * page, SortBy.PROCESS_STATE, 0);
            }
            else
            {
                ProcessSummaryData = service.getProcessByName(process_name);
            }

            foreach (ProcessSummary summary in ProcessSummaryData)
            {
                SummaryData.Add(new Process { ProcessId = summary._process.ProcessID, ProcessName = summary._process.ProcessName, LastAction = summary._message.Message1, MsgDate = summary._message.Date, _ProcessState = summary._process.State.ToString() });
            }
            
            /*
            SummaryData.Add(new Process { ProcessName = "Falcon10", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon11", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon12", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon13", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "RED" });
            SummaryData.Add(new Process { ProcessName = "Falcon14", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon15", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon16", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon17", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon18", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon19", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon20", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon21", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon22", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon23", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon24", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon25", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon26", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon27", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon28", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon29", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon30", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon31", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon32", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon33", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon34", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon35", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon36", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon37", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon38", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon39", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon40", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon41", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon42", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon43", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon44", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "RED" });
            SummaryData.Add(new Process { ProcessName = "Falcon45", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon46", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon47", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon48", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon49", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon50", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon51", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon52", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon53", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon54", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon55", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon56", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon57", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon58", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon59", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon60", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon61", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon62", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon63", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon64", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon65", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon66", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon67", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon68", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon69", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon70", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon71", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon72", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon73", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon74", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon75", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon76", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon77", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon78", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon79", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon80", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon81", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon82", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon83", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            SummaryData.Add(new Process { ProcessName = "Falcon84", LastAction = Path.GetRandomFileName(), MsgDate = DateTime.Now, ProcessState = "GREEN" });
            */

            var pagingContext = new PageData<Process>();

            // Filtering if searching is actived
            //if (process_name != "")
            //    SummaryData = SummaryData.Where(p => p.ProcessName.Contains(process_name)).ToList();

            //int totalpage = Convert.ToInt32(Math.Ceiling((double)SummaryData.Count() / pagesize));

            // Handle the case when user want to fetch a page that < 1 or > total page
            //if (page > totalpage)
            //    page = totalpage;
            //else if (page < 1)
            //    page = 1;

            // Do the sorting
            /*
            switch (sortColumm)
            {
                case 0:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.ProcessName).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.ProcessName).ToList();
                    }
                    break;
                case 1:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.LastAction).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.LastAction).ToList();
                    }
                    break;
                case 2:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.MsgDate).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.MsgDate).ToList();
                    }
                    break;
                case 3:
                    {
                        if (sort_type == 0)
                            SummaryData = SummaryData.OrderBy(p => p.ProcessState).ToList();
                        else
                            SummaryData = SummaryData.OrderByDescending(p => p.ProcessState).ToList();
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
    }
}