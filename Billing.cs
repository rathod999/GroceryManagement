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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\GroceryDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "Select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemListDVG.DataSource = ds.Tables[0];
            con.Close();
        }
        int n = 0,GrdTotal=0,Amount;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            
            if (ItQty.Text =="" ||  Convert.ToInt32(ItQty.Text)>stock||ItName.Text=="")
            {
                MessageBox.Show("Enter Quantity");
            }
            else
            {
                int total = Convert.ToInt32(ItQty.Text) * Convert.ToInt32(ItPrice.Text);
                DataGridViewRow newrow = new DataGridViewRow();
                newrow.CreateCells(BillDVG);
                newrow.Cells[0].Value = n + 1;
                newrow.Cells[1].Value = ItName.Text;
                newrow.Cells[2].Value = ItPrice.Text;
                newrow.Cells[3].Value = ItQty.Text;
                newrow.Cells[4].Value = total;
                BillDVG.Rows.Add(newrow);
                GrdTotal = GrdTotal + total;
                Amount = GrdTotal;
                TotalLbl.Text ="Rs "+GrdTotal;

                n++;
                updateItem();
                Reset();


            }
        }
        private void updateItem()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQty.Text);
                con.Open();
                string query = "Update ItemTbl set ItQty='" +newQty + "' where ItID='" + key + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully!!!");
                con.Close();
                populate();
                //clear();


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);

            }
        }
        int stock = 0,key=0;
        private void Reset()
        {
            ItPrice.Text = "";
            ItQty.Text = "";
            ItCust.Text = "";
            ItName.Text = "";
        }
      
        private void PrintBt_Click(object sender, EventArgs e)
        {
            if (ItCust.Text == "")
            {
                MessageBox.Show("Missig Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl values('" + EmployeeLbl.Text + "','" + ItCust.Text + "'," + Amount + ")", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved Successfully");
                    con.Close();
                    populate();
                    //clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }

        private void ItCust_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Billing_Load(object sender, EventArgs e)
        {
            EmployeeLbl.Text = Login.EmployeeName;
        }
        int pos = 0, prodid = 0,prodprice=0,prodqty=0,total=0;
        string prodname = "";

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            foreach(DataGridViewRow row in BillDVG.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = ""+row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                total = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 300));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 300));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 300));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 300));
                e.Graphics.DrawString("" + total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 300));
                pos = pos + 20;

            }
            e.Graphics.DrawString("Grand Total:RS"+Amount,  new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new Point(450, 300));
            e.Graphics.DrawString("############*Grocery Shop *#############", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new Point(450, 300));
            BillDVG.Rows.Clear();
            BillDVG.Refresh();
            pos = 100;
            Amount = 0;
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();


        }

        private void ItemListDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItName.Text = ItemListDVG.SelectedRows[0].Cells[1].Value.ToString();
           
            ItPrice.Text = ItemListDVG.SelectedRows[0].Cells[3].Value.ToString();
            if (ItName.Text == "")
            {
                stock = 0;
                key = 0;
            }
            else
            {
                stock = Convert.ToInt32(ItemListDVG.SelectedRows[0].Cells[2].Value.ToString());
                key= Convert.ToInt32(ItemListDVG.SelectedRows[0].Cells[0].Value.ToString());
            }


        }

        private void BillDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
