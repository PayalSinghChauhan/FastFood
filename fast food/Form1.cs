using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fast_food
{
    public partial class Form1 : Form
    {
        private int _ticks;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }
                       
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
            //_ticks++;

            //if (_ticks == 5)
            //{
            //    timer1.Stop();
            //    this.Hide();
            //    Form2 f2 = new Form2();
            //    f2.ShowDialog();
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
