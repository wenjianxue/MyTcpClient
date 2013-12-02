using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MyTcpClient.Util;
using System.Net;
using System.Net.Sockets;

namespace MyTcpClient
{
    public partial class Form1 : Form
    {
        private ConfigHelp config;
        private static string TargetSection = "TARGET";
        private static string TargetIP = "targetip";
        private static string TargetPort = "targetport";

        private TcpClient tclient = null;

        public Form1()
        {
            InitializeComponent();

            string path = System.Environment.CurrentDirectory;
            config = new ConfigHelp(path + "/../../config.ini");
            string targetIp = config.IniReadValue(TargetSection, TargetIP);
            string targetport = config.IniReadValue(TargetSection, TargetPort);
            targetIptextBox.Text = targetIp;
            targetPorttextBox.Text = targetport;

            tclient = new TcpClient();
        }

        private void Conectbutton_Click(object sender, EventArgs e)
        {
            bool _Result = InputTargetValid(targetIptextBox.Text, targetPorttextBox.Text);

            if (_Result)
            {
                string targetIp = targetIptextBox.Text;
                string targetport = targetPorttextBox.Text;
                int intTargetIp = GFConvertHelp.StrToInt(targetport);
                if (tclient.Connected)
                {
                    tclient.Close();
                    tclient = null;
                }

                if (null == tclient)
                {
                    tclient = new TcpClient();
                }


                tclient.Connect(targetIp, intTargetIp);

            }
        }

        private bool InputTargetValid(string targetIp, string targetport)
        {
            bool blnTest = false;
            bool _Result = true;

            Regex regex = new Regex("^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
            blnTest = regex.IsMatch(targetIp);
            if (blnTest == true)
            {
                string[] strTemp = targetIp.Split(new char[] { '.' }); // textBox1.Text.Split(new char[] { '.' });
                for (int i = 0; i < strTemp.Length; i++)
                {
                    if (Convert.ToInt32(strTemp[i]) > 255)
                    { //大于255则提示，不符合IP格式 
                        MessageBox.Show("不符合IP格式");
                        _Result = false;
                    }
                }
            }
            else
            {
                //输入非数字则提示，不符合IP格式 
                MessageBox.Show("不符合IP格式");
                _Result = false;
            }

            return _Result;
        }

        private void CloseConectbutton_Click(object sender, EventArgs e)
        {
            tclient.Close();
            tclient = null;
        }

        private void Sendbutton_Click(object sender, EventArgs e)
        {
            NetworkStream ns = tclient.GetStream();

            String content = SendMsgtextBox.Text;

            byte[] data = Encoding.Unicode.GetBytes(content);

            ns.Write(data, 0, data.Length); 
        }
    }
}
