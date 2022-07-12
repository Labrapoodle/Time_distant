using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            var bindingList = new BindingList<Configuration>(DB.Table);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].HeaderText = "Вес";
            dataGridView1.Columns[1].HeaderText = "Горло";
            dataGridView1.Columns[2].HeaderText = "Кол-во гнезд";
            dataGridView1.Columns[3].HeaderText = "Время цикла";
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;                
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            
        }
        
        void Form2_Shown()
        {
            dataGridView1.Width = dataGridView1.Columns.Cast<DataGridViewColumn>().Sum(x => x.Width) + (dataGridView1.RowHeadersVisible ? dataGridView1.RowHeadersWidth : 0) + 3;


        }
        
    }
}
