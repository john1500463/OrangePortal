using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Support_Ack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC Incident Number] From [Expedite].[dbo].['All_Incidents'] EXCEPT Select [INC Incident Number] From [Expedite].[dbo].['All_Incidents']  Where [INC Status]='Resolved' OR [INC Status]='Closed';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            ArrayList arr = new ArrayList();

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    arr.Add(row[column]);
                }
            }
            DataTable dt2 = new DataTable();

            SqlCommand command3 = new SqlCommand();
            for (int counter = 0; counter < arr.Count; counter++)
            {
                command3.Connection = conn;
                command3.CommandText = "Select [Incident_ID],[Submit_Date],[Expedite_By],[Expedite_Date],[Urgency_Reason] From [dbo].[Expedite_time] Where [Incident_ID] ='" + arr[counter] + "' AND [Expedite_By]='" + x +"';";

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command3;
                sda.Fill(dt2);
            }


                GridView1.DataSource = dt2;
                GridView1.DataBind();
                GridView1.Visible = true;


            //DataColumn dc = new DataColumn("Acknowledge", typeof(bool));
           //dc.AllowDBNull = false;
            //dc.Unique = true;  
           //dt.Columns.Add(dc);
           /* DataColumn dc1 = new DataColumn("PageName");
            dt.Columns.Add(dc1);

            foreach (var item in RoleName)
            {
                DataColumn dc = new DataColumn(item.RoleName, typeof(bool));
                dt.Columns.Add(dc);
            }

            foreach (var page in pageName)
            {
                DataRow dr = dt.NewRow();
                dr["PageName"] = page.PAGE_NAME;

                foreach (var role in RoleName)
                {
                    dr[role.RoleName] = true;
                }
                dt.Rows.Add(dr);
            }    
            */
            //GridView1.DataSource = dt;
           // GridView1.DataBind();
            //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
}