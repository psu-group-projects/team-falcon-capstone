﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PeregrineUI_2.Models.Repository
{
    public class JobRepository
    {
        /**-- This function is for Job Partial View in HomePage **/
        public static PageData<Job> GetJobByProcess(int page, int pagesize, string processName)
        {
            List<Job> JobByProcess = new List<Job>();
            Random random = new Random();

            int n = 10;

            for (int i = 0; i < n; i++)
            {
                JobByProcess.Add(new Job { ProcessName = processName, JobContent = string.Concat(processName, Path.GetRandomFileName()), PercentProgress = random.Next(1, 100)});
            }


            var pagingContext = new PageData<Job>();

            int totalpage = Convert.ToInt32(Math.Ceiling((double)JobByProcess.Count() / pagesize));

            // Handle the case when user want to fetch a page that < 1 or > total page
            if (page > totalpage)
                page = totalpage;
            else if (page < 1)
                page = 1;

            // Fill out the info of PageData var type
            pagingContext.Data = JobByProcess.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
            pagingContext.NumberOfPages = totalpage;
            pagingContext.CurrentPage = page;

            return pagingContext;
        }
    }
}