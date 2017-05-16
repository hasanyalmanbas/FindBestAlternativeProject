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
 
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count >= 2 && listBox2.Items.Count > 2)
            {
                this.Hide();
                Tablo tablo = new Tablo(listBox1, listBox2);
                tablo.ShowDialog();

            }
        }
    }

   
}
