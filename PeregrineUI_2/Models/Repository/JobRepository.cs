using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    public class JobRepository
    {
        /**-- This function is for Job Partial View in HomePage **/
        public static PageData<Job> GetJobByProcess(int page, int pagesize, int processID)
        {
            List<Job> JobByProcess= new List<Job>();
            var pagingContext = new PageData<Job>();


            PeregrineService service = new PeregrineService();
            List<PeregrineDB.Job> Jobs = service.getPageOfJobsByProcessId(processID, 1, page * pagesize);

            foreach (PeregrineDB.Job j in Jobs)
            {
                JobByProcess.Add(new Job { JobContent = j.JobName, PercentProgress = (int)j.PercentComplete });
            }

           // Jobs = service.getPageOfJobsByProcessId(int.Parse(processID), page, pagesize).ToList<PeregrineDB.Job>();
            /*
            foreach (PeregrineDB.Job j in Jobs)
            {
                Jobs.Add(new Job { ProcessName =  });
            }*/
            /*
            Random random = new Random();

            int n = 100;

            for (int i = 0; i < n; i++)
            {
                JobByProcess.Add(new Job { ProcessName = processID, JobContent = string.Concat(processID, Path.GetRandomFileName()), PercentProgress = random.Next(1, 100)});
            }*/


            //var pagingContext = new PageData<Job>();

            /*int totalpage = Convert.ToInt32(Math.Ceiling((double)JobByProcess.Count() / pagesize));

            // Handle the case when user want to fetch a page that < 1 or > total page
            if (page > totalpage)
                page = totalpage;
            else if (page < 1)
                page = 1;
            */

            // Fill out the info of PageData var type
            pagingContext.Data = JobByProcess;
            //pagingContext.NumberOfPages = totalpage;
            pagingContext.CurrentPage = page;

            return pagingContext;
        }
    }
}