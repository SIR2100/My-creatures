namespace ProdModel
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chaining_button = new System.Windows.Forms.Button();
            this.backwardChainig_button = new System.Windows.Forms.RadioButton();
            this.forwardChaining_button = new System.Windows.Forms.RadioButton();
            this.provableFact_textBox = new System.Windows.Forms.TextBox();
            this.output_textBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fact_textBox = new System.Windows.Forms.TextBox();
            this.del_fact_button = new System.Windows.Forms.Button();
            this.add_fact_button = new System.Windows.Forms.Button();
            this.knowledge_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.consequence_textBox = new System.Windows.Forms.TextBox();
            this.conditions_textBox = new System.Windows.Forms.TextBox();
            this.del_rule_button = new System.Windows.Forms.Button();
            this.add_rule_button = new System.Windows.Forms.Button();
            this.rules_textBox = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chaining_button);
            this.groupBox3.Controls.Add(this.backwardChainig_button);
            this.groupBox3.Controls.Add(this.forwardChaining_button);
            this.groupBox3.Controls.Add(this.provableFact_textBox);
            this.groupBox3.Controls.Add(this.output_textBox);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(255, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 440);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Вывод";
            // 
            // chaining_button
            // 
            this.chaining_button.Location = new System.Drawing.Point(387, 23);
            this.chaining_button.Name = "chaining_button";
            this.chaining_button.Size = new System.Drawing.Size(88, 28);
            this.chaining_button.TabIndex = 5;
            this.chaining_button.Text = "Доказать";
            this.chaining_button.UseVisualStyleBackColor = true;
            this.chaining_button.Click += new System.EventHandler(this.chaining_button_Click);
            // 
            // backwardChainig_button
            // 
            this.backwardChainig_button.AutoSize = true;
            this.backwardChainig_button.Location = new System.Drawing.Point(275, 29);
            this.backwardChainig_button.Name = "backwardChainig_button";
            this.backwardChainig_button.Size = new System.Drawing.Size(97, 22);
            this.backwardChainig_button.TabIndex = 4;
            this.backwardChainig_button.TabStop = true;
            this.backwardChainig_button.Text = "Обратный";
            this.backwardChainig_button.UseVisualStyleBackColor = true;
            // 
            // forwardChaining_button
            // 
            this.forwardChaining_button.AutoSize = true;
            this.forwardChaining_button.Checked = true;
            this.forwardChaining_button.Location = new System.Drawing.Point(188, 29);
            this.forwardChaining_button.Name = "forwardChaining_button";
            this.forwardChaining_button.Size = new System.Drawing.Size(81, 22);
            this.forwardChaining_button.TabIndex = 3;
            this.forwardChaining_button.TabStop = true;
            this.forwardChaining_button.Text = "Прямой";
            this.forwardChaining_button.UseVisualStyleBackColor = true;
            // 
            // provableFact_textBox
            // 
            this.provableFact_textBox.Location = new System.Drawing.Point(6, 27);
            this.provableFact_textBox.Name = "provableFact_textBox";
            this.provableFact_textBox.Size = new System.Drawing.Size(176, 24);
            this.provableFact_textBox.TabIndex = 1;
            this.provableFact_textBox.Text = "Факт";
            // 
            // output_textBox
            // 
            this.output_textBox.Location = new System.Drawing.Point(6, 57);
            this.output_textBox.Multiline = true;
            this.output_textBox.Name = "output_textBox";
            this.output_textBox.ReadOnly = true;
            this.output_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output_textBox.Size = new System.Drawing.Size(469, 369);
            this.output_textBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.fact_textBox);
            this.groupBox4.Controls.Add(this.del_fact_button);
            this.groupBox4.Controls.Add(this.add_fact_button);
            this.groupBox4.Controls.Add(this.knowledge_textBox);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(12, 242);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(237, 210);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "База знаний";
            // 
            // fact_textBox
            // 
            this.fact_textBox.Location = new System.Drawing.Point(9, 180);
            this.fact_textBox.Name = "fact_textBox";
            this.fact_textBox.Size = new System.Drawing.Size(177, 24);
            this.fact_textBox.TabIndex = 4;
            this.fact_textBox.Text = "Факт";
            // 
            // del_fact_button
            // 
            this.del_fact_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.del_fact_button.Location = new System.Drawing.Point(169, 154);
            this.del_fact_button.Name = "del_fact_button";
            this.del_fact_button.Size = new System.Drawing.Size(42, 20);
            this.del_fact_button.TabIndex = 2;
            this.del_fact_button.Text = "Clear";
            this.del_fact_button.UseVisualStyleBackColor = true;
            this.del_fact_button.Click += new System.EventHandler(this.del_fact_button_Click);
            // 
            // add_fact_button
            // 
            this.add_fact_button.Location = new System.Drawing.Point(189, 180);
            this.add_fact_button.Name = "add_fact_button";
            this.add_fact_button.Size = new System.Drawing.Size(42, 24);
            this.add_fact_button.TabIndex = 1;
            this.add_fact_button.Text = "OK";
            this.add_fact_button.UseVisualStyleBackColor = true;
            this.add_fact_button.Click += new System.EventHandler(this.add_fact_button_Click);
            // 
            // knowledge_textBox
            // 
            this.knowledge_textBox.Location = new System.Drawing.Point(6, 21);
            this.knowledge_textBox.Multiline = true;
            this.knowledge_textBox.Name = "knowledge_textBox";
            this.knowledge_textBox.ReadOnly = true;
            this.knowledge_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.knowledge_textBox.Size = new System.Drawing.Size(225, 154);
            this.knowledge_textBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.consequence_textBox);
            this.groupBox1.Controls.Add(this.conditions_textBox);
            this.groupBox1.Controls.Add(this.del_rule_button);
            this.groupBox1.Controls.Add(this.add_rule_button);
            this.groupBox1.Controls.Add(this.rules_textBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 224);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "База правил";
            // 
            // consequence_textBox
            // 
            this.consequence_textBox.Location = new System.Drawing.Point(99, 185);
            this.consequence_textBox.Name = "consequence_textBox";
            this.consequence_textBox.Size = new System.Drawing.Size(87, 24);
            this.consequence_textBox.TabIndex = 9;
            this.consequence_textBox.Text = "Консеквент";
            // 
            // conditions_textBox
            // 
            this.conditions_textBox.Location = new System.Drawing.Point(6, 185);
            this.conditions_textBox.Name = "conditions_textBox";
            this.conditions_textBox.Size = new System.Drawing.Size(87, 24);
            this.conditions_textBox.TabIndex = 8;
            this.conditions_textBox.Text = "Антецедент";
            // 
            // del_rule_button
            // 
            this.del_rule_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.del_rule_button.Location = new System.Drawing.Point(169, 158);
            this.del_rule_button.Name = "del_rule_button";
            this.del_rule_button.Size = new System.Drawing.Size(42, 21);
            this.del_rule_button.TabIndex = 2;
            this.del_rule_button.Text = "Clear";
            this.del_rule_button.UseVisualStyleBackColor = true;
            this.del_rule_button.Click += new System.EventHandler(this.del_rule_button_Click);
            // 
            // add_rule_button
            // 
            this.add_rule_button.Location = new System.Drawing.Point(191, 185);
            this.add_rule_button.Name = "add_rule_button";
            this.add_rule_button.Size = new System.Drawing.Size(40, 25);
            this.add_rule_button.TabIndex = 1;
            this.add_rule_button.Text = "OK";
            this.add_rule_button.UseVisualStyleBackColor = true;
            this.add_rule_button.Click += new System.EventHandler(this.add_rule_button_Click);
            // 
            // rules_textBox
            // 
            this.rules_textBox.Location = new System.Drawing.Point(6, 23);
            this.rules_textBox.Multiline = true;
            this.rules_textBox.Name = "rules_textBox";
            this.rules_textBox.ReadOnly = true;
            this.rules_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rules_textBox.Size = new System.Drawing.Size(225, 157);
            this.rules_textBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 467);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Продукционная модель";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button chaining_button;
        private System.Windows.Forms.RadioButton backwardChainig_button;
        private System.Windows.Forms.RadioButton forwardChaining_button;
        private System.Windows.Forms.TextBox provableFact_textBox;
        private System.Windows.Forms.TextBox output_textBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox fact_textBox;
        private System.Windows.Forms.Button del_fact_button;
        private System.Windows.Forms.Button add_fact_button;
        private System.Windows.Forms.TextBox knowledge_textBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox consequence_textBox;
        private System.Windows.Forms.TextBox conditions_textBox;
        private System.Windows.Forms.Button del_rule_button;
        private System.Windows.Forms.Button add_rule_button;
        private System.Windows.Forms.TextBox rules_textBox;
    }
}

