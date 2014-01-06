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
using System.Threading;

namespace MyTcpClient
{
    public partial class Form1 : Form
    {
        private ConfigHelp config;
        private static string TargetSection = "TARGET";
        private static string TargetIP = "targetip";
        private static string TargetPort = "targetport";

        private TcpClient tclient = null;

        private Thread oThread = null;
        bool bListen = true;

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
            oThread = new Thread(new ThreadStart(this.listen));
            oThread.Start();

        }

        private void Conectbutton_Click(object sender, EventArgs e)
        {
            bool _Result = InputTargetValid(targetIptextBox.Text, targetPorttextBox.Text);

            if (_Result)
            {
                string targetIp = targetIptextBox.Text;
                string targetport = targetPorttextBox.Text;
                int intTargetIp = GFConvertHelp.StrToInt(targetport);
                if (null == tclient)
                {
                    tclient = new TcpClient();
                }
                if (tclient.Connected)
                {
                    tclient.Close();
                    tclient = null;
                    tclient = new TcpClient();
                }

                try
                {
                    tclient.Connect(targetIp, intTargetIp);
                }
                catch(Exception ex)
                {
                   
                    AddStringRecvMsgtextBox(ex.Message);
                }
                

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
            bListen = false;
            if (tclient != null)
            {
                tclient.Close();
           
            }

        }

        private void Sendbutton_Click(object sender, EventArgs e)
        {
            if (tclient != null && tclient.Connected )
            {
                bListen = true;
                NetworkStream ns = tclient.GetStream();

                String content = SendMsgtextBox.Text;

                byte[] byteArray = System.Text.Encoding.Default.GetBytes(content);

                ns.Write(byteArray, 0, byteArray.Length);
            }
            
            
        }

        public void listen()
        {
            
             while (bListen)
             {

                        if (tclient != null && tclient.Connected && tclient.GetStream().CanRead)
                        {
                            NetworkStream stream = tclient.GetStream();

                            byte[] myReadBuffer = new byte[1024];
                            int dasd = tclient.GetStream().Read(myReadBuffer, 0, myReadBuffer.Length);
                            String sdf = Encoding.Default.GetString(myReadBuffer);
                            this.SetText(sdf);
                        }
            }
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.RecvMsgtextBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                AddStringRecvMsgtextBox(text);
            }
        }


        delegate void SetTextCallback(string text);

        private void AddStringRecvMsgtextBox(string text)
        {
            this.RecvMsgtextBox.Text += text;
            this.RecvMsgtextBox.Text += Environment.NewLine;
        }
    }
}
