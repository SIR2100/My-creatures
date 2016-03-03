using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProdModel
{
    public partial class Form1 : Form
    {
        private Logic productionSystem = new Logic();

        public Form1()
        {
            InitializeComponent();
        }

        private Production parseProduction(string cond, string cons) // получаем продукцию из строк
        {
            Production p = new Production();
            p.setConsequence(cons.ToUpper());
            string[] conditions = cond.ToUpper().Split(',');
            for (int i = 0; i < conditions.Length; i++)
                p.addCondition(conditions[i]);
            return p;
        }

        //---------------------------------------------------------ОБРАБОТКА СОБЫТИЙ--------------------------------------------------------//

        private void add_rule_button_Click(object sender, EventArgs e) // добавление продукции 
        {
            if (conditions_textBox.Text == "" || consequence_textBox.Text == "")
            {
                MessageBox.Show("Введите условия и следствие.", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            Production p = parseProduction(conditions_textBox.Text, consequence_textBox.Text);
            productionSystem.addRule(p);
            rules_textBox.AppendText(p.ToString() + '\n');
            conditions_textBox.Clear();
            consequence_textBox.Clear();
        }

        private void del_rule_button_Click(object sender, EventArgs e) // удаление продукций
        {
            productionSystem.delRules();
            rules_textBox.Clear();
        }


        private void add_fact_button_Click(object sender, EventArgs e) // добавление факта
        {
            if (fact_textBox.Text == "")
            {
                MessageBox.Show("Введите факт.", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            productionSystem.addFact(fact_textBox.Text.ToUpper());
            knowledge_textBox.AppendText(fact_textBox.Text.ToUpper() + '\n');
            fact_textBox.Clear();
        }

        private void del_fact_button_Click(object sender, EventArgs e) // удаление фактов
        {
            productionSystem.delFacts();
            knowledge_textBox.Clear();
        }

        private void chaining_button_Click(object sender, EventArgs e) // доказательство
        {
            output_textBox.Clear();

            if (provableFact_textBox.Text == "")
            {
                MessageBox.Show("Введите факт.", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            string result = "";
            string tab = "| ";
            if (forwardChaining_button.Checked)
                result = productionSystem.forwardChaining(provableFact_textBox.Text.ToUpper());
            else
                productionSystem.backwardChaining(provableFact_textBox.Text.ToUpper(), ref result, ref tab);

            string[] lines = result.Split('\n');
            output_textBox.Lines = lines;
            knowledge_textBox.Clear();
            knowledge_textBox.Lines = productionSystem.factsToLines();
        }

    }
}
