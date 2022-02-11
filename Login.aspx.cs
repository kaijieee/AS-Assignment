using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace Assignment
{
    public partial class Login : System.Web.UI.Page
    {
        static int attempt = 0;
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;

        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("/Registration.aspx", false);
        }
        public bool ValidateCaptcha()
        {
            bool result = true;

            //When user submits the recaptcha form, the user gets a response POST parameter. 
            //captchaResponse consist of the user click pattern. Behaviour analytics! AI :) 
            string captchaResponse = Request.Form["g-recaptcha-response"];

            //To send a GET request to Google along with the response and Secret key.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
           (" https://www.google.com/recaptcha/api/siteverify?secret=6LfwZGkeAAAAAK3BKeWAVC3joFJkfH-omWT5HBYp &response=" + captchaResponse);


            try
            {

                //Codes to receive the Response in JSON format from Google Server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        //The response in JSON format
                        string jsonResponse = readStream.ReadToEnd();

                        ////To show the JSON response string for learning purpose
                        //lbl_gScore.Text = jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        //Create jsonObject to handle the response e.g success or Error
                        //Deserialize Json
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        //Convert the string "False" to bool false or "True" to bool true
                        result = Convert.ToBoolean(jsonObject.success);//

                    }
                }

                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
       
        protected void LoginMe(object sender, EventArgs e)
        {
            string loginemail = tb_userid.Text.ToString().Trim();
            SqlConnection conSelect = new SqlConnection(MYDBConnectionString);
            string SQLSTATUS = "Select status from Account where email='" + loginemail + "'";
            SqlCommand selectstatus = new SqlCommand(SQLSTATUS, conSelect);
            conSelect.Open();
            using (SqlDataReader reader = selectstatus.ExecuteReader())
            {
                while (reader.Read())
                {
                    string dbstatus = null;
                    dbstatus = reader["status"].ToString();

                    if (attempt == 3 || dbstatus == "locked")
                    {
                        lbl_lockout.Text = "Your account has been locked due to multiple invalid login attempts, please contact administrator";
                        SqlConnection con = new SqlConnection(MYDBConnectionString);
                        String updatestatus = "Update Account set status='locked' where email='" + loginemail + "'";
                        con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = updatestatus;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                    }

                    else if (ValidateCaptcha())
                    {

                        // Check for Username and password (hard coded for this demo)
                        if (dbstatus == "open" && tb_userid.Text.Trim().Equals("gkj@gmail.com") && tb_pwd.Text.Trim().Equals("987654321Qq!"))
                        {

                            Session["LoggedIn"] = tb_userid.Text.Trim();

                            // createa a new GUID and save into the session
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            // now create a new cookie with this guid value
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                            Response.Redirect("/HomePage.aspx", false);
                        }
                        else
                        {
                            attempt += 1;
                            lblMessage.Text = "Wrong email or password";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Validate captcha to prove that you are a human.";
                    }
                }
                    
            }
        }
    }
}