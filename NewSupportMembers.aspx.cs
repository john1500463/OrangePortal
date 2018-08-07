using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrangePortal_NewSupportMembers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt;
            String UserName;
            String Rights = "S";
            String Email;
            String FTID;
            DataTable dt1;
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT  Distinct [PE Last name + First Name] ,[PE Login Name],[PE Email]FROM [Expedite].[dbo].[Staff];";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserName = (dt.Rows[i][0]).ToString();
                UserName=UserName.Replace(" ", ".");
                FTID = dt.Rows[i][1].ToString();
                Email = dt.Rows[i][2].ToString();
         
            command.CommandText = "Insert Into [Expedite].[dbo].[Users] (Username,Rights,Email,FTID) Values ('"+UserName+"','"+Rights+"','"+Email+"','"+FTID+"')";
           // command.CommandText = "Delete From [Expedite].[dbo].[Users]where FTID='"+FTID+"'";
            
            Debug.WriteLine(command.CommandText);
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
             sda.SelectCommand = command;
                using (dt1 = new DataTable())
                {

                   sda.Fill(dt);

                }
            }
            }
            
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }
}