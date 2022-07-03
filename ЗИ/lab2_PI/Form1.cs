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

namespace lab1_PI
{
    public partial class Form1 : Form
    {
        Char[] alf = { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ъ', 'э', 'ю', 'я', ' ', ','};

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String key = this.textBox3.Text;
            String text = this.textBox1.Text;

            this.textBox2.Text = "";

            for (int i = 0; i < text.Length; ++i)
            {
                int keyi = Array.IndexOf(alf, key[i % key.Length]);
                int texti = Array.IndexOf(alf, text[i]);
                this.textBox2.Text += alf[(keyi + texti)% alf.Length];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String key = this.textBox3.Text;
            String text = this.textBox1.Text;

            this.textBox2.Text = "";

            for (int i = 0; i < text.Length; ++i)
            {
                int keyi = Array.IndexOf(alf, key[i % key.Length]);
                int texti = Array.IndexOf(alf, text[i]);
                int idex = texti - keyi;

                this.textBox2.Text += alf[(idex < 0) ? (idex + alf.Length) % alf.Length : idex % alf.Length];
            }
        }

        private string get_name_file()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            var fileContent = string.Empty;

            openFile.InitialDirectory = "./";
            openFile.Filter = "All files (*.*)|*.*";

            if (openFile.ShowDialog() == DialogResult.OK)
            { 
                return openFile.FileName;
            }
            return null;
        }

        private byte[] get_string_file(string name)
        {
            using (FileStream reader = new FileStream(name, FileMode.Open, FileAccess.Read))
            {
                int leng = (int)reader.Length;
                byte[] bufer = new byte[reader.Length];

                reader.Read(bufer, 0, leng);
                reader.Close();
                
                return bufer;
            }
        }

        private void writeTofile(byte[] str, string name_file)
        {
            using (FileStream writer = new FileStream(name_file, FileMode.Open, FileAccess.Write))
            {
                writer.Write(str, 0, str.Length);
                writer.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] text;
            byte[] key = Encoding.ASCII.GetBytes(this.textBox4.Text);
            string name = get_name_file();

            if (name == null)
                return;

            text = get_string_file(name);

            for (int i = 0; i < text.Length; ++i)
            {
                byte keyi = key[i % key.Length];
                byte texti = text[i];
                byte idex = Convert.ToByte((texti + keyi) % 256);

                text[i] = idex;
            }

            writeTofile(text, name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] text;
            byte[] key = Encoding.ASCII.GetBytes(this.textBox4.Text);
            string name = get_name_file();

            if (name == null)
                return;

            text = get_string_file(name);

            for (int i = 0; i < text.Length; ++i)
            {
                byte keyi = key[i % key.Length];
                byte texti = text[i];
                int idex = texti - keyi;

                text[i] = Convert.ToByte((idex < 0)? idex + 256: idex % 256);
            }

            writeTofile(text, name);
        }
    }
}
