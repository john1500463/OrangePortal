using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class Expedite_multiple_incidents : System.Web.UI.Page
{
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
        //DropDownList1.Visible = false;
        //Expedite_Button.Visible = false;
        Textbox_message.Visible = false;
        updatedropdown();
        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
    }
    protected void updatedropdown()
    {
        ArrayList ar = new ArrayList();

        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt3 = new DataTable();
            conn.Open();
            SqlCommand command3 = new SqlCommand();
            command3.Connection = conn;
            command3.CommandText = "SELECT [UrgencyReason] FROM [Expedite].[dbo].[UrgencyReasons];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command3;
                using (dt3 = new DataTable())
                {
                    sda.Fill(dt3);
                }

            }
            foreach (DataRow row in dt3.Rows)
            {
                foreach (DataColumn column in dt3.Columns)
                {
                    // Debug.Write(row[column]);
                    ar.Add(row[column]);
                }
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        DropDownList1.Items.Add(new ListItem("-None-"));
        for (int i = 0; i < ar.Count; i++)
        {
            DropDownList1.Items.Add(new ListItem(ar[i].ToString()));
        }
    }
    protected void expedite(String id)
    {
        bool isexpedited = false;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM [Expedite].[dbo].[Expedite_time] Where [Incident_ID] = '" + id + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {
                    sda.Fill(dt);
                }
            }

            if (dt.Rows.Count == 0)
            {
                //Debug.Write("not expedited");
                isexpedited = false;
            }
            else
            {
                isexpedited = true;
               // Debug.Write("expedited");
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        Debug.Write("first0");
        if (!isexpedited) //exists only in all inc
        {

            try
            {
                String thereason = DropDownList1.SelectedItem.Value;
                DataTable dt2 = new DataTable();
                SqlCommand command2 = new SqlCommand();
                command2.Connection = conn;
                String thesubmitdate = get_submit_date(id);
                command2.CommandText = "INSERT INTO [Expedite].[dbo].[Expedite_time] (Incident_ID,Submit_Date,Expedite_Date,Urgency_Reason,Expedite_By,Expedited_mail) VALUES ('" + id + "','" + thesubmitdate + "','" + DateTime.Now.ToString() + "','" + thereason + "','" + x + "','" + Session["Email"] + "');";
                //Debug.Write("first");
                //  else
                //  {
                //  command2.CommandText = "UPDATE [Expedite].[dbo].[Expedite_time] SET Urgency_Reason = '" + thereason + "',Expedite_By='"+ x +"' Where [Incident_ID] = '" + id + "';";
                //     Debug.Write(" reason " + thereason + " id " + id);
                // }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = command2;
                    using (dt2 = new DataTable())
                    {
                        sda.Fill(dt2);
                    }
                }
                Textbox_message.Text += " ID: " + id + " is expedited sucessfully!" + "<br />";
                insert_expedite_time_to_allinc(id);
                expedite_mailnotification(id,thereason);
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }
        }
        else
        {
            Textbox_message.Text += " ID: " + id + " is already expedited" + "<br />";
        }
        conn.Close();
    }
    void insert_expedite_time_to_allinc(String idd)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [Expedite_Date] From [Expedite].[dbo].[Expedite_time] where [Incident_ID]='" + idd + "';";
            Debug.WriteLine(command.CommandText);
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            Debug.WriteLine("Value is " + dt.Rows[0][0]);
            DateTime value = (DateTime)dt.Rows[0][0];
            Debug.WriteLine("DAte time Value is " + value);
            DataTable dt2 = new DataTable();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = conn;
            command2.CommandText = "Update [Expedite].[dbo].['All_Incidents'] SET [Expedite Date]= '" + value + "' where [INC Incident Number]='" + idd + "';";
            Debug.WriteLine(command2.CommandText);
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command2;
                using (dt2 = new DataTable())
                {

                    sda.Fill(dt2);

                }
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }
    protected void expedite_mailnotification(String Incident, String Urgency_reason)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        String group_name = "";
        String assignee_name = "";
        String submitter_mail = "";
        String expeditedby_mail = "";
        String assigned_group = "";
        String tier2 = "";
        String summary = "";
        String Urg_Reason = "";
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [AG Assigned Group Name],[AG Assignee],[INC CI Email Address],[Expedited_mail],[AG Assigned Group Name],[INC Tier 2],[INC Summary],[Urgency_Reason] FROM [Expedite].[dbo].['All_Incidents'],[Expedite].[dbo].[Expedite_time] where [INC Incident Number]='" + Incident + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            group_name = dt.Rows[0][0].ToString();
            assignee_name = dt.Rows[0][1].ToString();
            submitter_mail = dt.Rows[0][2].ToString();
            expeditedby_mail = dt.Rows[0][3].ToString();
            assigned_group = dt.Rows[0][4].ToString();
            tier2 = dt.Rows[0][5].ToString();
            summary = dt.Rows[0][6].ToString();
            Urg_Reason = dt.Rows[0][7].ToString();
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("mx-us.equant.com");
        mail.From = new MailAddress("it.support4business@orange.com");
        //Debug.Write(submitter_mail);
        mail.CC.Add(submitter_mail);
        if (!String.IsNullOrEmpty(Session["Email"].ToString()) && (Session["Email"]) != null && !String.IsNullOrWhiteSpace(Session["Email"].ToString()))
        {
            mail.To.Add(Session["Email"].ToString());
        }
        mail.CC.Add("it.support4business@orange.com");
        mail.Subject = "Expedited Incident " + Incident;
        mail.Body = "Hello " + (string)Session["Fname"] + "," + "\n" + "Thank you for using the Expedite Portal." + "\n" + "Kindly note that the incident with reference " + Incident + " regarding application " + tier2 + " has been expedited with urgency reason " + Urg_Reason + "." + "\n" + "Incident is now assigned to group: " + group_name + " which is managed by " + getmanagername(getmanagerofinc(Incident)) + "\n" + "To check the update of this issue, please connect to: http://cas-its4b.vdr.equant.com/expedite/" + "\n" + "We assure you that the IT Support for business Team will put every effort into resolving this issue as soon as possible." + "\n" + "Regards," + "\n" + "IT Support for Business team";
        SmtpServer.Send(mail);
        Debug.WriteLine(mail.Body);
        MailMessage mail2 = new MailMessage();
        SmtpClient SmtpServer2 = new SmtpClient("mx-us.equant.com");
        mail2.From = new MailAddress("it.support4business@orange.com");
        ArrayList teamlist = getteamlist(assigned_group);
        foreach (String team_member in teamlist)
        {
            mail2.To.Add(team_member);
        }
        mail2.CC.Add("it.support4business@orange.com");
        mail2.Subject = "Expedited Incident " + Incident;
        mail2.Body = "Hello Team," + "\n" + "Kindly provide your urgent assistance upon this incident " + Incident + "." + "\n" + "Your fast action is highly appreciated." + "\n" + "Check and acknowledge expedites in your queue on: http://cas-its4b.vdr.equant.com/expedite/" + "\n" + "Regards," + "\n" + "IT Support for Business team";
        Debug.WriteLine(mail2.Body);
        SmtpServer2.Send(mail2);
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
    protected String getmanagername(String manager_email)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        DataTable dt = new DataTable();
        String name = "";
        conn.Open();
        SqlCommand command2 = new SqlCommand();
        command2.Connection = conn;
        command2.CommandText = "SELECT [PE Full Name] FROM [Expedite].[dbo].[Staff] where [PE Email]='" + manager_email + "';";
        using (SqlDataAdapter sda2 = new SqlDataAdapter())
        {
            sda2.SelectCommand = command2;
            using (dt = new DataTable())
            {

                sda2.Fill(dt);

            }
        }
        name = dt.Rows[0][0].ToString();
        conn.Close();
        return name;
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
    protected void Expedite_Button_Click(object sender, EventArgs e)
    {
        Textbox_message.Text = " ";
        if (DropDownList1.SelectedItem.Value != "-None-")
        {
            String idstring = TextBox_id.Text;
            //Debug.Write(idsstring);
            String idstring2 = idstring.Replace(" ", "");
            String[] ids = idstring2.Split(',');
            foreach (String id in ids)
            {
                //Debug.Write(id + " ");
                expedite(id);
            }
            GridView1.Visible = false;
            //TextBox_id.Text = " ";
            Textbox_message.Visible = true;
            //Textbox_message.ForeColor = System.Drawing.Color.Green;
            //Textbox_message.Text = "Expedited Sucessfully";
        }
        else
        {

            Textbox_message.Visible = true;
            Textbox_message.ForeColor = System.Drawing.Color.Red;
            Textbox_message.Text = "Please Choose an Urgency Reason!";
        }
 
    }
    protected void Search_Button_Click(object sender, EventArgs e)
    {
        ArrayList arr = new ArrayList();
        String idstring = TextBox_id.Text;
        String idstring2 = idstring.Replace(" ", "");
        String[] ids = idstring2.Split(',');
        foreach (String id in ids)
        {
            arr.Add(id);
        }

        showgrid(arr);
    }
    protected void showgrid(ArrayList arr)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            String cmdstring = "Select [INC Incident Number] as 'Incident Number',[INC Status] as 'Status',[INC Tier 2] as 'Tier 2',[INC DS Submit Date] as 'Submit Date',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified' From [dbo].['All_Incidents'] where [INC Incident Number]='" + arr[0] + "'";
                
            foreach(String idstring in arr){
                cmdstring +=" or [INC Incident Number]='" +idstring+ "'";
            }
            cmdstring += ";";
            command.CommandText = cmdstring;
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;

                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            if (dt.Rows.Count == 0)
            {
                //Label1.Text = "Incident doesn't exist";
                GridView1.Visible = false;
                // Label1.Text = "";

            }
            else
            {
                GridView1.DataSource = dt;
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
    protected String get_submit_date(String subid)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC DS Submit Date] From [Expedite].[dbo].['All_Incidents'] where [INC Incident Number]='" + subid + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            String value = dt.Rows[0][0].ToString();
            conn.Close();
            return value;
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        return " ";
    }
    protected void Clear_Button_Click(object sender, EventArgs e)
    {
        TextBox_id.Text = "";
        GridView1.Visible = false;
        ListItem selectedListItem = DropDownList1.Items.FindByValue("-None-");
        if (selectedListItem != null)
        {
            //Debug.Write("madeit");
            //DropDownList1.Visible = false;
            DropDownList1.SelectedItem.Selected = false;
            selectedListItem.Selected = true;
            //DropDownList1.Visible = true;
        }
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
        conn.Close();
        return dt.Rows[0][0].ToString();
    }
}