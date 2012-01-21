using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PeregrineUI_2.Models.Repository
{
    public class MsgInquiryRepo
    {
        public static PageData<Msg_inq> GetMessages(int page, int sortOption, int searchpriority, string searchprocess, int SU_SD_msg, int pagesize)
        {
            List<Msg_inq> SummaryData = new List<Msg_inq>();
            Random random = new Random();

            // Populate the SummaryData
            for (int i = 0; i < 200; i++)
            {
                Msg_inq element = new Msg_inq
                {
                    ProcessID = i,
                    ProcessName = "Falcon" + i,
                    Content = Path.GetRandomFileName(),
                    Priority = random.Next(1, 5),
                    Category = random.Next(1, 5),
                    Date = DateTime.Now
                };
                SummaryData.Add(element);
            }

            var pagingContext = new PageData<Msg_inq>();

            // Filtering if searching is actived
            if (SU_SD_msg == 1)
            {
                // Get startup and shutdown msg 
                // Implement in database

                // 
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
            switch (sortOption)
            {
                case 1:
                    SummaryData = SummaryData.OrderBy(p => p.ProcessID).ToList();
                    break;
                case 2:
                    SummaryData = SummaryData.OrderBy(p => p.ProcessName).ToList();
                    break;
                case 3:
                    SummaryData = SummaryData.OrderBy(p => p.Content).ToList();
                    break;
                case 4:
                    SummaryData = SummaryData.OrderBy(p => p.Priority).ToList();
                    break;
                case 5:
                    SummaryData = SummaryData.OrderBy(p => p.Date).ToList();
                    break;
                default:
                    break;
            }

            // Fill out the info of PageData var type
            pagingContext.Data = SummaryData.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            pagingContext.NumberOfPages = totalpage;
            pagingContext.CurrentPage = page;
            pagingContext.SortingType = sortOption;

            return pagingContext;
        }
    }
}