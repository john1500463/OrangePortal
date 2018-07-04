using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incidents_to_expedite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != null)
        {
            String searchid = TextBox1.Text;
            //Debug.Write(searchid);
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC Incident Number],[INC Status], [AG Assigned Group Name], [AG Assignee] From [Expedite].[dbo].['All_Incidents'] Where [INC Incident Number] = '" + searchid + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {
                    sda.Fill(dt);
                }
            }

            DataTable dt2 = new DataTable();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = conn;
            command.CommandText = "Select * From [Expedite].[dbo].[Expedite_time] Where [Incident_ID] = '" + searchid + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt2 = new DataTable())
            {

                sda.Fill(dt2);

            }
        }
            if (dt2.Rows.Count != 0)
            {
                dt2.Merge(dt);
                Debug.Write("in1");
              /*  for (int i = 0; i < dt2.Columns.Count; i++)
                {
                    Debug.Write("in2");
                    dt.Columns.Add(dt2.Columns[i]);
                }*/
                //Debug.Write("in");
            }

            Debug.Write("out");
            GridView1.DataSource = dt2;
            GridView1.DataBind();
            GridView1.Visible = true;
        }
        catch
        {

        }
        }
        else
        {

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}