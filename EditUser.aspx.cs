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
        if (!Page.IsPostBack)
        {
            TextBox1.Text = dt.Rows[0][1].ToString();
            TextBox3.Text = dt.Rows[0][3].ToString();
        }

        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
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
        Response.Redirect("ModifyUser.aspx");
   
    }
    string GetLastModifiedDate()
    {
        return System.IO.File.GetLastWriteTime("C:/Users/wkzw7370/Downloads/Project Code/WebSite2/OrangePortal/NewExpedite.xls").ToString();
    }

    string GetLastModifiedDateExe()
    {
        DataTable dt;
        SqlCommand command = new SqlCommand();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        conn.Open();
        command.Connection = conn;
        command.CommandText = "select * from [expedite].[dbo].[Last_Update_Time]";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }

        }
        return dt.Rows[0][0].ToString();
    }
}