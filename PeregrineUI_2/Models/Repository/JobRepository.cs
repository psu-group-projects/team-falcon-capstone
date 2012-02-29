using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PeregrineAPI;
using PeregrineDB;

namespace PeregrineUI_2.Models.Repository
{
    /// <summary>
    /// Class : JobRepository
    /// </summary>
    public class JobRepository
    {
        /// <summary>
        /// The function is used to get the list of jobs by a process from DB throught Perergrine API 
        /// and supply the return list to home controller which will be populated into UI page : Job.cshtml
        /// </summary>
        /// <param name="page">page [int]</param>
        /// <param name="pagesize">pagesize [int]</param>
        /// <param name="processID">processID [int]</param>
        /// <returns>PageData[Job]</returns>
        public static PageData<Job> GetJobByProcess(int page, int pagesize, int processID)
        {
            List<Job> JobByProcess      = new List<Job>();
            var pagingContext           = new PageData<Job>();
            PeregrineService service    = new PeregrineService();
            List<PeregrineDB.Job> Jobs = service.getPageOfJobsByProcessId(processID, 1, page * pagesize);

            foreach (PeregrineDB.Job j in Jobs)
            {
                JobByProcess.Add(new Job { JobContent = j.JobName, PercentProgress = (int)j.PercentComplete });
            }

            // Fill out the info of PageData var type
            pagingContext.Data = JobByProcess;
            pagingContext.CurrentPage = page;

            return pagingContext;
        }
    }
}