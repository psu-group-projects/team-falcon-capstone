using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    public class MessageRepository
    {
        /**-- This function is for Message Partial View in HomePage **/
        public static PageData<Message> GetMessageByProcess(int page, int pagesize, int processID)
        {
            List<Message> MessageByProcess = new List<Message>();

            PeregrineService service = new PeregrineService();

            List<PeregrineDB.Message> message_list = service.getPageOfMessagesByProcessId(processID, 1, page * pagesize);

            foreach (PeregrineDB.Message m in message_list)
            {
                MessageByProcess.Add(new Message { Category = m.Category, Date = m.Date, Content = m.Message1, Priority = m.Priority });
            }

            var pagingContext = new PageData<Message>();

            // Fill out the info of PageData var type
            pagingContext.Data = MessageByProcess;
            //pagingContext.NumberOfPages = totalpage;
            pagingContext.CurrentPage = page;

            return pagingContext;
        }
    }
}