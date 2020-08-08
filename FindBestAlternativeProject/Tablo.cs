using System;
using System.Windows.Forms;

namespace FindBestAlternativeProject
{
    public partial class Tablo : Form
    {
        ListBox table_Column;
        ListBox table_Row;

        bool isSaveTable;

        double[,] Values;


        public Tablo(ListBox column, ListBox row)
        {
            InitializeComponent();
            table_Column = column;
            table_Row = row;
            Values = new double[table_Row.Items.Count, table_Column.Items.Count];
            isSaveTable = false;


        }

        private void Tablo_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = table_Column.Items.Count;
            dataGridView1.RowCount = table_Row.Items.Count;

            for (int i = 0; i < table_Column.Items.Count; i++)
            {
                dataGridView1.Columns[i].HeaderText = table_Column.Items[i].ToString();
            }
            for (int i = 0; i < table_Row.Items.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = table_Row.Items[i].ToString();
            }
        }

        public bool IsNumeric(string text)
        {
            double test;
            Console.WriteLine(text);
            return double.TryParse(text, out test);
        }

        private void button2_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < table_Row.Items.Count; i++)
            {
                for (int j = 0; j < table_Column.Items.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        if (!IsNumeric(dataGridView1.Rows[i].Cells[j].Value.ToString()))
                        {
                            MessageBox.Show("Lütfen Tablodaki Tüm Hücrelerde Sadece Sayı Kullanın!");
                            return;
                        }

                        Values[i, j] = double.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Tablodaki Tüm Hücreleri Doldurun!");
                        return;
                    }

                }
            }
            MessageBox.Show("Tablo Kaydedildi.");
            isSaveTable = true;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (isSaveTable)
                {
                    Sonuc sonuc = new Sonuc(comboBox1.GetItemText(comboBox1.SelectedItem), Values, dataGridView1, table_Row.Items.Count, table_Column.Items.Count);
                    sonuc.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Lütfen Hücreleri Doldurup Kaydedin.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen Bir Yöntem Seçin.");
            }
        }
    }
}
