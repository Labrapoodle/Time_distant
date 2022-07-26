using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Time
{
    public partial class Form2 : Form
    {
        List<Configuration> Table;

        public Form2()
        {
            InitializeComponent();


            Table = DB.GetListOfConfigurations();
            var bindingList = new BindingList<Configuration>(Table);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].HeaderText = "Горло";
            
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "Кол-во гнёзд";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].HeaderText = "Вес";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].HeaderText = "Время цикла";
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            //this.FormClosed += new FormClosingEventHandler(this.Form2_Close());
            //FormClosed += Form2_FormClosed;
            
        }

        /*private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.KK();
            
        }*/

        void Form2_Shown()
        {
            dataGridView1.Width = dataGridView1.Columns.Cast<DataGridViewColumn>().Sum(x => x.Width) + (dataGridView1.RowHeadersVisible ? dataGridView1.RowHeadersWidth : 0) + 3;


        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string connectionString = "Server=192.168.0.12;Database=PetPro;Password=DbSyS@dm1n;User ID=sa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command17 = new SqlCommand("Update_Configuration_Nominal"))
                {
                    var t = Table[e.RowIndex];// DB.Table[e.RowIndex];
                    command17.Connection = connection;
                    command17.CommandType = System.Data.CommandType.StoredProcedure;
                    command17.Parameters.Add("@NECK", System.Data.SqlDbType.NVarChar).Value = t.NCK;
                    command17.Parameters.Add("@WEIGHT", System.Data.SqlDbType.Float).Value = t.WGHT;
                    command17.Parameters.Add("@MATRIX", System.Data.SqlDbType.Int).Value = t.MTX;
                    command17.Parameters.Add("@NOMINAL", System.Data.SqlDbType.Float).Value = t.Nominal_Cycle_Period;
                    command17.ExecuteNonQuery();
                }
            }
        }
    }
}
