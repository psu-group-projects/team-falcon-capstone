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

            // Fill out the info of PageData var type
            pagingContext.Data = JobByProcess;
            //pagingContext.NumberOfPages = totalpage;
            pagingContext.CurrentPage = page;

            return pagingContext;
        }
    }
}