using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Expedite_Extraction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        { 
            Response.Redirect("Default.aspx"); 
        }
        Label2.Visible = false;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        DateTime DateFrom = Calendar1.SelectedDate;
        DateTime DateTo = Calendar2.SelectedDate;
        if (DateTo > DateFrom)
        {
            String DateFromString = Calendar1.SelectedDate.ToString();
            String DateToString = Calendar2.SelectedDate.ToString();
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
            conn.Open();
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [Incident_ID] as 'Incident ID',[Submit_Date] as 'Submit Date',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason',[Comment] as 'Comment' From [dbo].[Expedite_time] where [Submit_Date] Between '" + DateFromString + "'AND'" + DateToString + "';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
           

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;

                using (dt = new DataTable())
                {

                    sda.Fill(dt);


                }
                if (dt.Rows.Count == 0)
                {
                    Label2.Visible = true;
                    Label2.Text = "No Incidents Available In This Range";
                }
                else { 
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                }
            }
        }
        else {
            Label2.Visible = true;
            Label2.Text="Wrong Selected Range";
        }

    }
 
}