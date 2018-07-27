using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
    
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String Username = TextBox1.Text;
        String FTID = TextBox3.Text;
        String Email = TextBox4.Text;
        String RoleName = DropDownList1.SelectedItem.Value;
        
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        DataTable dt1 = new DataTable();
        conn.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.CommandText = "INSERT INTO [Expedite].[dbo].[Users] ([Username],[Rights],[Email],[FTID]) Values ('"+Username+"','"+RoleName+"','"+Email+"','"+FTID+"')";

        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt1 = new DataTable())
            {

                sda.Fill(dt1);

            }
        }
        Response.Redirect("ModifyUser.aspx");
         
    }
    



  
}