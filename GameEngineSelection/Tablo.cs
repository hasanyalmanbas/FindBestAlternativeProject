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
    public partial class Tablo : Form
    {
        ListBox table_Column;
        ListBox table_Row;


        public Tablo(ListBox column, ListBox row)
        {
            InitializeComponent();
            table_Column = column;
            table_Row = row;
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
    }
}
