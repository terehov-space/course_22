using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace course_22_wfa
{
    public partial class Groups : Form
    {
        private string connectionString = "Server=DESKTOP-BQBPN94;Database=course_22;Trusted_Connection=True;";
        
        public Groups()
        {
            InitializeComponent();
            
            connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "")
            {
                string sql = "INSERT INTO groups (title) VALUES('" + textBox1.Text.ToString() + "')";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sq = new SqlCommand(sql, connection);
                    sq.ExecuteNonQuery();

                    connect();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "")
            {
                string sql = "DELETE FROM groups WHERE title LIKE '%" + textBox1.Text.ToString() + "%'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sq = new SqlCommand(sql, connection);
                    sq.ExecuteNonQuery();

                    connect();
                }
            }
        }

        private void connect(bool skipFilter = false)
        {
            string sql = "SELECT * FROM groups";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGrid1.DataSource = ds.Tables[0];
            }
        }
    }
}