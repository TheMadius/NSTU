namespace lab_2_terVer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.TB02 = new System.Windows.Forms.TextBox();
            this.TB01 = new System.Windows.Forms.TextBox();
            this.TB00 = new System.Windows.Forms.TextBox();
            this.TB12 = new System.Windows.Forms.TextBox();
            this.TB11 = new System.Windows.Forms.TextBox();
            this.TB10 = new System.Windows.Forms.TextBox();
            this.TB22 = new System.Windows.Forms.TextBox();
            this.TB21 = new System.Windows.Forms.TextBox();
            this.TB20 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(43, 33);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 4;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series1.Legend = "Legend1";
            series1.Name = "S";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(558, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "СП";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(659, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(31, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "0,8";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(696, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(31, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "0,2";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(733, 49);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(31, 20);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "0";
            // 
            // TB02
            // 
            this.TB02.Location = new System.Drawing.Point(733, 131);
            this.TB02.Name = "TB02";
            this.TB02.Size = new System.Drawing.Size(31, 20);
            this.TB02.TabIndex = 6;
            this.TB02.Text = "0,10";
            // 
            // TB01
            // 
            this.TB01.Location = new System.Drawing.Point(696, 131);
            this.TB01.Name = "TB01";
            this.TB01.Size = new System.Drawing.Size(31, 20);
            this.TB01.TabIndex = 5;
            this.TB01.Text = "0,40";
            // 
            // TB00
            // 
            this.TB00.Location = new System.Drawing.Point(659, 131);
            this.TB00.Name = "TB00";
            this.TB00.Size = new System.Drawing.Size(31, 20);
            this.TB00.TabIndex = 4;
            this.TB00.Text = "0,50";
            // 
            // TB12
            // 
            this.TB12.Location = new System.Drawing.Point(733, 157);
            this.TB12.Name = "TB12";
            this.TB12.Size = new System.Drawing.Size(31, 20);
            this.TB12.TabIndex = 9;
            this.TB12.Text = "0,45";
            // 
            // TB11
            // 
            this.TB11.Location = new System.Drawing.Point(696, 157);
            this.TB11.Name = "TB11";
            this.TB11.Size = new System.Drawing.Size(31, 20);
            this.TB11.TabIndex = 8;
            this.TB11.Text = "0,55";
            // 
            // TB10
            // 
            this.TB10.Location = new System.Drawing.Point(659, 157);
            this.TB10.Name = "TB10";
            this.TB10.ReadOnly = true;
            this.TB10.Size = new System.Drawing.Size(31, 20);
            this.TB10.TabIndex = 7;
            this.TB10.Text = "0";
            // 
            // TB22
            // 
            this.TB22.Location = new System.Drawing.Point(733, 183);
            this.TB22.Name = "TB22";
            this.TB22.Size = new System.Drawing.Size(31, 20);
            this.TB22.TabIndex = 12;
            this.TB22.Text = "1";
            // 
            // TB21
            // 
            this.TB21.Location = new System.Drawing.Point(696, 183);
            this.TB21.Name = "TB21";
            this.TB21.ReadOnly = true;
            this.TB21.Size = new System.Drawing.Size(31, 20);
            this.TB21.TabIndex = 11;
            this.TB21.Text = "0";
            // 
            // TB20
            // 
            this.TB20.Location = new System.Drawing.Point(659, 183);
            this.TB20.Name = "TB20";
            this.TB20.ReadOnly = true;
            this.TB20.Size = new System.Drawing.Size(31, 20);
            this.TB20.TabIndex = 10;
            this.TB20.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(670, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "S0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(704, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "S1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(741, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "S2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(634, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "P0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(741, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "S2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(704, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "S1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(667, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "S0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(634, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "S2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(633, 160);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "S1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(634, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "S0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(659, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(646, 248);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 24;
            this.textBox4.Text = "8";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(646, 229);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Кол-во Эксп";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(659, 309);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "StartN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 227);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "S0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "S1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "S2";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(43, 339);
            this.chart2.Name = "chart2";
            series2.BorderWidth = 4;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Ave";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(558, 257);
            this.chart2.TabIndex = 30;
            this.chart2.Text = "СП";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 608);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB22);
            this.Controls.Add(this.TB21);
            this.Controls.Add(this.TB20);
            this.Controls.Add(this.TB12);
            this.Controls.Add(this.TB11);
            this.Controls.Add(this.TB10);
            this.Controls.Add(this.TB02);
            this.Controls.Add(this.TB01);
            this.Controls.Add(this.TB00);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "СП";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox TB02;
        private System.Windows.Forms.TextBox TB01;
        private System.Windows.Forms.TextBox TB00;
        private System.Windows.Forms.TextBox TB12;
        private System.Windows.Forms.TextBox TB11;
        private System.Windows.Forms.TextBox TB10;
        private System.Windows.Forms.TextBox TB22;
        private System.Windows.Forms.TextBox TB21;
        private System.Windows.Forms.TextBox TB20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}

