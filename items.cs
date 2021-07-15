using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryManagement
{
    public partial class items : Form
    {
        public items()
        {
            InitializeComponent();
            populate();
        }
        int key = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\GroceryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "Select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDVG.DataSource = ds.Tables[0];
            con.Close();
        }
        private void clear()
        {
            ItNameTb.Text = "";
            ItQtyTb.Text = "";
            ItPriceTb.Text = "";
            ItCatCb.SelectedIndex = -1;
            key = 0;
        }
        

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || ItPriceTb.Text == "" || ItCatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missig Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ItemTbl values('" + ItNameTb.Text + "','" + ItQtyTb.Text + "','" + ItPriceTb.Text + "','" + ItCatCb.SelectedItem.ToString() + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Items Saved Successfully");
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

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {

            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || ItPriceTb.Text == "" || ItCatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select The Item To be Edited!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "Update ItemTbl set ItName='" + ItNameTb.Text + "',ItQty='" + ItQtyTb.Text + "',ItPrice='" + ItPriceTb.Text + "',ItCat='" + ItCatCb.SelectedItem.ToString() + "' where ItID='" + key + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Updated Successfully!!!");
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
        
        private void ItemDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = ItemDVG.SelectedRows[0].Cells[1].Value.ToString();
            ItQtyTb.Text = ItemDVG.SelectedRows[0].Cells[2].Value.ToString();
            ItPriceTb.Text = ItemDVG.SelectedRows[0].Cells[3].Value.ToString();
            ItCatCb.SelectedItem = ItemDVG.SelectedRows[0].Cells[4].Value.ToString();
            if (ItNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ItemDVG.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Item To be Deleted!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "Delete from ItemTbl where ItId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Items Deleted Successfully");
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

