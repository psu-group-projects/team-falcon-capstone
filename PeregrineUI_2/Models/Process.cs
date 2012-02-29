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
    /// <summary>
    /// Class : Process
    /// </summary>
    public class Process
    {
        public string ProcessState;

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        /// <value>
        /// ProcessName.
        /// </value>
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the process last action.
        /// </summary>
        /// <value>
        /// LastAction.
        /// </value>
        public string LastAction { get; set; }

        /// <summary>
        /// Gets or sets the MSG date.
        /// </summary>
        /// <value>
        /// MsgDate.
        /// </value>
        public DateTime MsgDate { get; set; }

        /// <summary>
        /// Gets or sets the state of the process.
        /// </summary>
        /// <value>
        /// _ProcessState.
        /// </value>
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

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// MessageType.
        /// </value>
        public int MessageType { get; set; }

        /// <summary>
        /// Gets or sets the job percentage.
        /// </summary>
        /// <value>
        /// JobPercentage.
        /// </value>
        public int JobPercentage { get; set; }

        /// <summary>
        /// Gets or sets the process id.
        /// </summary>
        /// <value>
        /// ProcessId.
        /// </value>
        public int ProcessId { get; set; }
    }
}