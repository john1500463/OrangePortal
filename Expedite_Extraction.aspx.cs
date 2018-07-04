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

    }



    protected void Button1_Click(object sender, EventArgs e)
    {
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
            command.CommandText = "Select * From [dbo].[Expedite_time] where [Submit_Date] Between '" + DateFromString + "'AND'"+ DateToString+"';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            Debug.Write(command.CommandText);

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;

                using (dt = new DataTable())
                {

                    sda.Fill(dt);


                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

    }
 
}