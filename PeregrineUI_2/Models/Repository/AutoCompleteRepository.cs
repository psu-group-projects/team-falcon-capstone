using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    /// <summary>
    /// Class : AutoCompleteRepository
    /// </summary>
    public class AutoCompleteRepository
    {
        /// <summary>
        /// This function is used for supply the list to autocomplete search
        /// The function is used to get the list of all available processes from DB throught Perergrine API 
        /// and supply the return list to home controller which will be populated into UI page : Index.cshtml and MsgInquiry.cshtml.
        /// </summary>
        /// <param name="search_string">search_string [string]</param>
        /// <returns>string</returns>
        public static string GetAutoCompleteList(string search_string){
            List<string> AutoCompleteList = new List<string>();
            PeregrineService service = new PeregrineService();

            List<PeregrineDB.Process> process_list = service.searchProcessByName(search_string);
            foreach (PeregrineDB.Process p in process_list)
            {
                AutoCompleteList.Add(p.ProcessName.Trim());
            }

            string ans = string.Join(",", AutoCompleteList.Take(10).ToArray());     

            return ans;
        }
    }
}