using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeWinformsApp.econtactClasses;

namespace YoutubeWinformsApp
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        ContactClass c = new ContactClass();

        private void Econtact_Load(object sender, EventArgs e)
        {
            // Show data on Data GridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Update
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtbContactID.Text == "") MessageBox.Show("You must select an item to update.");
            else
            {

                c.firstName = txtbFirstName.Text;
                c.lastName = txtbLastName.Text;
                c.number = txtbNumber.Text;
                c.contactID = int.Parse(txtbContactID.Text);

                bool success = c.Update(c);
                if (success)
                {
                    MessageBox.Show(String.Format("{0} updated.", c.firstName));
                    Clear();
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }

                // Show data on Data GridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
            }

        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Get contents
            string keyword = txtbSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblContact WHERE FirstName LIKE '%"+keyword+"%' OR LastName LIKE '%"+keyword+"%' OR Number LIKE '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvContactList.DataSource = dt;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the values from the form
            c.firstName = txtbFirstName.Text;
            c.lastName = txtbLastName.Text;
            c.number = txtbNumber.Text;

            // Inserting data into database using
            bool success = c.Insert(c);
            if (success)
            {
                MessageBox.Show("New contact added.");
                Clear();
            }
            else
            {
                MessageBox.Show("New contact failed.");
            }

            // Show data on Data GridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void txtbContactID_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtbContactID.Clear();
            txtbFirstName.Clear();
            txtbLastName.Clear();
            txtbNumber.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtbContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtbFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtbLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtbNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtbContactID.Text == "") MessageBox.Show("Select an item to delete.");
            else
            {
                c.contactID = int.Parse(txtbContactID.Text);

                bool success = c.Delete(c);
                if (success)
                {
                    MessageBox.Show(String.Format("{0} Deleted.", c.firstName));
                    Clear();
                }
                else
                {
                    MessageBox.Show("Delete failed.");
                }

                // Show data on Data GridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
            }
        }
    }
}
