using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incident_Details : System.Web.UI.Page
{
    
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
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
            command.CommandText = "SELECT[INC Incident Number] as 'Incident Number',[INC Priority] as 'Priority',[INC Status] as 'Status',[INC Tier 2] as 'Application',[INC Tier 3] as 'Tier 3',[AG Assignee] as 'Assignee',[INC CI Corporate ID] as 'Corporate ID',[INC CI Entity] as 'Entity' ,[INC CI Site] as 'Site' ,[INC CI Site Group] as 'Site Group' ,[INC CI Region] as 'Region',[AG Assignee Manager Name] as 'Assignee Manager',[AG Assigned Group Name] as 'Assigned Group',[AG M Email Address] as 'AG M Email Address' ,[INC DS Submit Date] as 'Submit Date' ,[INC DS Last Modified By Full Name] as 'Last Modified By',[INC DS Last Modified Date] as 'Last Modified Date',[INC DS Last Resolved Date] as 'Last Resolved Date',[INC DS Submitter Full Name] as 'Submitter',[INC RES Resolution] as 'Resolution',[AG Assignee Email Address] as 'Assignee Email',[INC CI Email Address] as 'Submitter Email',[RG Resolved By] as 'Resolved By',[RG Resolved Group Name] 'Resolved Group' ,[INC DS Closed Date] as 'Closed Date',[INC Summary] as 'Summary',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason',[Comment] as Comment FROM [dbo].['All_Incidents'] FULL OUTER JOIN [dbo].[Expedite_time] ON [dbo].['All_Incidents'].[INC Incident Number] = [dbo].[Expedite_time].[Incident_ID] Where [INC Incident Number]='" + Incident + "' or [dbo].[Expedite_time].[Incident_ID]='" + Incident + "';";
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

           for (int i = 0; i < dt.Columns.Count; i++)
           { 
            tRow = new TableRow();
            Table1.Rows.Add(tRow);
            tCell = new TableCell();
            tCell.Text = columnNames[i];
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            if (i == 0) {
                tCell.Text = Incident;
                tRow.Cells.Add(tCell);
                continue;
                
            }
            if (dt.Rows[0][i].ToString() == "" || dt.Rows[0][i].ToString() == "nan")
            {
                tCell.Text = "None";
            }
            else
            {
                tCell.Text = dt.Rows[0][i].ToString();
            }
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