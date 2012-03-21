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
    /// Class : MessageRepository
    /// </summary>
    public class MessageRepository
    {
        /// <summary>
        /// The function is used to get the list of messages by a process from DB throught Perergrine API 
        /// and supply the return list to home controller which will be populated into UI page : Message.cshtml
        /// </summary>
        /// <param name="page">page [int]</param>
        /// <param name="pagesize">pagesize [int]</param>
        /// <param name="processID">processID [int]</param>
        /// <returns>PageData[Message]</returns>
        public static PageData<Message> GetMessageByProcess(int page, int pagesize, int processID)
        {
            List<Message> MessageByProcess  = new List<Message>();
            var pagingContext               = new PageData<Message>();
            PeregrineService service        = new PeregrineService();
            List<PeregrineDB.Message> message_list = service.getPageOfMessagesByProcessId(processID, 1, page * pagesize);

            foreach (PeregrineDB.Message m in message_list)
            {
                MessageByProcess.Add(new Message { Category = m.Category, Date = m.Date, Content = m.Message1.Substring(0, Math.Min(Properties.Settings.Default.Message_Length, m.Message1.Length)), MessageLength = m.Message1.Length, MessageID = m.MessageID, Priority = m.Priority });
            }

            // Fill out the info of PageData var type
            pagingContext.Data = MessageByProcess;
            pagingContext.CurrentPage = page;

            return pagingContext;
        }
    }
}