using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text;

public partial class Pages_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["is_logged_in"] == null)
         {
             System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('You are trying to access the site in unauthorized way.Please use proper sign in to access the site. ');", true);
             Response.Redirect("Default.aspx");
            populate_data1();
            populate_data2();
        }
        
    }
    public void populate_data1()
    {
        String ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        using (SqlConnection DBCon = new SqlConnection(ConStr))
        {
            DBCon.Open();
            String sql = "SELECT SUM(ammount) AS amnt from transactions where account_no=(SELECT account_no from accounts where uid='"+Session["uid"].ToString()+"') ";
            SqlCommand comm = new SqlCommand(sql, DBCon);
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Session["ammount"] = (int)nwReader["amnt"];
                
            }
            nwReader.Close();

            lbl_spent.Text = Session["ammount"].ToString();
        }
    }
    public void populate_data2()
    {
        String ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        using (SqlConnection DBCon = new SqlConnection(ConStr))
        {
            DBCon.Open();
            String sql = "SELECT balance,account_no from accounts where uid='" + Session["uid"].ToString() + "' ";
            SqlCommand comm = new SqlCommand(sql, DBCon);
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Session["balance"] = (int)nwReader["balance"];
                Session["acc_no"] = (int)nwReader["account_no"];

            }
            nwReader.Close();

            lbl_income.Text = Session["balance"].ToString();
        }
    }
}