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
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nuwak\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select *from CarTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtRegNo.Text == "" || txtBrand.Text == "" || txtModel.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Insert into CarTbl values('" + txtRegNo.Text + "','" + txtBrand.Text + "','" + txtModel.Text + "','" + cmbAvailable.SelectedItem.ToString() + "','" + txtPrice.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Added");
                    Con.Close();

                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

       


        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Car_Load(object sender, EventArgs e)
        {
            populate();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtRegNo.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from CarTbl Where RegNum= '" + txtRegNo.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Details Delete Successfully");

                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void CarDGV_DoubleClick(object sender, EventArgs e)
        {
            txtRegNo.Text = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtBrand.Text = CarDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtModel.Text = CarDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtPrice.Text = CarDGV.SelectedRows[0].Cells[4].Value.ToString();
            cmbAvailable.SelectedItem = CarDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (txtRegNo.Text == "" || txtBrand.Text == "" || txtModel.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update CarTbl set Brand='" + txtBrand.Text + "',Model='" + txtModel.Text + "', Price='" + txtPrice.Text + "', Available='" + cmbAvailable.SelectedItem.ToString() + "' where RegNum='" + txtRegNo.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Detalils Successfully Updated");
                    Con.Close();

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void cmbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";
            if(cmbSearch.SelectedItem.ToString()=="Available")
            {
                flag = "Yes";
            }
            else
            {
                flag = "No";
            }
            Con.Open();
            string query = "select *from CarTbl where Available = '"+flag+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
    }
}
