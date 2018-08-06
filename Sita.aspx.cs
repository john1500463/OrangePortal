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
using System.IO;
using System.Drawing;

public partial class Sita : System.Web.UI.Page
{
    public static DataTable thedatatable;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
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
            command.CommandText = "Select [Inc Incident Number] From [dbo].['All_Incidents'] Where [INC CI Corporate ID] IN ('NGSL4427','TVDB2230');";
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
                command3.CommandText = "Select [Incident_ID] as 'Incident ID',[Submit_Date] as 'Submit Date',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedited Date',[Urgency_Reason] as 'Urgency Reason' From [dbo].[Expedite_time] Where [Incident_ID] ='" + SitaExpoditeIncidents[counter] + "' ; ";

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command3;
                sda.Fill(DtSitaExpoditeIncidents);
            }

            if (DtSitaExpoditeIncidents.Rows.Count <= 0)
            {
                Label2.Text = "No SITA Incidents exist";
                Button_Export.Visible = false;

            }
            else
            {
                Label2.Visible = false;
                DtSitaExpoditeIncidents.DefaultView.Sort = DropDownList1.SelectedItem.Text;
                thedatatable = DtSitaExpoditeIncidents.Copy();
                GridView1.DataSource = DtSitaExpoditeIncidents;
                GridView1.DataBind();
                GridView1.Visible = true;
                clickable_incidents();
                Button_Export.Visible = true;
            }

        }

        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        DateTime DateFrom = Calendar1.SelectedDate;
        DateTime DateTo = Calendar2.SelectedDate;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        if (DateTo > DateFrom)
        {
            try{
            String DateFromString = Calendar1.SelectedDate.ToString();
            String DateToString = Calendar2.SelectedDate.ToString();
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
            command.CommandText = "Select [Inc Incident Number] From [dbo].['All_Incidents'] Where [INC CI Corporate ID] IN ('NGSL4427','TVDB2230') AND convert(date,[INC DS Submit Date])>='" + DateFromString + "'AND convert(date,[INC DS Submit Date])<='" + DateToString + "';";
            command2.Connection = conn;
            command2.CommandText = "Select [Incident_ID] From [dbo].[Expedite_time]";



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
                command3.CommandText = "Select [Incident_ID] as 'Incident ID',[Submit_Date] as 'Submit Date',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedited Date',[Urgency_Reason] as 'Urgency Reason' From [dbo].[Expedite_time] Where [Incident_ID] ='" + SitaExpoditeIncidents[counter] + "' ; ";

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command3;
                sda.Fill(DtSitaExpoditeIncidents);
            }
            if (DtSitaExpoditeIncidents.Rows.Count == 0)
                {
                    Label2.Visible = true;
                    Label2.Text = "No Incidents Available In This Range";
                    Button_Export.Visible = false;

                }
                else
                {
                    DtSitaExpoditeIncidents.DefaultView.Sort = DropDownList1.SelectedItem.Text;
                    thedatatable = DtSitaExpoditeIncidents.Copy();
                    Button_Export.Visible = true;
                    Label2.Visible = false;
                    GridView1.DataSource = DtSitaExpoditeIncidents;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    clickable_incidents();

                }

            }

            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "Wrong Selected Range";
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Sita.aspx");
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
    protected void calendar1info_Click(object sender, System.EventArgs e)
    {
        if (Calendar1.Visible != true)
        {
            Calendar1.Visible = true;
        }
        else
        {
            Calendar1.Visible = false;
        }
        if (Calendar2.Visible)
        {
            Calendar2.Visible = false;
        }

    }
    protected void calendar2info_Click(object sender, System.EventArgs e)
    {
        if (Calendar2.Visible != true)
        {
            Calendar2.Visible = true;
        }
        else
        {
            Calendar2.Visible = false;
        }
        if (Calendar1.Visible)
        {
            Calendar1.Visible = false;
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Calendar1.Visible = false;
        Date1view.ForeColor = Color.Black;
        Date1view.Text = Calendar1.SelectedDate.ToString().Substring(0, 10);
        Date1view.Visible = true;
        if (Date2view.Visible == false)
        {
            Date2view.Visible = true;
            Date2view.ForeColor = Color.White;
            Date2view.Text = "-";
        }
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Calendar2.Visible = false;
        Date2view.ForeColor = Color.Black;
        Date2view.Text = Calendar2.SelectedDate.ToString().Substring(0, 10);
        Date2view.Visible = true;
        if (Date1view.Visible == false)
        {
            Date1view.Visible = true;
            Date1view.ForeColor = Color.White;
            Date1view.Text = "-";
        }
    }
}
