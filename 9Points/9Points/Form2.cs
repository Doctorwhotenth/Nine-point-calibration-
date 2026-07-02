using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _9Points
{
    public partial class Form2 : Form
    {
        private Form1 Form1;
        List<double> prx = new List<double>();
        List<double> pry = new List<double>();
        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.Form1 = form1;
            listView1.View = View.Details;
            listView1.Columns.Add("序号", 44, HorizontalAlignment.Center);
            listView1.Columns.Add("PX", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("Py", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("RobotNowX", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("RobotNowY", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("AbsX", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("AbsY", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("RelX", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("RelY", 107, HorizontalAlignment.Center);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double dou = double.Parse(textBox1.Text);
                dou = double.Parse(textBox2.Text);
                dou = double.Parse(textBox3.Text);
                dou = double.Parse(textBox4.Text);

                int i = listView1.Items.Count + 1;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                lvi.SubItems.Add(textBox1.Text);
                lvi.SubItems.Add(textBox2.Text);
                lvi.SubItems.Add(textBox3.Text);
                lvi.SubItems.Add(textBox4.Text);
                lvi.SubItems.Add(textBox11.Text);
                lvi.SubItems.Add(textBox10.Text);
                double x = double.Parse(textBox3.Text) - double.Parse(textBox11.Text);
                double y = double.Parse(textBox4.Text) - double.Parse(textBox10.Text);
                lvi.SubItems.Add(x.ToString());
                lvi.SubItems.Add(y.ToString());
                listView1.Items.Add(lvi);

            }
            catch
            {
                MessageBox.Show("输入格式错误！");
            }
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                int i = listView1.SelectedItems[0].Index;
                listView1.Items.RemoveAt(i);  // 按索引移除
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                int i = listView1.SelectedItems[0].Index;
                listView1.Items[i].SubItems[1].Text = textBox1.Text;
                listView1.Items[i].SubItems[2].Text = textBox2.Text;
                listView1.Items[i].SubItems[3].Text = textBox3.Text;
                listView1.Items[i].SubItems[4].Text = textBox4.Text;
                listView1.Items[i].SubItems[5].Text = textBox11.Text;
                listView1.Items[i].SubItems[6].Text = textBox10.Text;
                double x = double.Parse(textBox3.Text) - double.Parse(textBox11.Text);
                double y = double.Parse(textBox4.Text) - double.Parse(textBox10.Text);
                listView1.Items[i].SubItems[7].Text = x.ToString();
                listView1.Items[i].SubItems[8].Text = y.ToString();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                int i = listView1.SelectedItems[0].Index;
                textBox1.Text = listView1.Items[i].SubItems[1].Text;
                textBox2.Text = listView1.Items[i].SubItems[2].Text;
                textBox3.Text = listView1.Items[i].SubItems[3].Text;
                textBox4.Text = listView1.Items[i].SubItems[4].Text;
                textBox11.Text = listView1.Items[i].SubItems[5].Text;
                textBox10.Text = listView1.Items[i].SubItems[6].Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int length = listView1.Items.Count;
            double[,] orgp = new double[length, 3];
            for (int i = 0; i < length; i++)
            {
                orgp[i, 0] = double.Parse(listView1.Items[i].SubItems[1].Text);
                orgp[i, 1] = double.Parse(listView1.Items[i].SubItems[2].Text);
                orgp[i, 2] = 1;
            }

            double[,] rx = new double[length, 1];
            double[,] ry = new double[length, 1];
            for (int i = 0; i < length; i++)
            {
                rx[i, 0] = double.Parse(listView1.Items[i].SubItems[7].Text);
                ry[i, 0] = double.Parse(listView1.Items[i].SubItems[8].Text);
            }

            double[,] MatT = Matrix.MatrixT(orgp);
            double[,] xtx = Matrix.MatMul(MatT, orgp);
            double[,] inv = Matrix.inv3(xtx);
            double[,] invxt = Matrix.MatMul(inv, MatT);
            double[,] QX = Matrix.MatMul(invxt, rx);
            double[,] QY = Matrix.MatMul(invxt, ry);

            textBox5.Text = "Qx = " + QX[0, 0].ToString("f5")
                     + "X+" + QX[1, 0].ToString("f5")
                     + "Y+" + QX[2, 0].ToString("f5") + "\r\n" +
                    "Qy = " + QY[0, 0].ToString("f5")
                     + "X+" + QY[1, 0].ToString("f5")
                     + "Y+" + QY[2, 0].ToString("f5");

            prx.Add(QX[0, 0]);
            prx.Add(QX[1, 0]);
            prx.Add(QX[2, 0]);

            pry.Add(QY[0, 0]);
            pry.Add(QY[1, 0]);
            pry.Add(QY[2, 0]);
        }

        private void output_Click(object sender, EventArgs e)
        {
            Hxml.SaveXmlTwoNode2(listView1);
        }

        private void input_Click(object sender, EventArgs e)
        {
            Hxml.ReadXmlTwoNode2(listView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(textBox9.Text);
                double y = double.Parse(textBox8.Text);
                double rx = prx[0] * x + prx[1] * y + prx[2];
                double ry = pry[0] * x + pry[1] * y + pry[2];
                textBox7.Text = rx.ToString();
                textBox6.Text = ry.ToString();
            }
            catch
            {
                MessageBox.Show("输入格式错误！");
            }
        }
    }
}
