using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
namespace _9Points
{
    public partial class Form1 : Form
    {
        List<double> prx = new List<double>();
        List<double> pry = new List<double>();
        Form2 form2;
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("序号", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("像素(Pix_X)", 135, HorizontalAlignment.Center);
            listView1.Columns.Add("像素(Pix_y)", 135, HorizontalAlignment.Center);
            listView1.Columns.Add("机器(Robot_X)", 135, HorizontalAlignment.Center);
            listView1.Columns.Add("机器(Robot_Y)", 135, HorizontalAlignment.Center);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            int length = listView1.Items.Count;
            //将列表中的数据转为矩阵形式  Convert the data in the list to a matrix format
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
                rx[i, 0] = double.Parse(listView1.Items[i].SubItems[3].Text);
                ry[i, 0] = double.Parse(listView1.Items[i].SubItems[4].Text);
            }
            //Matrix Operations
            double[,] MatT = Matrix.MatrixT(orgp);
            double[,] xtx = Matrix.MatMul(MatT, orgp);
            double[,] inv = Matrix.inv3(xtx);
            double[,] invxt = Matrix.MatMul(inv, MatT);
            double[,] QX = Matrix.MatMul(invxt, rx);
            double[,] QY = Matrix.MatMul(invxt, ry);

            //result
            textBox5.Text = "Qx = " + QX[0,0].ToString("f5")
                     + "X+" + QX[1,0].ToString("f5")
                     + "Y+" + QX[2,0].ToString("f5") + "\r\n" +
                    "Qy = " + QY[0,0].ToString("f5")
                     + "X+" + QY[1,0].ToString("f5")
                     + "Y+" + QY[2,0].ToString("f5");

            prx.Add(QX[0, 0]);
            prx.Add(QX[1, 0]);
            prx.Add(QX[2, 0]);

            pry.Add(QY[0, 0]);
            pry.Add(QY[1, 0]);
            pry.Add(QY[2, 0]);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //Add Point from textBox and display on listview
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
                listView1.Items.Add(lvi);

            }
            catch
            {
                MessageBox.Show("输入格式错误！Input format error!");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //remove error point
            if (listView1.SelectedIndices.Count > 0)
            {
                int i = listView1.SelectedItems[0].Index;
                listView1.Items.RemoveAt(i);  
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //modify point
            if (listView1.SelectedIndices.Count > 0)
            {
                int i = listView1.SelectedItems[0].Index;
                listView1.Items[i].SubItems[1].Text = textBox1.Text;
                listView1.Items[i].SubItems[2].Text = textBox2.Text;
                listView1.Items[i].SubItems[3].Text = textBox3.Text;
                listView1.Items[i].SubItems[4].Text = textBox4.Text;
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
            }
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

        private void DataOutput_Click(object sender, EventArgs e)
        {
            Hxml.SaveXmlTwoNode(listView1);
        }

        private void DataInput_Click(object sender, EventArgs e)
        {
            Hxml.ReadXmlTwoNode(listView1);
        }

        private void othermodel_Click(object sender, EventArgs e)
        {
            form2 = new Form2(this);
            form2.Owner = this;
            form2.Show();
        }
    }
}
