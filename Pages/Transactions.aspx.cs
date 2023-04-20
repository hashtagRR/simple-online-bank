using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text;

public partial class Pages_Transactions : System.Web.UI.Page
{
    String ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["is_logged_in"] == null)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('You are trying to access the site in unauthorized way.Please use proper sign in to access the site. ');", true);
            Response.Redirect("Default.aspx");
            load_transactions();
        }
        else
            Response.Redirect("Desault.aspx");
    }

    public void load_transactions()
    {
        using (SqlConnection DBCon = new SqlConnection(ConStr))
        {
            DBCon.Open();
            SqlCommand sqlcmd = new SqlCommand("Select transactions AS Reciever,ammount AS Ammount,type as Type,date as Date FROM transactions WHERE account_no='" + Session["acc_no"].ToString() + "' AND type='Transaction' ", DBCon);
            SqlDataReader reader = sqlcmd.ExecuteReader();
            if (reader.HasRows)
            {
                dgv1.DataSource = reader;
                dgv1.DataBind();
                dgv1.Visible = true;
                Label1.Text = " ";

            }
            else
            {
                Label1.Text = "No record are avaliable for this Subject ID";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        

        using (SqlConnection DBCon = new SqlConnection(ConStr))
        {
            DBCon.Open();
            
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + EncPw.ToString() + "');", true);

            SqlCommand cmd = new SqlCommand("SP_transaction", DBCon);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter sq_transacions = new SqlParameter("@TRANS", ac_no.Text);
            SqlParameter sq_ammount = new SqlParameter("@AMMOUNT", amount.Text);
            SqlParameter sq_accno = new SqlParameter("@ACC_NO", Session["acc_no"].ToString());


            cmd.Parameters.Add(sq_transacions);
            cmd.Parameters.Add(sq_ammount);
            cmd.Parameters.Add(sq_accno);
            

            int ReturnCode = (int)cmd.ExecuteScalar();

            if (ReturnCode == 1)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Transaction Successful');", true);
                
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Well, this is embarrassing. Our System has a trouble with your authentication. Please try again later');", true);
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('"+salt2+"');", true);
            }
        }
    }
}