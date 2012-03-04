using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeregrineDB_WinForm
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PeregrineAPI.PeregrineService service = new PeregrineAPI.PeregrineService();
            service.logProcessMessage(comboBox1.Text,richTextBox1.Text, PeregrineAPI.Category.INFORMATION, PeregrineAPI.Priority.HIGH);
            service.logJobProgressAsPercentage(comboBox2.Text, comboBox1.Text,Convert.ToDouble(comboBox3.Text));
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }
}
