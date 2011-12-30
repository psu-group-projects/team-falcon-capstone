using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;


namespace PeregrineDB_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load_1(object sender, EventArgs e)
        {
            processBindingSource.DataSource
                = peregrineDB_formDataContext1.Processes;

  
            using (SqlConnection c = new SqlConnection(
            Properties.Settings.Default.PeregrineDBConnectionString))
            {
                c.Open();
                // 2
                // Create new DataAdapter
                using (SqlDataAdapter a = new SqlDataAdapter("SELECT * FROM Process", c))
                {
                    // 3
                    // Use DataAdapter to fill DataTable
                    DataTable t = new DataTable();
                    a.Fill(t);

                    // 4
                    // Render data onto the screen
                    // dataGridView1.DataSource = t; // <-- From your designer
                }
                c.Close();
            }



  

        }
      
        private PeregrineDB_formDataContext peregrineDB_formDataContext1
            = new PeregrineDB_formDataContext();

        private void processBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            peregrineDB_formDataContext1.UpdateProcesses(2,"UpdateProcess2",1);
            peregrineDB_formDataContext1.SubmitChanges();
        }

        private void processDataGridView_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        /*
        private void Form1_Load_1(object sender, EventArgs e)
        {


            var results = from pro in peregrineDB_formDataContext1.Processes
                          select pro;

            foreach(Process pro in results)
            {
                processDataGridView.Rows.Add(pro);
            }




        }

        */
        

    }
}

