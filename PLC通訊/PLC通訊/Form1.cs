using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace PLC通訊
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 9600;
                serialPort1.DataBits = 7;
                serialPort1.Parity = Parity.Even;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Open();
                string cmd = ch + change("01411") + ch1;
                serialPort1.Write(cmd);
            }
        }

        char ch = (char)Convert.ToInt32("2", 16);  // 開始STX
        char ch1 = (char)Convert.ToInt32("3", 16);  // 結束ETX
        private void button7_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string cmd = ch + change("01423X0009") + ch1;
                serialPort1.Write(cmd);
                Thread.Sleep(500);
                textBox1.Text = "" + serialPort1.ReadExisting();
            }
        }

        // 正轉
        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string cmd = ch + change("01423X0010") + ch1;
                serialPort1.Write(cmd);
                Thread.Sleep(500);
                textBox1.Text = "" + serialPort1.ReadExisting();
            }
        }
        // 反轉
        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string cmd = ch + change("01423X0011") + ch1;
                serialPort1.Write(cmd);
                Thread.Sleep(500);
                textBox1.Text = "" + serialPort1.ReadExisting();
            }
        }
        // 轉幾圈 
        private void button3_Click(object sender, EventArgs e)
        {//1圈 0.5CM
            int vale = Convert.ToInt32( Convert.ToDouble(textBox3.Text)*800);     // 轉幾圈  工件高度+12-現在高度
            string six = Convert.ToString(vale, 16).PadLeft(4, '0').ToUpper();
            string cmd1 = ch + change("014701R00120" + six) + ch1;
            serialPort1.Write(cmd1);
        }
        // ON
        private void button5_Click(object sender, EventArgs e)
        {
            string cmd = ch + change("01411") + ch1;
            serialPort1.Write(cmd);
        }
        // OFF
        private void button4_Click(object sender, EventArgs e)
        {
            string cmd = ch + change("01410") + ch1;
            serialPort1.Write(cmd);
        }

        // 取得驗證碼
        private string change(string text)
        {
            string verify = null;
            int a = 2;
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(text);  // 十進制
            for (int i = 0; i < byteArray.Length; i++)
            {
                a += byteArray[i];  //以十進制相加
            }
            verify = Convert.ToString(a, 16); // 轉換成16進制
            return text + verify.ToUpper().Substring(verify.Length - 2, 2);
        }
        // STATUS
        private void button6_Click(object sender, EventArgs e)
        {
            string cmd = ch + change(textBox2.Text) + ch1;
            serialPort1.Write(cmd);
            textBox1.Text = serialPort1.ReadExisting();
        }

        
        
    
    
    }
}
