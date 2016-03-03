namespace NeuralNetwork
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
            this.open_button = new System.Windows.Forms.Button();
            this.teaching_button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.recognize_button = new System.Windows.Forms.Button();
            this.circle = new System.Windows.Forms.RadioButton();
            this.rectangle = new System.Windows.Forms.RadioButton();
            this.triangle = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // open_button
            // 
            this.open_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.open_button.Location = new System.Drawing.Point(6, 15);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(77, 31);
            this.open_button.TabIndex = 0;
            this.open_button.Text = "Открыть";
            this.open_button.UseVisualStyleBackColor = true;
            this.open_button.Click += new System.EventHandler(this.open_button_Click);
            // 
            // teaching_button
            // 
            this.teaching_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.teaching_button.Location = new System.Drawing.Point(94, 14);
            this.teaching_button.Name = "teaching_button";
            this.teaching_button.Size = new System.Drawing.Size(100, 31);
            this.teaching_button.TabIndex = 1;
            this.teaching_button.Text = "Обучить";
            this.teaching_button.UseVisualStyleBackColor = true;
            this.teaching_button.Click += new System.EventHandler(this.teaching_button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(215, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(203, 201);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // recognize_button
            // 
            this.recognize_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recognize_button.Location = new System.Drawing.Point(89, 15);
            this.recognize_button.Name = "recognize_button";
            this.recognize_button.Size = new System.Drawing.Size(105, 31);
            this.recognize_button.TabIndex = 4;
            this.recognize_button.Text = "Распознать";
            this.recognize_button.UseVisualStyleBackColor = true;
            this.recognize_button.Click += new System.EventHandler(this.recognize_button_Click);
            // 
            // circle
            // 
            this.circle.AutoSize = true;
            this.circle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.circle.Location = new System.Drawing.Point(6, 19);
            this.circle.Name = "circle";
            this.circle.Size = new System.Drawing.Size(56, 20);
            this.circle.TabIndex = 8;
            this.circle.TabStop = true;
            this.circle.Text = "Круг";
            this.circle.UseVisualStyleBackColor = true;
            // 
            // rectangle
            // 
            this.rectangle.AutoSize = true;
            this.rectangle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rectangle.Location = new System.Drawing.Point(6, 71);
            this.rectangle.Name = "rectangle";
            this.rectangle.Size = new System.Drawing.Size(128, 20);
            this.rectangle.TabIndex = 9;
            this.rectangle.TabStop = true;
            this.rectangle.Text = "Прямоугольник";
            this.rectangle.UseVisualStyleBackColor = true;
            // 
            // triangle
            // 
            this.triangle.AutoSize = true;
            this.triangle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.triangle.Location = new System.Drawing.Point(6, 45);
            this.triangle.Name = "triangle";
            this.triangle.Size = new System.Drawing.Size(111, 20);
            this.triangle.TabIndex = 10;
            this.triangle.TabStop = true;
            this.triangle.Text = "Треугольник";
            this.triangle.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(34, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Откройте файл";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.open_button);
            this.groupBox1.Controls.Add(this.recognize_button);
            this.groupBox1.Location = new System.Drawing.Point(9, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 53);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Распознование";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.circle);
            this.groupBox2.Controls.Add(this.rectangle);
            this.groupBox2.Controls.Add(this.teaching_button);
            this.groupBox2.Controls.Add(this.triangle);
            this.groupBox2.Location = new System.Drawing.Point(9, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Обучение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Log:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(423, 219);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Распознавание образов";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.Button teaching_button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button recognize_button;
        private System.Windows.Forms.RadioButton circle;
        private System.Windows.Forms.RadioButton rectangle;
        private System.Windows.Forms.RadioButton triangle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
    }
}

