using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PeregrineWebUI.Models.Repository
{
    public class MessageRepository
    {
        public static IList<Message> MessageData = null;

        public static PageData<Message> GetMessageDatas(int page, int sortOption, string searchValue, int linesPerPage)
        {
            // Call the API to get data. After that, create MessageData
            Random random = new Random();

            MessageData = new List<Message>();
            MessageData.Add(new Message() { ID = 1, processName = "Falcon1", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 2, processName = "Falcon2", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 3, processName = "Falcon3", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 4, processName = "Falcon4", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 5, processName = "Falcon5", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 6, processName = "Falcon6", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 7, processName = "Falcon7", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 8, processName = "Falcon8", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 9, processName = "Falcon9", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 10, processName = "Eagle1", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 11, processName = "Eagle2", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 12, processName = "Eagle3", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 13, processName = "Eagle4", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });
            MessageData.Add(new Message() { ID = 14, processName = "Eagle5", content = Path.GetRandomFileName(), priority = random.Next(0, 5), Date = DateTime.Now });

            // get the list of messages in requested page
            PageData<Message> pageData = new PageData<Message>();
            pageData.Data = MessageData.OrderBy(p => p.processName).Skip(linesPerPage * (page - 1)).Take(linesPerPage).ToList();

            // get numberOfPages
            int numberOfPages = MessageData.Count / linesPerPage;
            pageData.NumberOfPages = numberOfPages;

            return pageData;
        }
    }
}