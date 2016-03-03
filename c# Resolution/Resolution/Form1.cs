using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resolution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<string> str = new List<string>();
            str.Add("B->C");
            str.Add("C->A");
            str.Add("C");
            str.Add("B");
            String prove = "A";
            Resolution r = new Resolution(str, prove);

            //Выводные данные
            foreach (Disjunct d in r.disjuncts)
                textBox1.AppendText(d.ToString() + '\n');
            textBox3.AppendText(prove);

            //Входные данные
            textBox0.AppendText(string.Join(System.Environment.NewLine, str) + '\n');
            textBox2.AppendText(r.Process().ToString());
        }
    }
}
