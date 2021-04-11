using System;
using System.Windows.Forms;

namespace lab6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.a = new System.Windows.Forms.TextBox();
            this.b = new System.Windows.Forms.TextBox();
            this.AddTriangle = new System.Windows.Forms.Button();
            this.c = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // a
            // 
            this.a.Location = new System.Drawing.Point(86, 42);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(183, 20);
            this.a.TabIndex = 0;
            // 
            // b
            // 
            this.b.Location = new System.Drawing.Point(86, 68);
            this.b.Name = "b";
            this.b.Size = new System.Drawing.Size(183, 20);
            this.b.TabIndex = 1;
            // 
            // AddTriangle
            // 
            this.AddTriangle.Location = new System.Drawing.Point(86, 129);
            this.AddTriangle.Name = "AddTriangle";
            this.AddTriangle.Size = new System.Drawing.Size(75, 23);
            this.AddTriangle.TabIndex = 3;
            this.AddTriangle.Text = "Результат";
            this.AddTriangle.UseVisualStyleBackColor = true;
            this.AddTriangle.Click += new System.EventHandler(this.AddTriangle_Click);
            // 
            // c
            // 
            this.c.Location = new System.Drawing.Point(86, 94);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(183, 20);
            this.c.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Сторона А";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Сторона B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Сторона C";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(329, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Введите значения сторон, чтобы узнать какой это треугольник";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 174);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.c);
            this.Controls.Add(this.AddTriangle);
            this.Controls.Add(this.b);
            this.Controls.Add(this.a);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox a;
        private System.Windows.Forms.TextBox b;
        private Button AddTriangle;
        private TextBox c;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}

