using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text;

public partial class Pages_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if(pw1.Text==pw2.Text)
        {

            String EncPw = Hash_calculator(pw1.Text);

            String Salt = Session["SALT"].ToString();



            String ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

            using (SqlConnection DBCon = new SqlConnection(ConStr))
            {
                DBCon.Open();
                SqlCommand cmd = new SqlCommand("SP_add_user", DBCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sq_email = new SqlParameter("@Email", email.Text);
                SqlParameter sq_nic = new SqlParameter("@UID", nic.Text);
                SqlParameter sq_name = new SqlParameter("@Name", name.Text);
                SqlParameter sq_username = new SqlParameter("@UN", username.Text);
                SqlParameter sq_pw = new SqlParameter("@PW", EncPw);
                SqlParameter sq_salt = new SqlParameter("@SALT", Salt);

                cmd.Parameters.Add(sq_email);
                cmd.Parameters.Add(sq_nic);
                cmd.Parameters.Add(sq_name);
                cmd.Parameters.Add(sq_username);
                cmd.Parameters.Add(sq_pw);
                cmd.Parameters.Add(sq_salt);


                int ReturnCode = (int)cmd.ExecuteScalar();

                if (ReturnCode == 1)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('User added successfully');", true);
                    Label1.ForeColor = System.Drawing.Color.Green;
                    username.Text = String.Empty;
                    pw1.Text = String.Empty;
                    pw2.Text = String.Empty;
                    nic.Text = String.Empty;
                    email.Text = String.Empty;


                }
                else
                {
                   // Label1.Text = "Already a user has added to this NIC No";
                    Label1.ForeColor = System.Drawing.Color.Red;
                    //Label2.Text = " ";
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Already a user has added to this NIC No');", true);
                }
            }
        }
        else
        Label3.Text = "Password Do Not Match";
        Label3.ForeColor = System.Drawing.Color.Green;
        //Label.Text = " ";
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

    private string Hash_calculator(String y)
    {
        String EncPw = String.Empty, hash_text = String.Empty;
        RandomGenerator generator = new RandomGenerator();
        string salt = generator.RandomPassword();

        string plain_text = pw1.Text + salt;
        Session["SALT"] = salt.ToString();

        int x = 0;
        while (x < 10)
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