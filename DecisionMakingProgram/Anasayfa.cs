using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngineSelection
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text != "")
                {
                    e.SuppressKeyPress = true;
                    listBox1.Items.Add(textBox1.Text);
                    textBox1.Text = "";
                }else
                {
                    MessageBox.Show("Lütfen Bir Alternatif Yazın.");
                }
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text != "")
                {
                    e.SuppressKeyPress = true;
                    listBox2.Items.Add(textBox2.Text);
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Kriter Yazın.");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count >= 1 && listBox2.Items.Count > 1)
            {
                Tablo tablo = new Tablo(listBox2, listBox1);
                tablo.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("En az 2 Alternatif ve 2 Kriter gereklidir.");
            }
        }

        
    }

   
}
