/*
Author   : Chinh T Cao
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeregrineUI_2.Models
{
    /// <summary>
    /// Class : PageData
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T> where T : class
    {
        /// <summary>
        /// Gets or sets the page data.
        /// </summary>
        /// <value>
        /// Data
        /// </value>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        /// <value>
        /// NumberOfPages.
        /// </value>
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// CurrentPage.
        /// </value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the type of the sorting.
        /// </summary>
        /// <value>
        /// SortingType.
        /// </value>
        public int SortingType { get; set; }
    }
}