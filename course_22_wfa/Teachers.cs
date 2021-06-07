using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace course_22_wfa
{
    public partial class Teachers : Form
    {
        private string connectionString = "Server=DESKTOP-BQBPN94;Database=course_22;Trusted_Connection=True;";
        
        public Teachers()
        {
            InitializeComponent();
            
            connect();
        }
        
        private void connect(bool skipFilter = false)
        {
            string sql = "SELECT * FROM teachers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGrid1.DataSource = ds.Tables[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "" && textBox2.Text.ToString() != "" && textBox3.Text.ToString() != "" && textBox4.Text.ToString() != "")
            {
                string sql = "INSERT INTO teachers (first_name, last_name, second_name, email) VALUES('" + textBox1.Text.ToString() + "', '" + textBox2.Text.ToString() + "', '" + textBox3.Text.ToString() + "', '" + textBox4.Text.ToString() + "')";
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
            if (textBox1.Text.ToString() != "" && textBox2.Text.ToString() != "" && textBox3.Text.ToString() != "" && textBox4.Text.ToString() != "")
            {
                string sql = "DELETE FROM teachers WHERE first_name='" + textBox1.Text.ToString() + "' AND last_name='" + textBox2.Text.ToString() + "' AND second_name='" + textBox3.Text.ToString() + "' AND email='" + textBox4.Text.ToString() + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sq = new SqlCommand(sql, connection);
                    sq.ExecuteNonQuery();

                    connect();
                }
            }
        }
    }
}