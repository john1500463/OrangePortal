using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;

using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


public partial class Expedited_Incidents_Support : System.Web.UI.Page
{
    // THE SELECT YOU'LL NEED
    //Select [INC Incident Number],[INC Tier 2],[INC Status], [AG Assigned Group Name], [AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason] From [Expedite].[dbo].['All_Incidents'] as AL INNER JOIN [Expedite].[dbo].[Expedite_time] as ET ON AL.[INC Incident Number] =  ET.[Incident_ID];
    public static DataTable thetable;
    DataTable dt;
    String Inc1;
    String Inc2;
    protected static String Alaa;
    DataTable dt1;
    DataTable dt4;
    SqlCommand command;
    int num;
    int Counter = 0;
    System.Web.UI.WebControls.Button Esclate1;
    System.Web.UI.WebControls.Label Label7;
    System.Web.UI.WebControls.CheckBox Checkbox;
    public static ArrayList getsgroups;

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
    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Alaa != null && !Page.IsPostBack)
        {
            Alaa = null;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            // Response.Redirect("Default.aspx");
        }
        Debug.WriteLine("Page Load : " + Alaa);
        Debug.WriteLine("chosen is " + DropDownList1.SelectedValue);
        getsgroups = get_groups_of_user((string)(Session["FTID"]));
        if (Alaa == "Calendar1")
        {
            String SelectedData = Calendar1.SelectedDate.ToShortDateString();
            string startdated = (Convert.ToDateTime(SelectedData)).ToString("yyyy/MM/dd");
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

            dt = new DataTable();
            conn.Open();
            command = new SqlCommand();
            command.Connection = conn;
            String cmdstrigg;
            if (getsgroups.Count != 0)
            {
                cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=(case when ([INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "' and [AG Assigned Group Name] = '" + getsgroups[0] + "'";
                foreach (String grp in getsgroups)
                {
                    cmdstrigg += "or [AG Assigned Group Name] = '" + grp + "' ";
                }
                cmdstrigg += ") then B.[INC Incident Number] END) ORDER BY " + DropDownList1.SelectedValue;
            }
            else
            {
                cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "' ORDER BY " + DropDownList1.SelectedValue;
                //cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [Incident_ID]='Not Exsist'";
            }
            command.CommandText = cmdstrigg;
            Debug.WriteLine(command.CommandText);
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Submit Date]) <='" + startdated + "';";
            //command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            thetable = dt.Copy();
            ButtonsAndCheckBoxes(dt, conn, "Calendar1");
            conn.Close();
        }
        if (Alaa == "Calendar2")
        {
            String SelectedData2 = Calendar2.SelectedDate.ToShortDateString();
            string startdated2 = (Convert.ToDateTime(SelectedData2)).ToString("yyyy/MM/dd");
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

            dt = new DataTable();
            conn.Open();
            command = new SqlCommand();
            command.Connection = conn;
            String cmdstrigg;
            if (getsgroups.Count != 0)
            {
                cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=(case when ([INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "' and [AG Assigned Group Name] = '" + getsgroups[0] + "'";
                foreach (String grp in getsgroups)
                {
                    cmdstrigg += "or [AG Assigned Group Name] = '" + grp + "' ";
                }
                cmdstrigg += ") then B.[INC Incident Number] END) ORDER BY " + DropDownList1.SelectedValue;
            }
            else
            {
                cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "' ORDER BY " + DropDownList1.SelectedValue;
                //cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [Incident_ID]='Not Exsist'";
            }
            command.CommandText = cmdstrigg;
            Debug.WriteLine(command.CommandText);
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            //command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            thetable = dt.Copy();
            ButtonsAndCheckBoxes(dt, conn, "Calendar2");
            conn.Close();
        }
        if (Alaa == null)
        {
                String x = (string)(Session["FTID"]);
                SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
                conn.Open();
                command = new SqlCommand();
                command.Connection = conn;
                String cmdstrigg;
                if (getsgroups.Count != 0)
                {
                    cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=(case when ([INC Status]!='Closed' and [INC Status]!='Resolved' and [AG Assigned Group Name] = '" + getsgroups[0] + "'";
                    foreach (String grp in getsgroups)
                    {
                        cmdstrigg += "or [AG Assigned Group Name] = '" + grp + "' ";
                    }
                    cmdstrigg += ") then B.[INC Incident Number] END) ORDER BY " + DropDownList1.SelectedValue;
                }
                else
                {
                    cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [INC Status]!='Closed' and [INC Status]!='Resolved' ORDER BY " + DropDownList1.SelectedValue;
                    //cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [Incident_ID]='Not Exsist'";
                }
                command.CommandText = cmdstrigg;
                Debug.WriteLine(command.CommandText);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = command;
                    using (dt = new DataTable())
                    {

                        sda.Fill(dt);

                    }
                }
                if (getsgroups.Count == 0)
                {
                    dt.Clear();
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                clickable_incidents();

            if (!Page.IsPostBack)
            {
                thetable = new DataTable();
                thetable = dt.Copy();
            }
            ButtonsAndCheckBoxes(dt, conn, null);
            GridView1.Visible = true;
            conn.Close();
        }
        if (GridView1.Rows.Count == 0 || getsgroups.Count == 0)
        {
            Button3.Visible = false;
            Label_Error.Visible = true;
        }
        else
        {
            Button3.Visible = true;
            Label_Error.Visible = false;
        }
        
    }

    protected void Calendar1_SelectionChanged(object sender, System.EventArgs e)
    {
        Calendar2.SelectedDates.Clear();
        String SelectedData = Calendar1.SelectedDate.ToShortDateString();
        string startdated = (Convert.ToDateTime(SelectedData)).ToString("yyyy/MM/dd");
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        dt = new DataTable();
        conn.Open();
        command = new SqlCommand();
        command.Connection = conn;
        String cmdstrigg;
        if (getsgroups.Count != 0)
        {
            cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=(case when ([INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "' and [AG Assigned Group Name] = '" + getsgroups[0] + "'";
            foreach (String grp in getsgroups)
            {
                cmdstrigg += "or [AG Assigned Group Name] = '" + grp + "' ";
            }
            cmdstrigg += ") then B.[INC Incident Number] END) ORDER BY " + DropDownList1.SelectedValue;
        }
        else
        {
            cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "' ORDER BY " + DropDownList1.SelectedValue;
            //cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [Incident_ID]='Not Exsist'";
        }
        command.CommandText = cmdstrigg;
        Debug.WriteLine(command.CommandText);
        //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Submit Date]) <='" + startdated + "';";
        //command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "';";
        //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }
        }
        thetable = dt.Copy();
        ButtonsAndCheckBoxes(dt, conn, "Calendar1");
        if (GridView1.Rows.Count == 0 || getsgroups.Count==0)
        {
            Button3.Visible = false;
            Label_Error.Visible = true;
        }
        else
        {
            Button3.Visible = true;
            Label_Error.Visible = false;
        }
        conn.Close();
        Calendar1.Visible = false;
        Date1view.ForeColor = Color.Black;
        Date1view.Text = Calendar1.SelectedDate.Day + "/" + Calendar1.SelectedDate.Month + "/" + Calendar1.SelectedDate.Year;
        Date1view.Visible = true;
        Date2view.Text = "-";
        Date2view.ForeColor = Color.White;
        Date2view.Visible = true;
    }
    protected void Calendar2_SelectionChanged(object sender, System.EventArgs e)
    {
        Calendar1.SelectedDates.Clear();
        String SelectedData2 = Calendar2.SelectedDate.ToShortDateString();
        string startdated2 = (Convert.ToDateTime(SelectedData2)).ToString("yyyy/MM/dd");
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        dt = new DataTable();
        conn.Open();
        command = new SqlCommand();
        command.Connection = conn;
        String cmdstrigg;
        if (getsgroups.Count != 0)
        {
            cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=(case when ([INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "' and [AG Assigned Group Name] = '" + getsgroups[0] + "'";
            foreach (String grp in getsgroups)
            {
                cmdstrigg += "or [AG Assigned Group Name] = '" + grp + "' ";
            }
            cmdstrigg += ") then B.[INC Incident Number] END) ORDER BY " + DropDownList1.SelectedValue;
        }
        else
        {
            cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "' ORDER BY " + DropDownList1.SelectedValue;
            //cmdstrigg = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] where [Incident_ID]='Not Exsist'";
        }
        command.CommandText = cmdstrigg;
        Debug.WriteLine(command.CommandText);
        //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
        //command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
        //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }
        }
        thetable = dt.Copy();
        ButtonsAndCheckBoxes(dt, conn, "Calendar2");
        if (GridView1.Rows.Count == 0 || getsgroups.Count == 0)
        {
            Button3.Visible = false;
            Label_Error.Visible = true;
        }
        else
        {
            Button3.Visible = true;
            Label_Error.Visible = false;
        }
        conn.Close();
        Calendar2.Visible = false;
        Date2view.ForeColor = Color.Black;
        Date2view.Text = Calendar2.SelectedDate.Day + "/" + Calendar2.SelectedDate.Month + "/" + Calendar2.SelectedDate.Year;
        Date2view.Visible = true;
        Date1view.Text = "-";
        Date1view.ForeColor = Color.White;
        Date1view.Visible = true;
    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        Alaa = null;
        Response.Redirect("Expedited_Incidents_Support.aspx");
    }

    protected void Button3_Click(object sender, System.EventArgs e)
    {
        //Debug.Write("When posting: " + GridView1.Rows[0].Cells[0].Text);
        //Debug.Write("When posting: " + thetable.Rows[0][0].ToString());
        exporttoxls(thetable);
    }

    ArrayList get_groups_of_user(String ftid)
    {
        ArrayList output = new ArrayList();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        DataTable dtgroups = new DataTable();
        conn.Open();
        command = new SqlCommand();
        command.Connection = conn;
        //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
        command.CommandText = "SELECT [GRP Group Name] FROM [Expedite].[dbo].[Staff] where [PE Login Name]='" + ftid + "';";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using (dtgroups = new DataTable())
            {

                sda.Fill(dtgroups);

            }
        }

        for (int i = 0; i < dtgroups.Rows.Count; i++)
        {
            output.Add(dtgroups.Rows[i][0]);
        }
        conn.Close();
        return output;
    }
    
    void EditingBtn_Click(Object sender,
                        EventArgs e)
    {
        // When the button is clicked,
        // change the button text, and disable it.

        System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
        int IncNum = Int32.Parse(clickedButton.ID);


        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            dt1 = new DataTable();
            conn.Open();
            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Insert into [Expedite].[dbo].[Esclation]([Incident Number],[DateEsclated]) Values ('" + dt.Rows[IncNum][0] + "',GETDATE())";

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }
            conn.Close();
            Response.Redirect("Expedited_Incidents.aspx");
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
    void ButtonsAndCheckBoxes(DataTable dt, SqlConnection conn, String Ahmed)
    {
        Alaa = Ahmed;
        Debug.WriteLine("Buttons and Checkboxes : " + Alaa);
        command = new SqlCommand();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        clickable_incidents();
        
    }
    protected void clickable_incidents()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HyperLink hlContro = new HyperLink();
            String Incident = GridView1.Rows[i].Cells[0].Text;
            hlContro.NavigateUrl = "./Incident_Details_Support.aspx?ID=" + Incident;
            hlContro.Text = GridView1.Rows[i].Cells[0].Text;
            GridView1.Rows[i].Cells[0].Controls.Add(hlContro);
        }

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
}