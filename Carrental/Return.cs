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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nuwak\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
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
        private void populateReturn()
        {
            Con.Open();
            string query = "select *from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Deleteonreturn()
        {
            int rentId;
            rentId =Convert.ToInt32( RentDGV.SelectedRows[0].Cells[1].Value.ToString());
            Con.Open();
            string query = "delete from RentTbl Where RentId= '" + rentId + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Rental Details Delete Successfully");

            Con.Close();
            populate();
           // UpdateonRentDelete();
        }

        private void Returndate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populateReturn();
        }

        private void RentDGV_DoubleClick(object sender, EventArgs e)
        {
           
            txtCarid.Text = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtCustname.Text = RentDGV.SelectedRows[0].Cells[2].Value.ToString();
            Returndate.Text = RentDGV.SelectedRows[0].Cells[4].Value.ToString();
            DateTime d1 = Returndate.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2 - d1;
            int NrOfDays = Convert.ToInt32(t.TotalDays);
            if(NrOfDays<=0)
            {
                txtDelay.Text = "No Delay";
                txtFine.Text = "0";

            }
            else
            {
                txtDelay.Text = "" + NrOfDays;
                txtFine.Text = "" + (NrOfDays * 1500);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainform main = new Mainform();
            main.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtRerurnid.Text == "" || txtCarid.Text == "" || txtCustname.Text == "" || txtDelay.Text == "" || txtFine.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Insert into ReturnTbl values('" + txtRerurnid.Text + "','" + txtCarid.Text + "','" + txtCustname.Text + "','" + Returndate.Text + "','" + txtDelay.Text + "','" + txtFine.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Returned Successfull");
                    Con.Close();
                    //UpdateonRent();
                    
                    populateReturn();
                    Deleteonreturn();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
        }
    }
}
