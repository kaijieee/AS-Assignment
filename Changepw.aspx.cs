using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment
{
    public partial class Changepw : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            string currentp = tb_currpw.Text.ToString().Trim();
            string newp = tb_newpw.Text.ToString().Trim();
            string uemail = tb_useremail.Text.ToString().Trim();

            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string pwdWithSalt = newp + salt;
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

            finalHash = Convert.ToBase64String(hashWithSalt);

            string dbHash = getDBHash(uemail);
            string dbSalt = getDBSalt(uemail);
            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string cpwdWithSalt = currentp + dbSalt;
                    byte[] chashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(cpwdWithSalt));
                    string userHash = Convert.ToBase64String(chashWithSalt);
                    if (userHash.Equals(dbHash))
                    {
                        Session["email"] = uemail;
                        SqlConnection con = new SqlConnection(MYDBConnectionString);
                        String updatehash = ("Update Account set PasswordHash=@hash where email=@email");
                        String updatesalt = ("Update Account set PasswordSalt=@salt where email=@email");
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = updatehash;
                        cmd.CommandText = updatesalt;
                        cmd.Parameters.AddWithValue("email", uemail);
                        cmd.Parameters.AddWithValue("hash", finalHash);
                        cmd.Parameters.AddWithValue("salt", salt);
                        con.Open();
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Login.aspx", false);
                    }

                    else
                    {
                        lbl_currpw.Text = "Email or password is not valid. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }

        }
        protected string getDBSalt(string uemail)
        {

            string s = null;

            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "Select PasswordSalt from Account where email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", uemail);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                s = reader["PasswordSalt"].ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { connection.Close(); }
            return s;

        }
        protected string getDBHash(string uemail)
        {

            string h = null;

            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "Select PasswordHash from Account where email = @email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", uemail);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { connection.Close(); }
            return h;
        }

    }
}
