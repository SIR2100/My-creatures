using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10_server
{
    public partial class Form1 : Form
    {
        private Thread Potok2 = null;
        private Dictionary<int, Thread> Thrs = new Dictionary<int, Thread>();
        private Dictionary<int, Queue<string>> Tasks = new Dictionary<int, Queue<string>>();
        TcpListener server = null;
        int k = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void StartServer()
        {
            //создаем функцию-делегат для безопасного добавления текста в textBox1 из данного параллельного потока
            TypeAddTextDelegate AddTextDelegate = new TypeAddTextDelegate(AddText);
            try
            {
                Int32 port = 1234; //порт сервера
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");//ip-адрес сервера
                Byte[] bytes = new Byte[1024];

                server = new TcpListener(localAddr, port);

                server.Start();
                while (true)
                {
                    if (server.Pending())//проверяем, есть ли в очереди клиенты
                    {                        
                        // Количество возможных потоков
                        if (Thrs.Count < 50)
                        {
                            //Invoke(AddTextDelegate, "Client connected!\r\n");                         
                            Thrs[k] = new Thread(new ParameterizedThreadStart(Sending));
                            Thrs[k].IsBackground = false;
                            Thrs[k].Start(k);
                            Tasks.Add(k, new Queue<string>());
                            k++;
                        }

                    }
                    else
                        Thread.Sleep(100);
                }
            }
            catch (SocketException expt)
            {
                MessageBox.Show(expt.ToString(), "Error", MessageBoxButtons.OK);
            }
            catch (ThreadAbortException)
            {
                Invoke(AddTextDelegate, "Server stoped\r\n");
            }
            finally
            {
                server.Stop();
                Thread.ResetAbort();
            }
        }

        void Sending(object server1)
        {

            int index = (int)server1;
            // Ждем соединения клиента
            try
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                Byte[] bytes = new Byte[1000];
                while (true)
                {
                    if (Tasks[index].Count > 0)
                    {

                        //преобразуем строчку-ответ сервера в массив байт
                        byte[] answer_bytes = System.Text.Encoding.UTF8.GetBytes(Tasks[index].Dequeue());
                        try
                        {
                            stream.Write(answer_bytes, 0, answer_bytes.Length);
                        }
                        catch { }
                    }
                }
            }
            catch { }
            
        }

        private delegate void TypeAddTextDelegate(string text);

        private void AddText(string text)
        {
            textBox1.Text += text;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            foreach (var item in Tasks)
            {
                item.Value.Enqueue(e.KeyChar.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Potok2 == null) || (Potok2.ThreadState == System.Threading.ThreadState.Stopped))
            {
                Potok2 = new Thread(StartServer);
                Potok2.IsBackground = true;
                Potok2.Start();
                button1.Enabled = false;
                button2.Enabled = false;
                Thread.Sleep(1000);
                button1.Enabled = true;
                button2.Enabled = true;
                textBox1.Text += "Server started\r\n";
            }
            else
            {
                textBox1.Text += "Server is already working\r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Potok2 != null)
            {
                if (Potok2.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    textBox1.Text += "Server is already stoped\r\n";
                }
                else
                { 
                Potok2.Abort();
                button1.Enabled = false;
                button2.Enabled = false;
                Thread.Sleep(1000);
                button1.Enabled = true;
                button2.Enabled = true;
                }
            }

            else if (Potok2 == null)
            {
                textBox1.Text += "Server is not started\r\n";
            }

        }
    }
}
