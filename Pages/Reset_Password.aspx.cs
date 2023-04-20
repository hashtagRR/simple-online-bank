using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text;

public partial class Pages_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["is_logged_in"] == null)
        { 
               Response.Redirect("Default.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (npw1.Text == npw2.Text)
        {
            String ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

            using (SqlConnection DBCon = new SqlConnection(ConStr))
            {
                DBCon.Open();

                String EncPw = Hash_calculator(npw1.Text);

                String Salt = Session["SALT"].ToString();
                // int increment = Convert.ToInt32(Session["INCREMENT"]);

                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + Session["pw"].ToString() + "');", true);
                
                SqlCommand cmd = new SqlCommand("create proc [dbo].[SP_login_data_password_reset]", DBCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sq_username1 = new SqlParameter("@UN1", Session["un"].ToString());
                SqlParameter sq_password1 = new SqlParameter("@UID", Session["uid"].ToString());
                SqlParameter sq_password2 = new SqlParameter("@PW2", EncPw.ToString());
                SqlParameter sq_salt = new SqlParameter("@SALT", Salt.ToString());
                SqlParameter sq_increment = new SqlParameter("@INCREMENT", Convert.ToInt32(Session["INCREMENT"]));

                cmd.Parameters.Add(sq_username1);
                cmd.Parameters.Add(sq_password1);
                cmd.Parameters.Add(sq_password2);
                cmd.Parameters.Add(sq_salt);
                cmd.Parameters.Add(sq_increment);

                int ReturnCode3 = (int)cmd.ExecuteScalar();

                if (ReturnCode3 == 1)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Password Successfully Changed');", true);
                    Session["is_logged_in"] = true;
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Well, this is embarrassing. Our System has a trouble with your authentication. Please try again later');", true);
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('"+salt2+"');", true);
                }
            }

        }
        else
        {
            Label3.Text = "Passwords Do Not Match";
            Label3.ForeColor = System.Drawing.Color.Red;
        }

    }
    public class RandomGenerator
    {
        // Generate a random number between two numbers  
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size  
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password  
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
    public int Random_Number(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    private string Hash_calculator(String input)
    {
        String EncPw = String.Empty, hash_text = String.Empty;
        RandomGenerator generator = new RandomGenerator();
        string salt = generator.RandomPassword();

        string plain_text = npw1.Text + salt;
        int random_increment = Random_Number(100, 10000);

        Session["SALT"] = salt.ToString();
        Session["INCREMENT"] = random_increment;

        int x = 0;
        while (x < random_increment)
        {
            EncPw = FormsAuthentication.HashPasswordForStoringInConfigFile(plain_text, "SHA256");
            EncPw = FormsAuthentication.HashPasswordForStoringInConfigFile(EncPw, "SHA512");
            hash_text = EncPw.ToString();
            x++;
        }
        string return_hash = hash_text.ToString();
        return return_hash.ToString();
    }
}