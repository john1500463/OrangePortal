using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Expedite_Extraction : System.Web.UI.Page
{
    public static DataTable thedatatable;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        { 
            Response.Redirect("Default.aspx"); 
        }
        Label2.Visible = false;
        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
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
            command.CommandText = "SELECT [Incident_ID] as 'Incident ID', [INC Tier 2] as 'Tier 2',[INC Tier 3] as 'Tier 3',[INC Priority] as 'Priority',[INC Status] as 'Status',[Urgency_Reason] as 'Urgency Reason',[INC DS Submitter Full Name] as 'Submitter',[RG Resolved By]as 'Resolved By' ,[RG Resolved Group Name] as 'Resolver Group', [INC DS Submit Date] as 'Submit Date',[dbo].['All_Incidents'].[Expedite Date] as 'Expedite Date', [INC DS Last Resolved Date] as 'Resolved Date',[INC DS Closed Date] as 'Closed Date', [MTTR] as 'MTTR',[MTTR_From_ExpediteDate] as 'MTTR From Expedite Date' FROM [Expedite].[dbo].[Expedite_time]INNER JOIN [Expedite].[dbo].['All_Incidents'] ON [Expedite].[dbo].[Expedite_time].[Incident_ID] = [Expedite].[dbo].['All_Incidents'].[INC Incident Number] where convert(date,[Expedite_Date])>='" + DateFromString + "'AND convert(date,[Expedite_Date])<='" + DateToString + "';";
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
                    Button_Export.Visible = false;
                }
                else {
                thedatatable = dt.Copy();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                clickable_incidents();
                Button_Export.Visible = true;
                }
            }
        }
        else {
            Label2.Visible = true;
            Label2.Text="Wrong Selected Range";
            Button_Export.Visible = false;
        }

    }
    protected void exporttoxls(DataTable dt)
    {

        //Create a dummy GridView and Bind the data source we have.
        GridView grdExportData = new GridView();

        grdExportData.AllowPaging = false;
        grdExportData.DataSource = dt;
        grdExportData.DataBind();

        //Clear the response and add the content types and headers to it.
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MyReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        //We need this string writer and HTML writer in order to render the grid inside it.
        StringWriter swExportData = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(swExportData);

        //Lets render the Grid inside the HtmlWriter and then automatically we will have it converted into eauivalent string.
        grdExportData.RenderControl(hw);

        //Write the response now and you will get your excel sheet as download file
        Response.Output.Write(swExportData.ToString());
        Response.Flush();
        Response.End();
    }

    protected void Exportxls_Click(object sender, EventArgs e)
    {
        exporttoxls(thedatatable);
    }
    protected void clickable_incidents()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HyperLink hlContro = new HyperLink();
            String Incident = GridView1.Rows[i].Cells[0].Text;
            hlContro.NavigateUrl = "./Incident_Details.aspx?ID=" + Incident;
            hlContro.Text = GridView1.Rows[i].Cells[0].Text;
            GridView1.Rows[i].Cells[0].Controls.Add(hlContro);
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Expedite_Extraction.aspx");
    }
    string GetLastModifiedDate()
    {
        return System.IO.File.GetLastWriteTime("D:/Expedite/NewExpedite.xls").ToString();
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