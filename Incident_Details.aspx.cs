using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incident_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String name;
        String Incident = Request.QueryString["ID"];
        TableRow tRow;
        TableCell tCell;
        
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT[INC Incident Number],[INC Priority],[INC Status],[INC Tier 2],[INC Tier 3],[AG Assignee],[INC CI Corporate ID] ,[INC CI Entity] ,[INC CI Site] ,[INC CI Site Group]  ,[INC CI Region],[AG Assignee Manager Name],[AG Assigned Group Name],[AG M Email Address] ,[INC DS Submit Date] ,[INC DS Last Modified By Full Name],[INC DS Last Modified Date],[INC DS Last Resolved Date],[INC DS Submitter Full Name],[INC RES Resolution],[AG Assignee Email Address],[INC CI Email Address],[RG Resolved By],[RG Resolved Group Name] ,[INC DS Closed Date],[INC Summary],[Submit_Date],[Expedite_By],[Expedite_Date],[Urgency_Reason],[Comment] FROM [Expedite].[dbo].['All_Incidents'],[dbo].[Expedite_time] WHERE [INC Incident Number] = '" + Incident + "' and [Incident_ID]= '" + Incident + "' ;";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
           string[] columnNames = dt.Columns.Cast<DataColumn>()
                            .Select(x => x.ColumnName)
                         .ToArray();

           for (int i = 0; i < columnNames.Length; i++)
           { 
            tRow = new TableRow();
            Table1.Rows.Add(tRow);
            tCell = new TableCell();
            tCell.Text = columnNames[i];
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = dt.Rows[0][i].ToString();
            tRow.Cells.Add(tCell);
            
            }
           
             
            
           
            
            }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

         }
}