using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeregrineWebUI.Models
{
    public class PageData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}