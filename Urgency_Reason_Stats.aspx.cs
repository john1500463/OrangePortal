using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Urgency_Reason_Stats : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
        ArrayList UrgencyReasons = new ArrayList();
        ArrayList Counters = new ArrayList();
       
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        conn.Open();
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand();
        DataTable dt2 = new DataTable();
        command.Connection = conn;
        command.CommandText = "SELECT [UrgencyReason] as 'Urgency Reason' FROM [Expedite].[dbo].[UrgencyReasons]";
        SqlCommand command2;
        command2 = new SqlCommand();
        command2.Connection = conn;
        //   command.CommandText = "Select * From [dbo].['All_Incidents'];";


        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt = new DataTable())
            {

                sda.Fill(dt);


            }
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    UrgencyReasons.Add(row[column]);
                    

                }
            }
            DataColumn countCol = dt.Columns.Add("Number",typeof(Int32));
            for (int Counter = 0; Counter < UrgencyReasons.Count; Counter++)
            {

                command2.CommandText = "SELECT Count(*) FROM [dbo].[Expedite_time] Where [Urgency_Reason] = '" + UrgencyReasons[Counter] + "';";
                sda.SelectCommand = command2;
                sda.Fill(dt2);
                dt.Rows[Counter][1] = dt2.Rows[Counter][0];
                

            }
        
            
          
            

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
        }

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
       
                