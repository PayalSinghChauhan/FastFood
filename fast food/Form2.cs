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
    public partial class Form2 : Form
    {
        private TabPage About;

        int i=0;

        decimal grandtotal = 0;
        String[] ordereditems = new string[40];
        String[] price = new string[40];
        String[] quantity = new string[40];
        public static string speical_name,rate;

        public Form2()
        {

            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = MenuTabPage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = OrderTabPage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Bill;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = About;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = About;
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
           tabControl1.SelectedTab = MenuTabPage;
           // button2.Enabled = true;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = OrderTabPage;
            foreach (Control ctrl in MenuTabPage.Controls)
            {
                if(ctrl is CheckBox)
                {
                    CheckBox cb = (CheckBox)ctrl;
                    if (cb.CheckState == CheckState.Checked)
                    {
                        ordereditems[i] = cb.Text;
                        price[i] = GetLabelText(cb.Name);
                        i++;
                    }
                }
            }

            int sn = 1; string myItemName = string.Empty; int myIndex=0;
            for (int i = 0; i < ordereditems.Length; i++)
            {
                if (!string.IsNullOrEmpty(ordereditems[i]))
                {
                    myIndex = ordereditems[i].IndexOf('R');
                    myItemName = ordereditems[i].Substring(0, myIndex);

                    dgvOrder.Rows.Add(sn, myItemName.Trim(), "", price[i], "");
                    sn++;
                }
            }
           // button2.Enabled = false;
            resetForm();
        }

        private void resetForm()
        {
            foreach (Control ctrl in MenuTabPage.Controls)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox cb = (CheckBox)ctrl;
                    if (cb.CheckState == CheckState.Checked)
                    {
                        cb.CheckState = CheckState.Unchecked;
                    }
                }
            }

            Array.Clear(ordereditems,0,ordereditems.Length);
            Array.Clear(price, 0, price.Length);
        }

        private string GetLabelText(string checkboxName)
        {
            string myText = string.Empty;
            int myLength = checkboxName.Length - 3;
            string labelName = "lbl" + checkboxName.Substring(3, myLength);

            foreach (Control ctrl in MenuTabPage.Controls)
            {
                if(ctrl is Label)
                {
                    Label lbl = (Label)ctrl;
                    if (lbl.Name == labelName)
                    {
                        myText = lbl.Text;
                        break;
                    }
                }
            }
            
            return myText;
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Bill;
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dgvOrder.Rows[i].Cells[2].FormattedValue.ToString()))
                {
                    MessageBox.Show("Quantity not set.");
                    dgvOrder.Rows[i].Cells[2].Selected = true;
                    return;
                }
            }
            grandtotal = 0;
            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {
                int qty = 0; decimal rate = 0, totalPrice = 0;
                int myLength = 0;
                myLength = dgvOrder.Rows[i].Cells[3].Value.ToString().Length;
                
                    qty = Convert.ToInt32(dgvOrder.Rows[i].Cells[2].Value);
                    rate = Convert.ToDecimal(dgvOrder.Rows[i].Cells[3].Value.ToString().Substring(2, myLength - 2).Trim());

                    totalPrice = qty * rate;
                    grandtotal += totalPrice;
                
            }
            txtTotal.Text = "Rs. " + grandtotal.ToString();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Bill;

            dvgbill.Rows.Clear();

            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {
                dvgbill.Rows.Add(dgvOrder.Rows[i].Cells[0].FormattedValue, dgvOrder.Rows[i].Cells[1].FormattedValue, dgvOrder.Rows[i].Cells[2].FormattedValue, dgvOrder.Rows[i].Cells[3].FormattedValue);
            }

            dvgbill.Rows.Add("", "", "", "");
            dvgbill.Rows.Add("", "Total Amount", "", txtTotal.Text);     
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Panel grd = new Panel();
            grd = panel1;
            Bitmap bmp = new Bitmap(grd.Width, grd.Height, grd.CreateGraphics());
            grd.DrawToBitmap(bmp, new Rectangle(0, 0, grd.Width, grd.Height));
            RectangleF bounds = e.PageSettings.PrintableArea;
            float factor = ((float)bmp.Height / (float)bmp.Width);
            e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, bounds.Width, factor * bounds.Width);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void clear_Click_1(object sender, EventArgs e)
        {
            dgvOrder.Rows.Clear();
            txtTotal.Text = "";
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Empty E-mail Not Allowed!!");
                textBox1.Focus();
                return;
            }
            System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
            doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);
            doc.Print();
        }
    }
}
