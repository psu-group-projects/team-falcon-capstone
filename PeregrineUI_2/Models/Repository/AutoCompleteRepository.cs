using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    public class AutoCompleteRepository
    {
        public static string GetAutoCompleteList(string search_string){
            List<string> AutoCompleteList = new List<string>();
            PeregrineService service = new PeregrineService();

            List<PeregrineDB.Process> process_list = service.searchProcessByName(search_string);
            foreach (PeregrineDB.Process p in process_list)
            {
                AutoCompleteList.Add(p.ProcessName.Trim());
            }

            string ans = string.Join(",", AutoCompleteList.ToArray());

            return ans;
        }
    }
}