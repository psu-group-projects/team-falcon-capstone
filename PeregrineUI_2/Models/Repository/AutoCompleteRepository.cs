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

            /*
            AutoCompleteList.Add("Falcon10");
            AutoCompleteList.Add("Falcon11");
            AutoCompleteList.Add("Falcon12");
            AutoCompleteList.Add("Falcon13");
            AutoCompleteList.Add("Falcon14");
            AutoCompleteList.Add("Falcon15");
            AutoCompleteList.Add("Falcon16");
            AutoCompleteList.Add("Falcon17");
            AutoCompleteList.Add("Falcon18");
            AutoCompleteList.Add("Falcon19");
            AutoCompleteList.Add("Falcon20");
            AutoCompleteList.Add("Falcon21");
            AutoCompleteList.Add("Falcon22");
            AutoCompleteList.Add("Falcon23");
            AutoCompleteList.Add("Falcon24");
            AutoCompleteList.Add("Falcon25");
            AutoCompleteList.Add("Falcon26");
            AutoCompleteList.Add("Falcon27");
            AutoCompleteList.Add("Falcon28");
            AutoCompleteList.Add("Falcon29");
            */
            PeregrineService service = new PeregrineService();

            List<PeregrineDB.Process> process_list = service.searchProcessByName(search_string);
            foreach (PeregrineDB.Process p in process_list)
            {
                AutoCompleteList.Add(p.ProcessName);
            }

            string ans = string.Join(",", AutoCompleteList.ToArray());

            return ans;
        }
    }
}