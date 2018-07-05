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

public partial class Sita : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        try
        {

            ArrayList AllSitaIncidents = new ArrayList();
            ArrayList AllExpoxiedIncidents = new ArrayList();
            ArrayList SitaExpoditeIncidents = new ArrayList();
            DataTable DtOfSitaIncidents = new DataTable();
            DataTable dtOfAllIncidents = new DataTable();
            DataTable DtSitaExpoditeIncidents = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            SqlCommand command2 = new SqlCommand();
            command.Connection = conn;
            //Get All Incidents in Sita
            command.CommandText = "Select [Inc Incident Number] From [dbo].['All_Incidents'] Where [INC CI Corporate ID] IN ('KXZP1876','TVDB2230');";
            command2.Connection = conn;
            command2.CommandText = "Select [Incident_ID] From [dbo].[Expedite_time];";



            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;

                using (DtOfSitaIncidents = new DataTable())
                {

                    sda.Fill(DtOfSitaIncidents);


                }
            }
            foreach (DataRow row in DtOfSitaIncidents.Rows)
            {
                foreach (DataColumn column in DtOfSitaIncidents.Columns)
                {
                    AllSitaIncidents.Add(row[column]);
                    

                }
            }


            using (SqlDataAdapter sda1 = new SqlDataAdapter())
            {
                sda1.SelectCommand = command2;

                using (dtOfAllIncidents = new DataTable())
                {

                    sda1.Fill(dtOfAllIncidents);
                    

                }
            }


            foreach (DataRow row in dtOfAllIncidents.Rows)
            {
                foreach (DataColumn column in dtOfAllIncidents.Columns)
                {
                    AllExpoxiedIncidents.Add(row[column]);
                                 
                }
            }

            for (int i = 0; i < AllExpoxiedIncidents.Count; i++)
            {
                for (int j = 0; j < AllSitaIncidents.Count; j++)
                {
                    if (AllExpoxiedIncidents[i].Equals(AllSitaIncidents[j]))
                    {
                        SitaExpoditeIncidents.Add(AllExpoxiedIncidents[i]);
                        Debug.Write(AllExpoxiedIncidents[i]);

                    }

                }

            }

            SqlCommand command3 = new SqlCommand();
            for (int counter = 0; counter < SitaExpoditeIncidents.Count; counter++)
            {
                command3.Connection = conn;
                command3.CommandText = "Select [Incident_ID],[Submit_Date],[Expedite_By],[Expedite_Date],[Urgency_Reason] From [dbo].[Expedite_time] Where [Incident_ID] ='" + SitaExpoditeIncidents[counter] + "' ; ";

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command3;
                sda.Fill(DtSitaExpoditeIncidents);
            }

            if (DtSitaExpoditeIncidents.Rows.Count <= 0)
            {
                Label1.Text = "No Incidents exist";

            }
            else
            {
                Label1.Visible = false;
                GridView1.DataSource = DtSitaExpoditeIncidents;
                GridView1.DataBind();
                GridView1.Visible = true;


            }

        }

        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }
}
