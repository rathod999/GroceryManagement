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


namespace GroceryManagement
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            populate();

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\GroceryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDVG.DataSource = ds.Tables[0];

            con.Close();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Missig Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl values('" + EmpNameTb.Text + "','" + EmpPhoneTb.Text + "','" + EmpAddTb.Text + "','" + EmpPassTb.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved Successfully");
                    con.Close();
                    populate();
                    clear();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }

        }
        private void clear()
        {
            EmpNameTb.Text = "";
            EmpPhoneTb.Text = "";
            EmpAddTb.Text = "";
            EmpPassTb.Text = "";
            key = 0;
        }
        private void Clearbtn_Click(object sender, EventArgs e)
        {
            clear();

        }
        int key = 0;


        private void EmployeeDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeeDVG.SelectedRows[0].Cells[1].Value.ToString();
            EmpPhoneTb.Text = EmployeeDVG.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeeDVG.SelectedRows[0].Cells[3].Value.ToString();
            EmpPassTb.Text = EmployeeDVG.SelectedRows[0].Cells[4].Value.ToString();
            if (EmpNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmployeeDVG.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Employee To be Deleted!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "Delete from EmployeeTbl where EmpId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted Successfully");
                    con.Close();
                    populate();
                    clear();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Select The Employee To be Edited!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "Update EmployeeTbl set EmpName='" + EmpNameTb.Text + "',EmpPhone='" + EmpPassTb.Text + "',EmpAdd='" + EmpAddTb.Text + "',EmpPass='" + EmpPassTb.Text + "' where EmpId='" + key + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated Successfully!!!");
                    con.Close();
                    populate();
                    clear();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}

