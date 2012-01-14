using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeregrineUI_2.Models
{
    public class Job
    {
        public string ProcessName { get; set; }
        public string JobContent { get; set; }
        public int PercentProgress { get; set; }
    }
}