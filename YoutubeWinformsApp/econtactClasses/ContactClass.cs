using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWinformsApp.econtactClasses
{
    class ContactClass
    {
        // Getter Setter Props
        public int contactID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string number { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        // Select Data from database
        public DataTable Select()
        {
            // Connect
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                // Querying the Database
                string sql = "SELECT * FROM tblContact";
                // Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        
        // Insert into DataBase
        public bool Insert(ContactClass c)
        {
            // Creating default return type and settings its value to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                // Create an SQL query to insert data
                string sql = "INSERT INTO tblContact (FirstName, LastName, Number) VALUES(@FirstName, @LastName, @Number)";
                // Creating SQL cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Create parmas to add data
                cmd.Parameters.AddWithValue("@FirstName", c.firstName);
                cmd.Parameters.AddWithValue("@LastName", c.lastName);
                cmd.Parameters.AddWithValue("@Number", c.number);
                // Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query runs succesfully then the value of rows will be greater than 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            
            return isSuccess;
        } 

        // Update data
        public bool Update(ContactClass c)
        {
            // Create default return
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                // SQL to update data
                string sql = "UPDATE tblContact SET FirstName=@FirstName, LastName=@LastName, Number=@Number WHERE ContactID=@ContactID";
                // Creating SQL cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.firstName);
                cmd.Parameters.AddWithValue("@LastName", c.lastName);
                cmd.Parameters.AddWithValue("@Number", c.number);
                cmd.Parameters.AddWithValue("@ContactID", c.contactID);
                // Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query runs succesfully then the value of rows will be greater than 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        // Delete from Database 
        public bool Delete(ContactClass c)
        {
            // Create default return
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                // SQL to update data
                string sql = "DELETE FROM tblContact WHERE ContactID=@ContactID";
                // Creating SQL cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.contactID);
                // Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query runs succesfully then the value of rows will be greater than 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
