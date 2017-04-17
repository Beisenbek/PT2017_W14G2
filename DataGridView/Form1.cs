using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection mySqlConnection;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlCommandBuilder mySqlCommandBuilder;
        DataTable dataTable;
        BindingSource bindingSource;

        private void button1_Click(object sender, EventArgs e)
        {
            mySqlConnection = new MySqlConnection(
                            "SERVER=localhost;" +
                            "DATABASE=kbtudb;" +
                            "UID=root;" +
                            "PASSWORD=;");


            mySqlConnection.Open();

            string query = "SELECT * FROM student order by gpa desc";

            mySqlDataAdapter = new MySqlDataAdapter(query, mySqlConnection);
            mySqlCommandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);

            mySqlDataAdapter.UpdateCommand = mySqlCommandBuilder.GetUpdateCommand();
            mySqlDataAdapter.DeleteCommand = mySqlCommandBuilder.GetDeleteCommand();
            mySqlDataAdapter.InsertCommand = mySqlCommandBuilder.GetInsertCommand();

            dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);

            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int res = mySqlDataAdapter.Update(dataTable);
            toolStripStatusLabel1.Text = res.ToString();
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell c = dataGridView1[e.ColumnIndex, e.RowIndex];
            string str = c.FormattedValue as string;
            
            if (str.Length > 1)
            {
                MessageBox.Show("bad value");
            }
        }
    }
}
