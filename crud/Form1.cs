using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace crud
{
    public partial class Form1 : Form
    {
        string conn = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);

            string querymatch = "select * from ab where id = @id";

            SqlCommand cmd2 = new SqlCommand(querymatch, con);
            cmd2.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
            con.Open();
            SqlDataReader reader = cmd2.ExecuteReader();
            if (reader.HasRows == true)
            {
                MessageBox.Show(id.Text + " this id alraedy exist");


            }
            else
            {

                con.Close();

               
                string query = "insert into ab values (@id, @name, @email, @password)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@password", pass.Text);


                con.Open();

                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Inserted Successfully");
                    gridview();
                    reset();
                }
                else
                {
                    MessageBox.Show("Invalid Error");
                }

                con.Close();
            }



            
        }

       public void gridview()
        {
            SqlConnection con = new SqlConnection(conn);
            string queryshow = "select * from ab";
            SqlDataAdapter adp = new SqlDataAdapter(queryshow,con);
            DataTable data = new DataTable();
            adp.Fill(data);
            dataGridView1.DataSource = data;

        }

        public void reset()
        {
            id.Clear();
            name.Clear();
            email.Clear();
            pass.Clear();
        }

        private void view_Click(object sender, EventArgs e)
        {
            gridview();
        }

        private void update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            string queryupdate = "update ab set id = @id, name = @name, email = @email, password = @password where id = @id";
            SqlCommand cmd = new SqlCommand(queryupdate, con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
            cmd.Parameters.AddWithValue("@name", name.Text);
            cmd.Parameters.AddWithValue("@email", email.Text);
            cmd.Parameters.AddWithValue("@password", pass.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();

            if (a > 0)
            {
                MessageBox.Show("Updated Successfully");
                gridview();
                reset();
            }
            else
            {
                MessageBox.Show("Invalid Error");
            }

            con.Close();


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            email.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            pass.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void id_TextChanged(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            string querydelete = "delete from ab where @id = id";
            SqlCommand cmd = new SqlCommand(querydelete, con);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));

            con.Open();

            int a = cmd.ExecuteNonQuery();

            if (a > 0)
            {
                MessageBox.Show("Deleted Successfully");
                gridview();
                reset();
            }
            else
            {
                MessageBox.Show("Invalid Error");
            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void sear_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);

            string query = "select * from ab where name like @name +'%'";
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            adp.SelectCommand.Parameters.AddWithValue("@name", search.Text);
            DataTable data = new DataTable();
            adp.Fill(data);


            if (data.Rows.Count > 0)
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                MessageBox.Show("data does not exists");
                dataGridView1.DataSource = null;

            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(conn);

            //string query = "select * from abc where name like @name +'%'";
            //SqlDataAdapter adp = new SqlDataAdapter(query, con);
            //adp.SelectCommand.Parameters.AddWithValue("@name", search.Text);
            //DataTable data = new DataTable();
            //adp.Fill(data);


            //if (data.Rows.Count > 0)
            //{
            //    dataGridView1.DataSource = data;
            //}
            //else
            //{
            //    MessageBox.Show("data does not exists");
            //    dataGridView1.DataSource = null;

            //}
        }
    }
    
}
