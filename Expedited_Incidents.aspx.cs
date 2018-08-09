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

public partial class Expedited_Incidents : System.Web.UI.Page
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
        if (((String)Session["Right"]) == "else")
        {
            Response.Redirect("Home_Page_User.aspx");
        }
        if (((String)Session["Right"]) == "S")
        {
            Response.Redirect("Home_Page_Support.aspx");
        }
        Debug.WriteLine("Page Load : "+Alaa);
        if (Alaa == "Search") {
            String ManagerName = TextBox1.Text.ToString();
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
            dt = new DataTable();
            conn.Open();
             command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and [AG Assignee Manager Name]='" + ManagerName + "' ORDER BY " + DropDownList1.SelectedValue;
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
            Alaa = "Search";
            ButtonsAndCheckBoxes(dt, conn,"Search");
            conn.Close();
            //Alaa = null;
        }
        if (Alaa == "Calendar1")
        {
            String SelectedData = Calendar1.SelectedDate.ToShortDateString();
            string startdated = (Convert.ToDateTime(SelectedData)).ToString("yyyy/MM/dd");
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

            dt = new DataTable();
            conn.Open();
            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Submit Date]) <='" + startdated + "';";
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "' ORDER BY " + DropDownList1.SelectedValue;
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
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
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
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
            String x = (string)(Session["FTID"]);

            conn.Open();
             command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' ORDER BY " + DropDownList1.SelectedValue;
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
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
            Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
            Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
        }

        if (GridView1.Rows.Count == 0)
        {
            Button3.Visible = false;
            Button4.Visible = false;
            Label_Error.Text = "No Incidents Exist";
            Label_Error.Visible = true;
        }
        else
        {
            Button3.Visible = true;
            Button4.Visible = true;
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
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Submit Date]) <='" + startdated + "';";
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, A.[Expedite_Date]) <='" + startdated + "' ORDER BY " + DropDownList1.SelectedValue;
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
            ButtonsAndCheckBoxes(dt, conn,"Calendar1");
            if (GridView1.Rows.Count == 0)
            {
                Button3.Visible = false;
                Button4.Visible = false;
                Label_Error.Text = "No Incidents Exist";
                Label_Error.Visible = true;
            }
            else
            {
                Button3.Visible = true;
                Button4.Visible = true;
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
        string startdated2= (Convert.ToDateTime(SelectedData2)).ToString("yyyy/MM/dd");
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
  
            dt = new DataTable();
            conn.Open();
             command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "' ORDER BY " + DropDownList1.SelectedValue;
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
            if (GridView1.Rows.Count == 0)
            {
                Button3.Visible = false;
                Button4.Visible = false;
                Label_Error.Text = "No Incidents Exist";
                Label_Error.Visible = true;
            }
            else
            {
                Button3.Visible = true;
                Button4.Visible = true;
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
    
    protected void TextBox1_TextChanged(object sender, System.EventArgs e)
    {

    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        
        TextBox1.Text = "";
        Alaa = null;
        Response.Redirect("Expedited_Incidents.aspx");
    }

    protected void Button3_Click(object sender, System.EventArgs e)
    {
        //Debug.Write("When posting: " + GridView1.Rows[0].Cells[0].Text);
        //Debug.Write("When posting: " + thetable.Rows[0][0].ToString());
        exporttoxls(thetable);
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        String ManagerName = TextBox1.Text.ToString();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
             dt = new DataTable();
            conn.Open();
             command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',A.[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and [AG Assignee Manager Name]='" + ManagerName + "' ORDER BY " + DropDownList1.SelectedValue;
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
            Alaa = "Search";
            ButtonsAndCheckBoxes(dt, conn,"Search");
            if (GridView1.Rows.Count == 0)
            {
                Button3.Visible = false;
                Button4.Visible = false;
                if (String.IsNullOrWhiteSpace(ManagerName) || String.IsNullOrEmpty(ManagerName))
                {
                    Label_Error.Text = "Please enter a manager name!";
                }
                else
                {
                    Label_Error.Text = "No Incidents Exist";
                }
                Label_Error.Visible = true;
            }
            else
            {
                Button3.Visible = true;
                Button4.Visible = true;
                Label_Error.Visible = false;
            }
            Calendar1.Visible = false;
            Calendar2.Visible = false;
            Date1view.Visible = false;
            Date2view.Visible = false;
            conn.Close();
    }

   
    void EditingBtn_Click(Object sender,
                        EventArgs e)
    {
        // When the button is clicked,
        // change the button text, and disable it.

     System.Web.UI.WebControls.Button clickedButton = ( System.Web.UI.WebControls.Button)sender;
         int IncNum = Int32.Parse(clickedButton.ID);
        
        
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
             
            conn.Open();
             command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Insert into [Expedite].[dbo].[Esclation]([Incident Number],[DateEsclated]) Values ('" + dt.Rows[IncNum][0] + "',GETDATE())";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt1 = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }
            esclate_to_manager(dt.Rows[IncNum][0].ToString());
            conn.Close();
            Response.Redirect("Expedited_Incidents.aspx");
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
    protected void esclate_to_manager(String Inc_id)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        String group_name = "";
        String assignee_name = "";
        String manager_mail = "";
        String expeditedby_mail = "";
        String assigned_group = "";
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = conn;
            command2.CommandText = "SELECT [AG Assigned Group Name],[AG M Email Address],[INC CI Email Address],[Expedited_mail],[AG Assigned Group Name] FROM [Expedite].[dbo].['All_Incidents'],[Expedite].[dbo].[Expedite_time] where [INC Incident Number]='" + Inc_id + "';";
            using (SqlDataAdapter sda2 = new SqlDataAdapter())
            {
                sda2.SelectCommand = command2;
                using (dt = new DataTable())
                {

                    sda2.Fill(dt);

                }
            }
            group_name = dt.Rows[0][0].ToString();
            assignee_name = dt.Rows[0][1].ToString();
            manager_mail = dt.Rows[0][2].ToString();
            expeditedby_mail = dt.Rows[0][3].ToString();
            assigned_group = dt.Rows[0][4].ToString();

            DataTable dtb = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [GRP M Full Name (mail)] FROM [Expedite].[dbo].[Manger_Mail] Where [GRP Group Name]='" + assigned_group + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dtb = new DataTable())
                {

                    sda.Fill(dtb);

                }
            }
            if (dtb.Rows.Count != 0)
            {
                manager_mail = dtb.Rows[0][0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        Debug.WriteLine("manager to esclate is " + manager_mail);
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("mx-us.equant.com");
        mail.From = new MailAddress("it.support4business@orange.com");
        mail.To.Add(manager_mail);
        ArrayList teamlist = getteamlist(assigned_group);
        foreach (String team_member in teamlist)
        {
            mail.CC.Add(team_member);
        }
        mail.CC.Add("it.support4business@orange.com");
        mail.Body = "Hello Manager";
        mail.Subject = "Incident " + Inc_id + " Expedited";
        SmtpServer.Send(mail);
    }
    protected ArrayList getteamlist(String group_name)
    {
        ArrayList outputeamlist = new ArrayList();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [mail] FROM [Expedite].[dbo].[Group_Mail] Where [GRP Group Name]='" + group_name + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            if (dt.Rows.Count != 0)
            {
                outputeamlist.Add(dt.Rows[0][0].ToString());
                conn.Close();
                return outputeamlist;
            }
            else
            {
                DataTable dt2;
                SqlCommand command2 = new SqlCommand();
                command2.Connection = conn;
                command2.CommandText = "SELECT [PE Email] FROM [Expedite].[dbo].[Staff] Where [GRP Group Name]='" + group_name + "';";
                using (SqlDataAdapter sda2 = new SqlDataAdapter())
                {
                    sda2.SelectCommand = command2;
                    using (dt2 = new DataTable())
                    {

                        sda2.Fill(dt2);

                    }
                }

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    outputeamlist.Add(dt2.Rows[i][0].ToString());
                }
                conn.Close();
                return outputeamlist;
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        return outputeamlist;
    }


    void CheckBoxClicked(Object sender, EventArgs e)
    {
        Debug.WriteLine("Checkbox interaction");
        System.Web.UI.WebControls.CheckBox Checkbox = (System.Web.UI.WebControls.CheckBox)sender;
        Debug.WriteLine("Ahmed");
        int IncNum = Int32.Parse(Checkbox.ID);
        IncNum = IncNum - num - num - num;
        Debug.Write(IncNum);
        String incName = dt.Rows[IncNum][0].ToString();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

         dt1 = new DataTable();
        conn.Open();
         command = new SqlCommand();
        command.Connection = conn;
        command.CommandText = "Update [Expedite].[dbo].[Expedite_time] Set [Acknowledge]='true' where [Incident_ID]='" + incName + "';";

        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using ( dt4 = new DataTable())
            {

                sda.Fill(dt4);
                Debug.WriteLine(command.CommandText);
            }
        }
        conn.Close();
        Response.Redirect("Expedited_Incidents.aspx");
    }
    


    protected void notifymanagers() {
        ArrayList all_managers_emails = new ArrayList();
        ArrayList all_inc = new ArrayList();
        ArrayList unique_emails = new ArrayList();
        ArrayList idstoextract = new ArrayList();
        for (int i = 0; i < thetable.Rows.Count; i++)
        {
            idstoextract.Add(thetable.Rows[i][0].ToString());
        }

        foreach (String idd in idstoextract)
        {
            Debug.WriteLine(idd);
        }
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
             dt = new DataTable();
            conn.Open();
             command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [Incident_ID],[AG M Email Address] FROM [Expedite].[dbo].['All_Incidents'],[Expedite].[dbo].[Expedite_time] where [Incident_ID]=[INC Incident Number]";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(idstoextract.Contains(dt.Rows[i][0]))
                {
                    all_managers_emails.Add(getmanagerofinc(dt.Rows[i][0].ToString()));
                all_inc.Add(dt.Rows[i][0]);
                }
            }

            String temp = all_managers_emails[0].ToString();
            unique_emails.Add(temp);
            foreach (String mail in all_managers_emails)
            {
                if (!unique_emails.Contains(mail))
                {
                    unique_emails.Add(mail);
                }
            }

            foreach (String email in unique_emails)
            {
                ArrayList alltheinc = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (getmanagerofinc(((String)(dt.Rows[i][0]))) == email)
                    {
                        if (idstoextract.Contains(dt.Rows[i][0]))
                        {
                            alltheinc.Add(dt.Rows[i][0]);
                        }
                    }
                }
                sendmailtomanager(email,alltheinc);
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        conn.Close();
    }
    protected String getmanagerofinc(String incid)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        String group_name = "";
        String assignee_name = "";
        String manager_mail = "";
        String expeditedby_mail = "";
        String assigned_group = "";
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = conn;
            command2.CommandText = "SELECT [AG Assigned Group Name],[AG M Email Address],[INC CI Email Address],[Expedited_mail],[AG Assigned Group Name] FROM [Expedite].[dbo].['All_Incidents'],[Expedite].[dbo].[Expedite_time] where [INC Incident Number]='" + incid + "';";
            using (SqlDataAdapter sda2 = new SqlDataAdapter())
            {
                sda2.SelectCommand = command2;
                using (dt = new DataTable())
                {

                    sda2.Fill(dt);

                }
            }
            group_name = dt.Rows[0][0].ToString();
            assignee_name = dt.Rows[0][1].ToString();
            manager_mail = dt.Rows[0][2].ToString();
            expeditedby_mail = dt.Rows[0][3].ToString();
            assigned_group = dt.Rows[0][4].ToString();

            DataTable dtb = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [GRP M Full Name (mail)] FROM [Expedite].[dbo].[Manger_Mail] Where [GRP Group Name]='" + assigned_group + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dtb = new DataTable())
                {

                    sda.Fill(dtb);

                }
            }
            if (dtb.Rows.Count != 0)
            {
                manager_mail = dtb.Rows[0][0].ToString();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        return manager_mail;
    }

    protected void sendmailtomanager(String manager_email, ArrayList IncidentsIDs)
    {
        String body = "The following Incidents are expedited: </br> <style>table, th, td {border: 1px solid black; border-collapse: collapse;}</style> <table>";
        body += "<tr> <th>Incident ID</th> </tr>";
        foreach (String id in IncidentsIDs)
        {
            body +="<tr> <td> " + id + "</td> </tr>";
        }
        body+="</table>";
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("mx-us.equant.com");
        mail.From = new MailAddress("it.support4business@orange.com");
        mail.To.Add(manager_email);
        //mail.To.Add("waleed.mohamed@orange.com");
        mail.Body = body;
        mail.Subject = "Expedtied Incidents";
        mail.IsBodyHtml = true;
        mail.CC.Add("it.support4business@orange.com");
        SmtpServer.Send(mail);
        //Debug.Write(body + " EMAIL IS " + manager_email);
    }
    protected void Notify_Click(object sender, System.EventArgs e)
    {
        notifymanagers();
    }

    void ButtonsAndCheckBoxes(DataTable dt ,SqlConnection conn, String Ahmed)
    {
            Alaa = Ahmed;
            Debug.WriteLine("Buttons and Checkboxes : " + Alaa);
             command= new SqlCommand();
            dt.Columns.Add(new DataColumn("Esclate", typeof(string)));
            dt.Columns.Add(new DataColumn("Acknowledge", typeof(string)));
            
            
            GridView1.DataSource = dt;
            GridView1.DataBind();
            clickable_incidents();
            num = dt.Rows.Count;

             dt1 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number],[DateEsclated] FROM [Expedite].[dbo].[Esclation] Order by [DateEsclated]; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt1 = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }


             dt4 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT [Incident_ID] From [Expedite].[dbo].[Expedite_time] where [Acknowledge]='True';";

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt4 = new DataTable())
                {

                    sda.Fill(dt4);

                }
            }
 
            for (int i = 0; i < num; i++)
            {


                //  Esclate2.Click += new EventHandler(this.EditingBtn_Click);

                //  Delete.Click += new EventHandler(this.DeleteBtn_Click);
                Esclate1 = new System.Web.UI.WebControls.Button();
                Checkbox = new System.Web.UI.WebControls.CheckBox();
                Esclate1.ID = (i).ToString();
                Checkbox.ID = (i + num + num + num).ToString();
                Esclate1.Click += new EventHandler(this.EditingBtn_Click);

                Checkbox.CheckedChanged += new EventHandler(this.CheckBoxClicked);
                Checkbox.CausesValidation = false;
                Checkbox.AutoPostBack = true;
                Label7 = new System.Web.UI.WebControls.Label();
                  for (int j = 0; j < dt1.Rows.Count;j++ )
                {
                    Inc1 = (String) dt.Rows[i][0] ;
                    Inc2 = (String) dt1.Rows[j][0] ;
                    
                    if (Inc1 == Inc2)
                    {
                        Esclate1.Enabled = false;
                        Label7.Text = dt1.Rows[j][1].ToString();
                        GridView1.Rows[i].Cells[8].Controls.Add(Label7);
                    
                    }
                }
                  
                  if (dt4.Rows.Count == 0) {
                      Checkbox.Checked = false;
                      GridView1.Rows[i].Cells[9].Controls.Add(Checkbox);
                  }

                for (int j = 0; j < dt4.Rows.Count; j++)
                  {
                      Inc1 = (String)dt.Rows[i][0];
                      Inc2 = (String)dt4.Rows[j][0];

                       
                      if (Inc1 == Inc2)
                      {
                          
                          Checkbox.Checked = true;
                          GridView1.Rows[i].Cells[9].Controls.Clear();
                          GridView1.Rows[i].Cells[9].Controls.Add(Checkbox);
                          break;


                      }
                      else {
                          Checkbox.Checked = false ;
                          GridView1.Rows[i].Cells[9].Controls.Add(Checkbox);
                      }
                  }

                
                
                Esclate1.Text = "Esclate";
                GridView1.Rows[i].Cells[8].Controls.Add(Esclate1);
                GridView1.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                GridView1.Rows[i].Cells[9].HorizontalAlign = HorizontalAlign.Center;

            }
            
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

    string GetLastModifiedDateExe() { 
           command = new SqlCommand();
           DataTable dt12;
           SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
           conn.Open();
            command.Connection = conn;
            command.CommandText = "select * from [expedite].[dbo].[Last_Update_Time]";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using ( dt12 = new DataTable())
                {

                    sda.Fill(dt12);

                }
                 
            }
            conn.Close();
        return dt12.Rows[0][0].ToString();
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
