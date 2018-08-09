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


public partial class CSM_entity : System.Web.UI.Page
{
    public static DataTable thedatatable;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (((String)Session["Right"]) == "else")
        {
            Response.Redirect("Home_Page_User.aspx");
        }
        if (((String)Session["Right"]) == "S")
        {
            Response.Redirect("Home_Page_Support.aspx");
        }
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
                //command3.CommandText = "Select [Incident_ID] as 'Incident ID',[INC Tier 2] as 'Tier 2',[Submit_Date] as 'Submit Date',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [dbo].[Expedite_time] Where [Incident_ID] ='" + CSMExpoditeIncidents[counter] + "' ;";
                command3.CommandText = "Select [Incident_ID] as 'Incident ID',[INC Tier 2] as 'Tier 2',[Submit_Date] as 'Submit Date',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedited Date',[Urgency_Reason] as 'Urgency Reason' From [dbo].['All_Incidents'] ,[Expedite].[dbo].[Expedite_time] where [INC Incident Number] = '" + CSMExpoditeIncidents[counter] + "' and [Expedite].[dbo].[Expedite_time].[Incident_ID] = '" + CSMExpoditeIncidents[counter] + "';";
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command3;
                sda.Fill(DtCSMExpoditeIncidents);
            }
            if (DtCSMExpoditeIncidents.Rows.Count == 0)
            {
                Label2.Text = "No CSM Incidents exist";
                Button_Export.Visible = false;

            }
            else
            {
                Label2.Visible = false;
                DtCSMExpoditeIncidents.DefaultView.Sort = DropDownList1.SelectedItem.Text;
                thedatatable = DtCSMExpoditeIncidents.Copy();
                GridView1.DataSource = DtCSMExpoditeIncidents;
                GridView1.DataBind();
                GridView1.Visible = true;
                clickable_incidents();
                Button_Export.Visible = true;
            }
            conn.Close();
        }

        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

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
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

            try
            {

                String DateFromString = Calendar1.SelectedDate.ToString();
                String DateToString = Calendar2.SelectedDate.ToString();
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
                command.CommandText = "Select [INC Incident Number] From [dbo].['All_Incidents'] where [INC CI Entity] LIKE '%CSM%' AND convert(date,[INC DS Submit Date])>='" + DateFromString + "'AND convert(date,[INC DS Submit Date])<='" + DateToString + "';";
                command2.Connection = conn;
                command2.CommandText = "Select [Incident_ID] From [dbo].[Expedite_time]";
                Debug.WriteLine(command2.CommandText);
                Debug.WriteLine(command2.CommandText);


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
                    //command3.CommandText = "Select [Incident_ID] as 'Incident ID',[Submit_Date] as 'Submit Date',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [dbo].[Expedite_time] Where [Incident_ID] ='" + CSMExpoditeIncidents[counter] + "' ;";
                    command3.CommandText = "Select [Incident_ID] as 'Incident ID',[INC Tier 2] as 'Tier 2',[Submit_Date] as 'Submit Date',[Expedite_By] as 'Expedited By',[Expedite_Date] as 'Expedited Date',[Urgency_Reason] as 'Urgency Reason' From [dbo].['All_Incidents'] ,[Expedite].[dbo].[Expedite_time] where [INC Incident Number] = '" + CSMExpoditeIncidents[counter] + "' and [Expedite].[dbo].[Expedite_time].[Incident_ID] = '" + CSMExpoditeIncidents[counter] + "';";
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command3;
                    sda.Fill(DtCSMExpoditeIncidents);
                }
                if (DtCSMExpoditeIncidents.Rows.Count == 0)
                {
                    Label2.Visible = true;
                    Label2.Text = "No Incidents Available In This Range";
                    Button_Export.Visible = false;

                }
                else
                {

                    DtCSMExpoditeIncidents.DefaultView.Sort = DropDownList1.SelectedItem.Text;
                    thedatatable = DtCSMExpoditeIncidents.Copy();
                    Button_Export.Visible = true;
                    Label2.Visible = false;
                    GridView1.DataSource = DtCSMExpoditeIncidents;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    clickable_incidents();

                }
                conn.Close();
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
        Response.Redirect("CSM_entity.aspx");
    }

    protected void clickable_incidents()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HyperLink hlContro = new HyperLink();
            String Incident = GridView1.Rows[i].Cells[0].Text;
            hlContro.NavigateUrl = String.Format("javascript:void(window.open('" + "./Incident_Details.aspx?ID=" + Incident + "','_blank'));");
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
        SqlCommand command = new SqlCommand();
        DataTable dt = new DataTable();
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
        conn.Close();
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
        Date1view.Text = Calendar1.SelectedDate.Day + "/" + Calendar1.SelectedDate.Month + "/" + Calendar1.SelectedDate.Year;
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
        Date2view.Text = Calendar2.SelectedDate.Day + "/" + Calendar2.SelectedDate.Month + "/" + Calendar2.SelectedDate.Year;
        Date2view.Visible = true;
        if (Date1view.Visible == false)
        {
            Date1view.Visible = true;
            Date1view.ForeColor = Color.White;
            Date1view.Text = "-";
        }
    }
}
