using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
namespace _9Points
{
    class Hxml
    {

        public static void ReadXmlTwoNode(ListView listView)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory; //使用当前目录作为初始目录
            dialog.Filter = "xml文件(*.xml)|*.xml";   //文件过滤，仅打开txt
            if (dialog.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                string path = dialog.FileName;//获取在文件对话框中选定的路径或者字符串
                XDocument xdoc = new XDocument();
                xdoc = XDocument.Load(path);
                XElement root = xdoc.Root;           
                IEnumerable<XElement> xns = root.Elements();
                int count = xns.Count();
                int i = 1;
                foreach (XElement xn in xns)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.Add(xn.Element("PX").Value);
                    lvi.SubItems.Add(xn.Element("PY").Value);
                    lvi.SubItems.Add(xn.Element("RX").Value);
                    lvi.SubItems.Add(xn.Element("RY").Value);
                    listView.Items.Add(lvi);
                    i++;
                }
                xdoc.Save(path);

            }            
        }

        public static void SaveXmlTwoNode(ListView listView)
        {
            string name = DateTime.Now.ToString("yyyyMMddHHmmss")+".xml";
            XDocument xdoc = new XDocument();
            XElement root = new XElement("points");//根节点
            for (int i = 0; i < listView.Items.Count; i++)
            {
                XElement xmlTree1 = new XElement("point"+(i+1).ToString(),
                    new XElement("PX", listView.Items[i].SubItems[1].Text),
                    new XElement("PY", listView.Items[i].SubItems[2].Text),
                    new XElement("RX", listView.Items[i].SubItems[3].Text),
                    new XElement("RY", listView.Items[i].SubItems[4].Text)
                                        );
                root.Add(xmlTree1); 
            }
            xdoc.Add(root);
            xdoc.Save(name);

        }


        public static void ReadXmlTwoNode2(ListView listView)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory; //使用当前目录作为初始目录
            dialog.Filter = "xml文件(*.xml)|*.xml";   //文件过滤，仅打开txt
            if (dialog.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                string path = dialog.FileName;//获取在文件对话框中选定的路径或者字符串
                XDocument xdoc = new XDocument();
                xdoc = XDocument.Load(path);
                XElement root = xdoc.Root;
                IEnumerable<XElement> xns = root.Elements();
                int count = xns.Count();
                int i = 1;
                foreach (XElement xn in xns)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.Add(xn.Element("PX").Value);
                    lvi.SubItems.Add(xn.Element("PY").Value);
                    lvi.SubItems.Add(xn.Element("RobotNowX").Value);
                    lvi.SubItems.Add(xn.Element("RobotNowY").Value);
                    lvi.SubItems.Add(xn.Element("AbsX").Value);
                    lvi.SubItems.Add(xn.Element("AbsY").Value);
                    lvi.SubItems.Add(xn.Element("RelX").Value);
                    lvi.SubItems.Add(xn.Element("RelY").Value);
                    listView.Items.Add(lvi);
                    i++;
                }
                xdoc.Save(path);

            }
        }

        public static void SaveXmlTwoNode2(ListView listView)
        {
            string name = "R" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            XDocument xdoc = new XDocument();
            XElement root = new XElement("points");//根节点
            for (int i = 0; i < listView.Items.Count; i++)
            {
                XElement xmlTree1 = new XElement("point" + (i + 1).ToString(),
                    new XElement("PX", listView.Items[i].SubItems[1].Text),
                    new XElement("PY", listView.Items[i].SubItems[2].Text),
                    new XElement("RobotNowX", listView.Items[i].SubItems[3].Text),
                    new XElement("RobotNowY", listView.Items[i].SubItems[4].Text),
                    new XElement("AbsX", listView.Items[i].SubItems[5].Text),
                    new XElement("AbsY", listView.Items[i].SubItems[6].Text),
                    new XElement("RelX", listView.Items[i].SubItems[7].Text),
                    new XElement("RelY", listView.Items[i].SubItems[8].Text)
                                        );
                root.Add(xmlTree1);
            }
            xdoc.Add(root);
            xdoc.Save(name);

        }

    }
}
