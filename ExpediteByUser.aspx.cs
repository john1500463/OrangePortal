using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class OrangePortal_ExpediteByUser : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (((String)Session["Right"]) == "else")
        {
            Response.Redirect("Home_Page_User.aspx");
        }
        if (((String)Session["Right"]) == "S")
        {
            Response.Redirect("Home_Page_Support.aspx");
        }
        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
        ArrayList UrgencyReasons = new ArrayList();
        ArrayList Counters = new ArrayList();

        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        conn.Open();
        
        SqlCommand command = new SqlCommand();
        DataTable dt2 = new DataTable();
        command.Connection = conn;
        command.CommandText = "SELECT distinct [PE Full Name] as 'Full Name' ,[Expedite_By] as 'FTID' FROM [Expedite].[dbo].[Expedite_time] inner join [Expedite].[dbo].[Staff] on [Expedite].[dbo].[Expedite_time].[Expedite_By] = [Expedite].[dbo].[Staff].[PE Login Name]";
        SqlCommand command2;
        command2 = new SqlCommand();
        command2.Connection = conn;
        //   command.CommandText = "Select * From [dbo].['All_Incidents'];";


        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            Debug.WriteLine(command.CommandText);
            using (dt = new DataTable())
            {

                sda.Fill(dt);


            }
            foreach (DataRow row in dt.Rows)
            {
               
                    UrgencyReasons.Add(row[1]);


            }
            DataColumn countCol = dt.Columns.Add("Number", typeof(Int32));
            for (int Counter = 0; Counter < UrgencyReasons.Count; Counter++)
            {

                command2.CommandText = "SELECT Count(*) FROM [dbo].[Expedite_time] Where [Expedite_By] = '" + UrgencyReasons[Counter] + "';";
                sda.SelectCommand = command2;
                sda.Fill(dt2);
                dt.Rows[Counter][2] = dt2.Rows[Counter][0];


            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
            clickable_incidents();
        }
        conn.Close();
    }
    string GetLastModifiedDate()
    {
        return System.IO.File.GetLastWriteTime("D:/Expedite/NewExpedite.xls").ToString();
    }
    protected void clickable_incidents()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HyperLink hlContro = new HyperLink();
            String Incident = GridView1.Rows[i].Cells[0].Text;
            hlContro.NavigateUrl = String.Format("javascript:void(window.open('" + "./Expedited_Users.aspx?FTID=" + dt.Rows[i][1] + "&NAME="+dt.Rows[i][0]+"','_blank'));");
            hlContro.Text = GridView1.Rows[i].Cells[0].Text;
            GridView1.Rows[i].Cells[0].Controls.Add(hlContro);
        }

    }
    

    string GetLastModifiedDateExe()
    {
        dt = new DataTable();
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
        conn.Close();
        return dt.Rows[0][0].ToString();
    
    }
   
}