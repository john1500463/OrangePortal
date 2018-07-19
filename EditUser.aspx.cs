using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditUser : System.Web.UI.Page
{
    String FTID;
    protected void Page_Load(object sender, EventArgs e)
    {

         FTID = Request.QueryString["param1"];
        if (FTID == null)
        {
            Response.Redirect("Default.aspx");
        }
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        DataTable dt;
        conn.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.CommandText = "Select * From [Expedite].[dbo].[Users] Where [FTID]= '" + FTID + "';";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }
        }

        Label7.Text = dt.Rows[0][1].ToString();
        Label8.Text = dt.Rows[0][3].ToString();
        if(dt.Rows[0][2].ToString() == "S"){
        Label9.Text = "Support";
         }
        if (dt.Rows[0][2].ToString() == "A")
        {
            Label9.Text = "Admin";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        String UserName = TextBox1.Text;
        String Email = TextBox3.Text;
        String Role = DropDownList1.SelectedItem.Value;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        DataTable dt = new DataTable();
        conn.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.CommandText = "Update [Expedite].[dbo].[Users] SET [Username]='" + UserName + "',[Rights]='" + Role + "', [Email] = '" + Email + "' Where [FTID]= '" + FTID + "' ;";

        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }
        }
        Response.Redirect("User.aspx");
    
    }
}