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
        public string ProcessState;
        public string _ProcessState
        {
            get { return this.ProcessState; }
            set
            {
                if(value == "0"){
                    this.ProcessState = "green";
                } else if (value == "1") {
                    this.ProcessState = "yellow";
                } else if (value == "2") {
                    this.ProcessState = "red";
                }
            }
        }
        public int MessageType { get; set; }
        public int JobPercentage { get; set; }
        public int ProcessId { get; set; }
    }
}