using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Assignment
{
    public partial class Registration : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            var fname = tb_fname.Text;
            var lname = tb_lname.Text;
            var dob = tb_dob.Text;
            var email = tb_email.Text;
            var credit = tb_creditcard.Text;    
           
            if (fname.Length == 0)
            {
                lbl_fname.Text = "First name cannot be empty";
            }           
            if (fname.Length < 3)
            {
                lbl_fname.Text = "First name should be at least 3 characters";
            }           
            if (lname.Length == 0)
            {
                lbl_lname.Text = "Last name cannot be empty";
            }           
            if (lname.Length < 3)
            {
                lbl_lname.Text = "Last name should be at least 3 characters";
            }
            if (dob.Length == 0)
            {
                RequiredFieldValidator1.Text = "Date of birth cannot be empty";
            }            
            if (email.Length == 0)
            {
                RequiredFieldValidator3.Text = "Email cannot be empty";
            }          
            if (credit.Length == 0)
            {
                lbl_creditcard.Text = "Credit card info cannot be empty";
            }
            if (credit.Length < 8 || credit.Length > 8)
            {
                lbl_creditcard.Text = "Credit card info should be 8 digits";
            }

            // implement codes for the button event
            // Extract data from textbox
            int scores = checkPassword(tb_password.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Excellent";
                    break;
                default:
                    break;
            }
            lbl_pwchecker.Text = "Status : " + status;
            if (scores < 4)
            {
                lbl_pwchecker.ForeColor = Color.Red;
                return;
            }
            lbl_pwchecker.ForeColor = Color.Green;

            string pwd = tb_password.Text.ToString().Trim(); ;

            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

            finalHash = Convert.ToBase64String(hashWithSalt);

            lb_error1.Text = "Salt:" + salt;
            lb_error2.Text = "Hash with salt:" + finalHash;

            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;

            createAccount();


        }

        private int checkPassword(string password)
        {
            int score = 0;
            if (password.Length < 12)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }
            return score;
        }

        protected void tb_password_TextChanged(object sender, EventArgs e)
        {

        }
        protected void createAccount()
        {
            try
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(fu_photo.PostedFile.InputStream))
                {
                    bytes = br.ReadBytes(fu_photo.PostedFile.ContentLength);
                }
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                 
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@fname, @lname, @dob, @email, @PasswordHash, @PasswordSalt, @creditcard, @profilepic, @IV, @Key, @status)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@fname", tb_fname.Text.Trim());
                            cmd.Parameters.AddWithValue("@lname", tb_lname.Text.Trim());
                            //cmd.Parameters.AddWithValue("@Nric", encryptData(tb_nric.Text.Trim()));
                            cmd.Parameters.AddWithValue("@dob", tb_dob.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", tb_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@profilepic", bytes);
                            cmd.Parameters.AddWithValue("@creditcard", Convert.ToBase64String(encryptData(tb_creditcard.Text.Trim())));
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Parameters.AddWithValue("@status", "open");
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Response.Redirect("/HomePage.aspx", false);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);


                //Encrypt
                //cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
                //cipherString = Convert.ToBase64String(cipherText);
                //Console.WriteLine("Encrypted Text: " + cipherString);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { }
            return cipherText;
        }
    }
}