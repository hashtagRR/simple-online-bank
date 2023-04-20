using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    private string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    int login_count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Btn_clear(object sender, EventArgs e)
    {
        un.Text = String.Empty;
        pw.Text = String.Empty;
    }

    protected void Btn_login(object sender, EventArgs e)
    {
        if (un.Text == String.Empty && pw.Text == String.Empty)
        {
            Label1.Text = "Username and password cannot be empty";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
          if (un.Text == String.Empty)
        {
            Label1.Text = "Username cannot be empty";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
              if (pw.Text == String.Empty)
        {
            Label1.Text = "Password cannot be empty";
            Label1.ForeColor = System.Drawing.Color.Red;
            Session["login_count"] = 0;
        }
        else
        if(Convert.ToInt32(Session["login_count"])>3)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('You have reached the maximum no of login attempts. Please try again in a 30 minutes');", true);
        }
        else
        {

            String ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

            using (SqlConnection DBCon = new SqlConnection(ConStr))
            {
                DBCon.Open();

                SqlCommand cmd = new SqlCommand("SP_loginuser_UNvalidation", DBCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               
                SqlParameter sqpusername = new SqlParameter("@UN", un.Text);

                cmd.Parameters.Add(sqpusername);

                int ReturnCode = (int)cmd.ExecuteScalar();

                if (ReturnCode == 1)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        String sql = "SELECT UID,SALT,INCREMENT,PW_TYPE FROM login_data WHERE UN='" + un.Text + "'";
                        SqlCommand comm = new SqlCommand(sql, DBCon);
                        SqlDataReader nwReader = comm.ExecuteReader();
                        while (nwReader.Read())
                        {
                            Session["uid"] = (int)nwReader["UID"];
                            Session["salt"] = (string)nwReader["SALT"];
                            Session["increment"] = (int)nwReader["INCREMENT"];
                            Session["pw_type"] = (string)nwReader["PW_TYPE"];
                        }
                        nwReader.Close();
                        int increment = Convert.ToInt32(Session["increment"]);
                        String salt = Session["salt"].ToString();
                        String pw_type = Session["pw_type"].ToString();

                        if (pw_type == "System_Created")
                        {
                            String EncPw = Hash_calculator_1(pw.Text);

                           // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + EncPw + "');", true);

                            
                            SqlCommand cmd2 = new SqlCommand("SP_user_login", DBCon);
                            cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlParameter sq_username = new SqlParameter("@UN", un.Text);
                            SqlParameter sq_password = new SqlParameter("@PW", EncPw);


                            Session["un"] = un.Text;

                            cmd2.Parameters.Add(sq_username);
                            cmd2.Parameters.Add(sq_password);

                            int ReturnCode5 = (int)cmd2.ExecuteScalar();

                            if (ReturnCode5 == 1)
                            {
                                Session["is_logged_in"] = true;
                                Response.Redirect("Reset_Password.aspx");
                            }
                            else
                            {
                                Label1.Text = "Wrong Passsword";
                                Label1.ForeColor = System.Drawing.Color.Red;
                                Btn_clear(sender, e);
                               Session["login_count"] = Convert.ToInt32(Session["login_count"]) + 1;
                            }
                        }
                        else
                            if (pw_type == "User_Created")
                        {
                            //int x = 1,y=1;
                            String hash_text=String.Empty;

                            String EncPw2 = Hash_calculator_1(pw.Text);
                            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + EncPw2 + "');", true);
                            

                            SqlCommand cmd3 = new SqlCommand("SP_user_login", DBCon);
                            cmd3.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlParameter sq_username = new SqlParameter("@UN", un.Text);
                            SqlParameter sq_password = new SqlParameter("@PW", EncPw2.ToString());

                            cmd3.Parameters.Add(sq_username);
                            cmd3.Parameters.Add(sq_password);

                            int ReturnCode5 = (int)cmd3.ExecuteScalar();

                            if (ReturnCode5 == 1)
                            {
                                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Success');", true);
                                String EncPw3 = Hash_calculator(pw.Text);

                                int random_increment = Random_Number(100,10000);

                                String Salt2 = Session["SALT2"].ToString();
                               
                              // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + un.Text + "');", true);
                               
                                 SqlCommand cmd4 = new SqlCommand("SP_login_data_update", DBCon);
                                 cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                                 SqlParameter sq_username1 = new SqlParameter("@UN1", un.Text);
                                 SqlParameter sq_password1 = new SqlParameter("@PW1", EncPw2.ToString());
                                 SqlParameter sq_password2 = new SqlParameter("@PW2", EncPw3.ToString());
                                 SqlParameter sq_salt2 = new SqlParameter("@SALT", Salt2.ToString());
                                 SqlParameter sq_increment2 = new SqlParameter("@INCREMENT", random_increment);

                                 cmd4.Parameters.Add(sq_username1);
                                 cmd4.Parameters.Add(sq_password1);
                                 cmd4.Parameters.Add(sq_password2);
                                 cmd4.Parameters.Add(sq_salt2);
                                 cmd4.Parameters.Add(sq_increment2);

                                 int ReturnCode3 = (int)cmd4.ExecuteScalar();

                                 if (ReturnCode3 == 1)
                                 {
                                     //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('"+Session["uid"].ToString()+"');", true);
                                     Session["is_logged_in"] = true;
                                     Response.Redirect("Home.aspx");
                                 }
                                 else
                                 {
                                     System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Well, this is embarrassing. Our System has a trouble with your authentication. Please try again later');", true);
                                      //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('"+salt2+"');", true);
                                 }
                                 
    
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Incorrect Password. Please try again');", true);
                            }
                            
                        }    
                    }
                }
                else
                if (ReturnCode == 0)
                {
                    Label1.Text = "Incorrect Username, Please Try Again";
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Btn_clear(sender, e);
                    Session["login_count"] = Convert.ToInt32(Session["login_count"]) + 1;
                }
            }
        }
    }

    // Generate a random number between two numbers  
    public int Random_Number(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
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

    private string Hash_calculator_1(String y)
    {
        String EncPw = String.Empty, hash_text = String.Empty;
       // RandomGenerator generator = new RandomGenerator();

        String salt = Session["salt"].ToString();

        string plain_text = pw.Text + salt;

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


    private string Hash_calculator(String input)
    {
        String EncPw = String.Empty, hash_text = String.Empty;
        RandomGenerator generator = new RandomGenerator();
        string salt = generator.RandomPassword();

        Session["SALT2"]=salt.ToString();

        string plain_text = pw.Text + salt;

        int increment= Convert.ToInt32(Session["increment"]);

        int x = 0;
        while (x < increment)
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