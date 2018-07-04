using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Diagnostics;


public partial class CSM_entity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        try
        {

            ArrayList AllCSMIncidents = new ArrayList();
            ArrayList AllIncidents = new ArrayList();
            ArrayList CSMExpoditeIncidents = new ArrayList();
            DataTable DtOfCSMIncidents = new DataTable();
            DataTable dtOfAllIncidents = new DataTable();
            DataTable DtCSMExpoditeIncidents = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            SqlCommand command2 = new SqlCommand();
            command.Connection = conn;
            //Get All Indients with CSM 
            command.CommandText = "Select [INC Incident Number] From [dbo].['All_Incidents'] where [INC CI Entity] LIKE '%CSM%';";
            command2.Connection = conn;
            command2.CommandText = "Select [Incident_ID] From [dbo].[Expedite_time];";



            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;

                using (DtOfCSMIncidents = new DataTable())
                {

                    sda.Fill(DtOfCSMIncidents);


                }
            }
            foreach (DataRow row in DtOfCSMIncidents.Rows)
            {
                foreach (DataColumn column in DtOfCSMIncidents.Columns)
                {
                    AllCSMIncidents.Add(row[column]);

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
                    AllIncidents.Add(row[column]);
                }
            }

            for (int i = 0; i < AllIncidents.Count; i++)
            {
                for (int j = 0; j < AllCSMIncidents.Count; j++)
                {
                    if (AllIncidents[i].Equals(AllCSMIncidents[j]))
                    {
                        CSMExpoditeIncidents.Add(AllIncidents[i]);


                    }

                }

            }

            SqlCommand command3 = new SqlCommand();
            for (int counter = 0; counter < CSMExpoditeIncidents.Count; counter++)
            {
                command3.Connection = conn;
                command3.CommandText = "Select [Incident_ID],[Submit_Date],[Expedite_By],[Expedite_Date],[Urgency_Reason] From [dbo].[Expedite_time] Where [Incident_ID] ='" + CSMExpoditeIncidents[counter] + "' ;";

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command3;
                sda.Fill(DtCSMExpoditeIncidents);
            }
<<<<<<< HEAD
=======

>>>>>>> 68afd68c145b17050821eef556173335b2f9d3ec
            if (DtCSMExpoditeIncidents.Rows.Count == 0)
            {
                Label1.Text = "No Incidents exist";

            }
            else
            {
                Label1.Visible = false;
                GridView1.DataSource = DtCSMExpoditeIncidents;
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
