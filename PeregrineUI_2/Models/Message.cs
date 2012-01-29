/*
Author   : Anh T Nguyen
           Chinh T Cao
Version  : 1.0.0
Date     : 1/12/2012
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeregrineUI_2.Models
{
    public class Message
    {
        public string ProcessName { get; set; }
        public string Content { get; set; }
        public int Priority { get; set; }
        public int Category { get; set; }
        public string MsgType { get; set; }
        public DateTime Date { get; set; }
    }

    public class Msg_inq : Message
    {
        public int ProcessID { get; set; }
    }

}