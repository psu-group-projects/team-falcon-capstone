using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    public class MsgContentRepository
    {
        
        public static string GetMessageContent(string msg_id)
        {
            int msg_id_int = Convert.ToInt32(msg_id);
            // API call will be here
            PeregrineService service = new PeregrineService();


            string content = service.getMessage(msg_id_int).ToString(); 
            return content;
        }
    }
}