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

namespace course_22_wfa
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=DESKTOP-BQBPN94;Database=course_22;Trusted_Connection=True;";
        private int teacher = 0;
        private int group = 0;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Teachers grUI = new Teachers();
            grUI.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Groups grUI = new Groups();
            grUI.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Schedule grUI = new Schedule();
            grUI.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedGroup = comboBox1.SelectedItem as DataRowView;
            var data = selectedGroup.Row["id"];

            if (selectedGroup != null)
            {
                group = int.Parse(data.ToString());
                connect(true);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedTeacher = comboBox2.SelectedItem as DataRowView;
            var data = selectedTeacher.Row["id"];

            if (selectedTeacher != null)
            {
                teacher = int.Parse(data.ToString());
                connect(true);
            }
        }

        private void connect(bool skipFilter = false)
        {
            string sql = "SELECT s.id, s.lesson_date, s.lesson_subject, t.last_name, t.first_name, t.second_name, g.title, st.title FROM Schedule AS s INNER JOIN teachers AS t ON t.id=s.teacher_id INNER JOIN groups AS g ON g.id=s.group_id INNER JOIN schedule_types AS st ON st.id=s.type_id";

            if (group != 0)
            {
                sql += " WHERE s.group_id='" + group + "'";
            }
            
            if (teacher != 0)
            {
                if (group != 0)
                {
                    sql += " AND s.teacher_id='" + teacher + "'";
                }
                else
                {
                    sql += " WHERE s.teacher_id='" + teacher + "'";
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGrid1.DataSource = ds.Tables[0];
            }

            if (!skipFilter)
            {

                string selectGroups = "SELECT id, title FROM groups";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter1 = new SqlDataAdapter(selectGroups, connection);
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);
                    comboBox1.DataSource = ds1.Tables[0];
                    comboBox1.DisplayMember = "title";
                    comboBox1.ValueMember = "id";

                }

                string selectTeachers = "SELECT id, CONCAT(last_name, ' ', first_name, ' ', second_name) AS title FROM teachers";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter2 = new SqlDataAdapter(selectTeachers, connection);
                    DataSet ds2 = new DataSet();
                    adapter2.Fill(ds2);
                    comboBox2.DataSource = ds2.Tables[0];
                    comboBox2.DisplayMember = "title";
                    comboBox2.ValueMember = "id";

                }
            }
        }
    }
}