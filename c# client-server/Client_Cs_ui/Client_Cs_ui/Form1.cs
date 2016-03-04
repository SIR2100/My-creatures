using System;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Client_Cs_ui
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button3.Enabled = false;
            button4.Enabled = false;
        }
        
        string[] split;
        int number = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 port = 12345;//порт сервера
                string IP = textBox1.Text;

                string send_message = textBox1.Text, recv_message;//пошлем серверу
                // буффер для приема сообщений
                Byte[] recv_data = new Byte[10000];

                //проверяем ввел ли пользователь хоть что-нибудь
                if (send_message.Length == 0)
                {
                    MessageBox.Show("Строка не должна быть пустой", "Error", MessageBoxButtons.OK);
                    return;
                }

                //подключаемся к серверу
                TcpClient client = new TcpClient(IP, port);
                // вводим поток stream для чтения и записи через установленное соединение                
                NetworkStream stream = client.GetStream();

                //преобразуем строчку в массив байт
                Byte[] send_data = System.Text.Encoding.UTF8.GetBytes(send_message);
                // посылаем сообщение серверу 
                stream.Write(send_data, 0, send_data.Length);

                // получаем сообщение от сервера, i - кол-во реально полученных байт
                int i = stream.Read(recv_data, 0, recv_data.Length);
                recv_message = System.Text.Encoding.UTF8.GetString(recv_data, 0, i);
                textBox2.Text = recv_message;

                split = recv_message.Split(new Char[] { '.', '\r', '\n' });
                button3.Enabled = true;
                
             /*
                //Выбираем задание
                string num = textBox3.Text;
                int len = split.Length;
                for (int l = 1; l <= len; l++)
                {
                    if (num == split[l])
                    {
                        textBox4.Text = split[l] + split[l + 1];
                        split[l + 1] = "";
                    }
                }
            */

                // закрываем соединение
                stream.Close();
                client.Close();
            }
            catch (SocketException expt)
            {
                MessageBox.Show(expt.ToString(),"Error",MessageBoxButtons.OK);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            bool fl = true;
                string num = textBox3.Text;
                int len = split.Length;
                for (int l = 0; l <= len - 1; l++)
                {
                    if (num == split[l])
                    {
                        textBox4.Text = split[l] + "." + split[l + 1];
                        fl = false;
                        number = l + 1;
                    }                        
                }     
                if (fl)
                {
                    textBox4.Text = "Задания с таким номером нет!";
                }
                button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Int32 port = 12345;//порт сервера
            string IP = textBox1.Text;

            string send = "";
            int len = split.Length;
            int q = 0; // переменная проверки на элемента
            
            for (int l = 0; l <= len - 1; l++)
            {
                if (q == 2) // Значит пустой элемент массива -> переходим к следующему
                {
                    
                }
                else
                {                    
                    if (q == 0) // Значит элемент массива - номер -> ставим после него точку
                    {
                        send += split[l] + '.'; // добавление точки после номера задания
                    }
                    else
                    {
                        if (q == 1) // Значит элемент массива - задание -> ставим после него символ переноса строки
                        {
                            if (l == number)
                            {
                                send += split[l] + " -> " + textBox5.Text + "\r\n"; // добавление фамилии к выбранному заданию
                            }
                            else
                            {
                                send += split[l] + "\r\n"; // добавление символа переноса строки к заданию
                            }
                        }
                    }
                }
                q++;
                if (q == 3)
                {
                    q = 0;
                }
            }

            string send_message = send, recv_message;//пошлем серверу              //СЮДА Переменную String с отправкой
            // буффер для приема сообщений
            Byte[] recv_data = new Byte[1000];

            //подключаемся к серверу
            TcpClient client = new TcpClient(IP, port);
            // вводим поток stream для чтения и записи через установленное соединение                
            NetworkStream stream = client.GetStream();

            //преобразуем строчку в массив байт
            Byte[] send_data = System.Text.Encoding.UTF8.GetBytes(send_message);
            // посылаем сообщение серверу 
            stream.Write(send_data, 0, send_data.Length);

            // получаем сообщение от сервера, i - кол-во реально полученных байт
            int i = stream.Read(recv_data, 0, recv_data.Length);
            recv_message = System.Text.Encoding.UTF8.GetString(recv_data, 0, i);
            textBox2.Text = recv_message;

            split = recv_message.Split(new Char[] { '.', '\r', '\n' });
            button3.Enabled = false;
            button4.Enabled = false;
        }
    }
}