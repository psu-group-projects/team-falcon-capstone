/*
Author   : Chinh T Cao
           Anh T Nguyen
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PeregrineUI_2.Models
{
    public class Process
    {
        public string ProcessName { get; set; }
        public string LastAction { get; set; }
        public DateTime MsgDate { get; set; }
        public string ProcessState { get; set; }
 
    }
}