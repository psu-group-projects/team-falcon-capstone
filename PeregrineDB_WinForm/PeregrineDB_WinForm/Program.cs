/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace PeregrineDB_WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form3());
               
         }
    }
}
