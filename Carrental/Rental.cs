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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nuwak\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void fillcombo()
        {
            Con.Open();
            string query = "select RegNum from CarTbl where Available='"+"Yes"+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(string));
            dt.Load(rdr);
            cmbCarreg.ValueMember = "RegNum";
            cmbCarreg.DataSource = dt;
            Con.Close();
        }

        private void fillCustomer()
        {
            Con.Open();
            string query = "select CustId from CustomerTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            cmbCustId.ValueMember = "CustId";
            cmbCustId.DataSource = dt;
            Con.Close();
        }

        private void fetchCustName()
        {
            Con.Open();
            string query = "select *from CustomerTbl where CustId=" + cmbCustId.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                txtCname.Text = dr["CustName"].ToString();

            }
            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "select *from RentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void UpdateonRent()
        {
            Con.Open();
            string query = "update CarTbl set  Available='" +"No"+ "' where RegNum='" + cmbCarreg.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Car Detalils Successfully Updated");
            Con.Close();
        }

        private void UpdateonRentDelete()
        {
            Con.Open();
            string query = "update CarTbl set  Available='" + "Yes" + "' where RegNum='" + cmbCarreg.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
           // MessageBox.Show("Car Detalils Successfully Updated");
            Con.Close();
        }

        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustomer();
            populate();
        }

        private void cmbCarreg_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmbCustId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (txtRid.Text == "" || txtCname.Text == "" || txtRental.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Insert into RentTbl values('" + txtRid.Text + "','" + cmbCarreg.SelectedValue.ToString() + "','" + txtCname.Text + "','" + Rentdate.Text + "','" + Returndate.Text + "','" + txtRental.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Rented");
                    Con.Close();
                    UpdateonRent();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainform main = new Mainform();
            main.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtRid.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from RentTbl Where RentId= '" + txtRid.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental Details Delete Successfully");

                    Con.Close();
                    populate();
                    UpdateonRentDelete();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void RentDGV_DoubleClick(object sender, EventArgs e)
        {
            txtRid.Text = RentDGV.SelectedRows[0].Cells[0].Value.ToString();
            cmbCarreg.SelectedValue = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
            //txtCname.Text = RentDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtRental.Text = RentDGV.SelectedRows[0].Cells[5].Value.ToString();
                      
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
