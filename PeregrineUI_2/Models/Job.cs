using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeregrineUI_2.Models
{
    /// <summary>
    /// Class : Job
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Gets or sets the name of the process
        /// </summary>
        /// <value>
        /// ProcessName.
        /// </value>
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the the content of the job
        /// </summary>
        /// <value>
        /// JobContent.
        /// </value>
        public string JobContent { get; set; }

        /// <summary>
        /// Gets or sets the percent progress.
        /// </summary>
        /// <value>
        /// PercentProgress.
        /// </value>
        public int PercentProgress { get; set; }
    }
}