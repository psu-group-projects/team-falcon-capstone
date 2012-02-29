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
    /// <summary>
    /// Class : Message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        /// <value>
        /// ProcessName.
        /// </value>
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the state of the process.
        /// </summary>
        /// <value>
        /// ProcessState.
        /// </value>
        public string ProcessState { get; set; }

        /// <summary>
        /// Gets or sets the process ID
        /// </summary>
        /// <value>
        /// ProcessID.
        /// </value>
        public int ProcessID { get; set; }

        /// <summary>
        /// Gets or sets the message content.
        /// </summary>
        /// <value>
        /// Content
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the message priority.
        /// </summary>
        /// <value>
        /// Priority.
        /// </value>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the message category.
        /// </summary>
        /// <value>
        /// Category.
        /// </value>
        public int Category { get; set; }

        /// <summary>
        /// Gets or sets the type of the MSG.
        /// </summary>
        /// <value>
        /// MsgType.
        /// </value>
        public string MsgType { get; set; }

        /// <summary>
        /// Gets or sets the message date.
        /// </summary>
        /// <value>
        /// Date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the message ID.
        /// </summary>
        /// <value>
        /// MessageID.
        /// </value>
        public int MessageID { get; set; }

        /// <summary>
        /// Gets or sets the length of the message.
        /// </summary>
        /// <value>
        /// MessageLength.
        /// </value>
        public int MessageLength { get; set; }
    }
}