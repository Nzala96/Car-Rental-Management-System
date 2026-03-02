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

namespace Carrental
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nuwak\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select *from CustomerTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CutomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainform main = new Mainform();
            main.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCid.Text == "" || txtCname.Text == "" || txtAdd.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Insert into CustomerTbl values('" + txtCid.Text + "','" + txtCname.Text + "','" + txtAdd.Text + "','" + txtPhone.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Successfully Added");
                    Con.Close();
                   
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCid.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from CustomerTbl Where CustId=" + txtCid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Delete Successfully");

                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void CutomerDGV_DoubleClick(object sender, EventArgs e)
        {
            txtCid.Text = CutomerDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtCname.Text = CutomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtAdd.Text = CutomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = CutomerDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCid.Text == "" || txtCname.Text == "" || txtAdd.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update CustomerTbl set CustName='" + txtCname.Text + "',CustAdd='" + txtAdd.Text + "', Phone='" + txtPhone.Text + "' where CustId='" + txtCid.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Detalils Successfully Updated");
                    Con.Close();

                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
    }
}
