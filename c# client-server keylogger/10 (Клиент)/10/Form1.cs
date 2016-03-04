using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace _10
{
    public partial class Client : Form
    {
        string FormText = "Text:";
        private Thread Potok = null;
        public Client()
        {
            InitializeComponent();
        }

        private delegate void TypeAddTextDelegate(string text);


        public void GetServer()
        {

            Int32 port = 1234;//порт сервера
            Byte[] recv_data = new Byte[1000];
            string IP = textBox2.Text;
            try
            {
                TcpClient client = new TcpClient(IP, port);
                NetworkStream stream = client.GetStream();
                while (true)
                {

                    int i = stream.Read(recv_data, 0, recv_data.Length);
                    string recv_message = System.Text.Encoding.UTF8.GetString(recv_data, 0, i);
                    TypeAddTextDelegate AddTextDelegate = new TypeAddTextDelegate(AddText);
                    Invoke(AddTextDelegate, recv_message);
                    Thread.Sleep(100);
                }
            }
            catch { }
            
        }

        private void AddText(string text)
        {
            if (text.Length >= 2)
               FormText += text.Remove(0, text.Length - 1);
            else 
            FormText += text;
            textBox1.Text = FormText;
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Potok = new Thread(GetServer);
            Potok.IsBackground = true;
            Potok.Start();
            textBox1.Text = "Waiting for server...";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Potok != null)
            {
                Potok.Abort();
                textBox1.Text = "Disconnected";
            }
            else
            {
                textBox1.Text = "Client is not started\r\n";
            }
            
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") 
            {
                button3.Enabled = false;
            }
            else
            {
                textBox1.Text = "Text:";
                FormText = "Text:";
            }
            
        }

    }
}
