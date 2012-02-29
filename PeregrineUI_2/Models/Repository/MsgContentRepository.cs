using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    /// <summary>
    /// Class : MsgContentRepository
    /// </summary>
    public class MsgContentRepository
    {
        /// <summary>
        /// Get the content of the long message from DB throught Perergrine API 
        /// and supply the return string to home controller which will be populated into UI page : MsgInquiry.cshtml
        /// </summary>
        /// <param name="msg_id">The msg_id [string]</param>
        /// <returns> string </returns>
        public static string GetMessageContent(string msg_id)
        {
            int msg_id_int = Convert.ToInt32(msg_id);
            PeregrineService service = new PeregrineService();

            string content = service.getMessage(msg_id_int).Message1;
            return content;
        }
    }
}