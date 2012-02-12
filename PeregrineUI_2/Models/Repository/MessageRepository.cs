using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PeregrineUI_2.Models.Repository
{
    public class MessageRepository
    {
        /**-- This function is for Message Partial View in HomePage **/
        public static PageData<Message> GetMessageByProcess(int page, int pagesize, string processName)
        {
            List<Message> MessageByProcess = new List<Message>();
            Random random = new Random();

            int n = 10;

            for (int i = 0; i < n; i++)
            {
                MessageByProcess.Add(new Message { ProcessName = processName, Content = string.Concat(processName, Path.GetRandomFileName()), Priority = random.Next(1, 5), Category = random.Next(1, 5), Date = DateTime.Now });
            }


            var pagingContext = new PageData<Message>();

            int totalpage = Convert.ToInt32(Math.Ceiling((double)MessageByProcess.Count() / pagesize));

            // Handle the case when user want to fetch a page that < 1 or > total page
            if (page > totalpage)
                page = totalpage;
            else if (page < 1)
                page = 1;

            // Fill out the info of PageData var type
            pagingContext.Data = MessageByProcess.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            pagingContext.NumberOfPages = totalpage;
            pagingContext.CurrentPage = page;

            return pagingContext;
        }
    }
}