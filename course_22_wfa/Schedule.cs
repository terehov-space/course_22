using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace course_22_wfa
{
    public partial class Schedule : Form
    {
        private string connectionString = "Server=DESKTOP-BQBPN94;Database=course_22;Trusted_Connection=True;";
        private int teacher = 0;
        private int type = 0;
        private int group = 0;
        private string lesson = "";
        private string datetime = "";
        
        public Schedule()
        {
            InitializeComponent();
            
            connect();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var data = comboBox1.SelectedItem;
            lesson = data.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedGroup = comboBox2.SelectedItem as DataRowView;
            var data = selectedGroup.Row["id"];

            if (selectedGroup != null)
            {
                type = int.Parse(data.ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedGroup = comboBox3.SelectedItem as DataRowView;
            var data = selectedGroup.Row["id"];

            if (selectedGroup != null)
            {
                group = int.Parse(data.ToString());
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedGroup = comboBox4.SelectedItem as DataRowView;
            var data = selectedGroup.Row["id"];

            if (selectedGroup != null)
            {
                teacher = int.Parse(data.ToString());
            }
        }

        private void connect()
        {

            string selectTypes = "SELECT id, title FROM schedule_types";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter1 = new SqlDataAdapter(selectTypes, connection);
                DataSet ds1 = new DataSet();
                adapter1.Fill(ds1);
                comboBox2.DataSource = ds1.Tables[0];
                comboBox2.DisplayMember = "title";
                comboBox2.ValueMember = "id";

            }

            string selectGroups = "SELECT id, title FROM groups";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter1 = new SqlDataAdapter(selectGroups, connection);
                DataSet ds1 = new DataSet();
                adapter1.Fill(ds1);
                comboBox3.DataSource = ds1.Tables[0];
                comboBox3.DisplayMember = "title";
                comboBox3.ValueMember = "id";

            }

            string selectTeachers =
                "SELECT id, CONCAT(last_name, ' ', first_name, ' ', second_name) AS title FROM teachers";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter2 = new SqlDataAdapter(selectTeachers, connection);
                DataSet ds2 = new DataSet();
                adapter2.Fill(ds2);
                comboBox4.DataSource = ds2.Tables[0];
                comboBox4.DisplayMember = "title";
                comboBox4.ValueMember = "id";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (group != 0 && teacher != 0 && type != 0 && lesson != "" && dateTimePicker1.Value.ToString() != "")
            {
                string sql = "INSERT INTO schedule (group_id, type_id, teacher_id, lesson_date, lesson_subject) VALUES(" + teacher + ", " + type + ", " + group + ", '" + dateTimePicker1.Value.ToString() + "', '" + lesson + "')";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sq = new SqlCommand(sql, connection);
                    sq.ExecuteNonQuery();

                    this.Close();
                }
            }
        }
    }
}