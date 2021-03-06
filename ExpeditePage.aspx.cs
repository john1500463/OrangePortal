﻿using System;
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
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
    

public partial class ExpeditePage : System.Web.UI.Page
{
    public static int tablerow;
    public static ArrayList emailsarrlist;
    public static ArrayList emailsarrlistedit;
    public static ArrayList emailsarrlistremove;
    public class CryptoEngine1
    {
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }    


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
        String Incident = Request.QueryString["param1"];
        try
        {
            Incident = CryptoEngine1.Decrypt(Incident, "sblw-3hn8-sqoy19");
        }
        catch (Exception ex)
        { 
      //  Response.Redirect("Default.aspx");
        }
        Label1.Text = Incident;

        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$";
        cnn = new SqlConnection(connetionString);
        // cnn.Open();

        string strSelect = "Select * From [Expedite].[dbo].[UrgencyReasons]";
        // string strSelect = "Select * From [dbo].['All_Incidents'] ;";
        //   SqlCommand cmd = new SqlCommand(strSelect, cnn);
        SqlDataAdapter adpt = new SqlDataAdapter(strSelect, cnn);

        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "UrgencyReason";
        DropDownList1.DataValueField = "ID";
        DropDownList1.DataBind();
        //  Label1.Text = myReader.GetValue(0).ToString();

        if (!Page.IsPostBack)
        {
            tablerow = 0;
            emailsarrlist = new ArrayList();
            emailsarrlistedit = new ArrayList();
            emailsarrlistremove = new ArrayList();
        }
        else
        {
            /*
            foreach (TextBox  t in emailsarrlist)
            {

                Control myControl1 = FindControl("theoneformails");
                myControl1.Controls.Add(t);
                //Debug.WriteLine("Added " + t.ID +"with text" + t.Text);
            }

            foreach (Button t in emailsarrlistedit)
            {

                Control myControl1 = FindControl("theoneformails");
                myControl1.Controls.Add(t);
                //Debug.WriteLine("Added " + t.ID + "with text" + t.Text);
            }
            foreach (Button t in emailsarrlistremove)
            {

                Control myControl1 = FindControl("theoneformails");
                myControl1.Controls.Add(t);
              //  Debug.WriteLine("Added " + t.ID + "with text" + t.Text);
            }
            */

            for (int i = 0; i < emailsarrlist.Count; i++)
            {
                Control myControl1 = FindControl("theoneformails");
                myControl1.Controls.Add((TextBox)emailsarrlist[i]);
                ((Button)emailsarrlistedit[i]).Click += new EventHandler(this.editclick);
                myControl1.Controls.Add((Button)emailsarrlistedit[i]);
                ((Button)emailsarrlistremove[i]).Click += new EventHandler(this.deleteclick);
                myControl1.Controls.Add((Button)emailsarrlistremove[i]);
            }

        }
       
        /*   SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
           conn.Open();
           SqlCommand command = new SqlCommand();
           command.Connection = conn;
           command.CommandText = "Select * From [Expedite].[dbo].[UrgencyReasons]";
           DataTable dt = new DataTable();
        //   SqlDataAdapter dr = new SqlDataAdapter();
       //    DropDownList1.Items.Clear();

           using (SqlDataAdapter dr = new SqlDataAdapter())
           {
               dr.SelectCommand = command;
               using (dt = new DataTable())
               {

                   dr.Fill(dt);

               }
            //   DropDownList1.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
           }
            
           DropDownList1.DataSource = dt;
        //   DropDownList1.DataBind();
           DropDownList1.DataTextField = "UrgencyReason";
          DropDownList1.DataValueField = "ID ";
           DropDownList1.DataBind();
            
           conn.Close();
       } */


        //   Label3.Text = (string)(Session["Email"]);

        /*    String Email = (string)(Session["Email"]);
            String FTID =  (string)(Session["FTID"]);
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "Select * From [dbo].[Expedite_time] where [Expedite_BY]='" + FTID + "';";
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
                //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }

            */
        cnn.Close();
        Label2.Visible = false;
        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();

    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        if (DropDownList1.SelectedItem.Value == "Select Reason")
        {
           // System.Windows.Forms.MessageBox.Show("Please Select A Reason!");
            Label2.Visible = true;
            Label2.Text = "Please Select a Reason!";
        }
        else
        {
            Label2.Text = "";
            Label2.Visible = false;
            String Incident = Request.QueryString["param1"];
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            string strSelect = "Select [INC DS Submit Date] from [dbo].['All_Incidents'] where [INC Incident Number]='" + Incident + "';";
            // string strSelect = "Select * From [dbo].['All_Incidents'] ;";
            SqlCommand cmd = new SqlCommand(strSelect, cnn);
            SqlDataReader myReader = cmd.ExecuteReader();
            myReader.Read();
            //  Label1.Text = myReader.GetValue(0).ToString(); */
            // refresh_grid1();
           // String SubmitDate = myReader.GetValue(0).ToString();
            String SubmitDate = get_submit_date(Incident); ;
            String UrgenyReason = DropDownList1.SelectedItem.Text;
            String FTID = (string)(Session["FTID"]);
            string strSelect2 = "insert into [Expedite].[dbo].[Expedite_time](Incident_ID,Submit_Date,Expedite_By,Expedite_Date,Urgency_Reason,Expedited_mail,Expedited_Fullname) values ('"
                + Incident + "','" + SubmitDate + "','" + FTID + "', GETDATE() ,'" + UrgenyReason + "','" + Session["Email"] + "','"+Session["Fname"]+ " " +Session["Lname"]+"');";
            SqlCommand cmd1 = new SqlCommand(strSelect2, cnn);
            myReader.Close();
            cmd1.ExecuteNonQuery();



            cnn.Close();
            insert_expedite_time_to_allinc(Incident);
            expedite_mailnotification(Incident, UrgenyReason);
            // Response.Write("<script LANGUAGE='JavaScript' >alert('The Incident has been Expedited')</script>");
            Response.Redirect("Home_Page.aspx");
        }

    }
    void insert_expedite_time_to_allinc(String idd){
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
        var textboxesInContainer = FindControl("theoneformails").Controls.OfType<TextBox>();
        foreach (TextBox tb in textboxesInContainer)
        {
            if (tb.ID != "TextBox_Mail" && tb.Visible == true)
            {
                //Debug.WriteLine("Mail will be sent to" + tb.Text);
                mail.CC.Add(tb.Text);
            }
            else
            {
                //Debug.WriteLine("Mail will be not sent to" + tb.ID);
            }
        }
        if (!String.IsNullOrEmpty(Session["Email"].ToString()) && (Session["Email"]) != null && !String.IsNullOrWhiteSpace(Session["Email"].ToString()))
        {
            mail.To.Add(Session["Email"].ToString());
        }
        mail.CC.Add("it.support4business@orange.com");
        mail.Subject = "Expedited Incident " + Incident;
        mail.Body = "Hello " + (string)Session["Fname"] + "," + "\n" + "Thank you for using the Expedite Portal." + "\n" + "Kindly note that the incident with reference " + Incident + " regarding " + tier2 + " has been expedited with urgency reason " + Urg_Reason + "." + "\n" + "Incident is now assigned to group: " + group_name + " which is managed by " + getmanagername(getmanagerofinc(Incident)) + "\n" + "To check the update of this issue, please check your Expedited Incidents tab on: http://cas-its4b.vdr.equant.com/expedite/" + "\n" + "We assure you that the IT Support for business Team will put every effort into resolving this issue as soon as possible." + "\n" + "Regards," + "\n" + "IT Support for Business team";
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
        mail2.Body = "Hello Team," + "\n" + "Thanks to note that the subjected incident is expedited by the user with urgency reason " +Urg_Reason+". Appreciate if you can check the incident on priority and feedback us accordingly." + "\n" + "Your fast action is highly appreciated." + "\n" + "You can also check the expedited incidents that are in your queue in the Expedited Incidents Tab (if any) on: http://cas-its4b.vdr.equant.com/expedite/" + "\n" + "Regards," + "\n" + "IT Support for Business team";
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
            manager_mail = dt.Rows[0][1].ToString();
            assignee_name = dt.Rows[0][2].ToString();
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
        Debug.WriteLine("GET NAME of " + manager_email);
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
            Debug.WriteLine(command2.CommandText);
            using (dt = new DataTable())
            {

                sda2.Fill(dt);

            }
        }
        if (dt.Rows.Count > 0)
        {
            name = dt.Rows[0][0].ToString();
        }
        else
        {
            DataTable newdt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [AG Assignee Manager Name] FROM [Expedite].[dbo].['All_Incidents'] where [AG M Email Address]='" + manager_email + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                Debug.WriteLine(command.CommandText);
                using (newdt = new DataTable())
                {

                    sda.Fill(newdt);

                }
            }
            if (newdt.Rows.Count > 0)
            {
                name = newdt.Rows[0][0].ToString();
            }
        }
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
            command.CommandText = "SELECT [mail] FROM [Expedite].[dbo].[Group_Mail] Where [GRP Group Name]='"+group_name+"';";
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
    protected void Button_Addmail_Click(object sender, System.EventArgs e)
    {
        if (TextBox_Mail.Text != "")
        {
            Button delete = new Button();
            delete.Text = "Remove";
            delete.ID = "deleteid" + tablerow;
            delete.Click += new EventHandler(this.deleteclick);

            Button edit = new Button();
            edit.Text = "Edit";
            edit.ID = "editid" + tablerow;
            edit.Click += new EventHandler(this.editclick);

            TextBox textb = new TextBox();
            textb.ID = "tbid" + tablerow;
            textb.Text = TextBox_Mail.Text;


          
            textb.Enabled = false;
            //textb.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

            emailsarrlist.Add(textb);
            emailsarrlistedit.Add(edit);
            emailsarrlistremove.Add(delete);

            Control myControl1 = FindControl("theoneformails");
            myControl1.Controls.Add(textb);
            myControl1.Controls.Add(edit);
            myControl1.Controls.Add(delete);
            tablerow++;
            TextBox_Mail.Text = "";
            Label_Emailslist.Visible = true;
        }
    }
    void editclick(Object sender, EventArgs e)
    {

        Debug.WriteLine("Pressed ");
        System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
        String id = clickedButton.ID;
        String finalid = id.Substring(6);
        String searchfor = "tbid" +finalid;
        if (clickedButton.Text == "Save")
        {
            var textboxesInContainer = FindControl("theoneformails").Controls.OfType<TextBox>();
            foreach (TextBox tb in textboxesInContainer)
            {
                if (tb.ID == searchfor)
                {
                    //Debug.WriteLine("Mail will be sent to" + tb.Text);
                    tb.Enabled = false;
                    int index = -1;
                    for (int i = 0; i < emailsarrlist.Count; i++)
                    {
                        if (((TextBox)(emailsarrlist[i])).ID == searchfor)
                        {
                            index = i;
                        }
                    }
                    ((Button)(emailsarrlistedit[index])).Text = "Edit";
                }
            }
        }
        else
        {

            var textboxesInContainer = FindControl("theoneformails").Controls.OfType<TextBox>();
            foreach (TextBox tb in textboxesInContainer)
            {
                if (tb.ID == searchfor)
                {
                    //Debug.WriteLine("Mail will be sent to" + tb.Text);
                    tb.Enabled = true;
                    tb.Focus();
                    int index = -1;
                    for (int i = 0; i < emailsarrlist.Count; i++)
                    {
                        if (((TextBox)(emailsarrlist[i])).ID == searchfor)
                        {
                            index = i;
                        }
                    }
                    ((Button)(emailsarrlistedit[index])).Text = "Save";
                }
                else
                {
                    //Debug.WriteLine("Mail will be not sent to" + tb.ID);
                }
            }
        }

    }
    void deleteclick(Object sender, EventArgs e)
    {
Debug.WriteLine("Pressed ");
        System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
        String id = clickedButton.ID;
        String finalid = id.Substring(8);
        String searchfor = "tbid" +finalid;
        String searchforedit = "editid" + finalid;
        String searchfordelete = "deleteid" + finalid;
        Debug.WriteLine("remove row" + finalid);
        var textboxesInContainer = FindControl("theoneformails").Controls.OfType<TextBox>();
        foreach (TextBox tb in textboxesInContainer)
        {
            if (tb.ID == searchfor)
            {
                tb.Visible = false;
            }
            else
            {
                //Debug.WriteLine("Mail will be not sent to" + tb.ID);
            }
        }
        var editbuttonsInContainer = FindControl("theoneformails").Controls.OfType<Button>();
        foreach (Button tb in editbuttonsInContainer)
        {
            if (tb.ID == searchforedit)
            {
                tb.Visible = false;
            }
            else
            {
                //Debug.WriteLine("Mail will be not sent to" + tb.ID);
            }
        }
        var deletebuttonsInContainer = FindControl("theoneformails").Controls.OfType<Button>();
        foreach (Button tb in deletebuttonsInContainer)
        {
            if (tb.ID == searchfordelete)
            {
                tb.Visible = false;
            }
            else
            {
                //Debug.WriteLine("Mail will be not sent to" + tb.ID);
            }
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
    protected void plazadiscussion_click(object sender, System.EventArgs e)
    {
        DataTable dtpz;
        SqlCommand command = new SqlCommand();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        conn.Open();
        command.Connection = conn;
        command.CommandText = "select [INC Summary] from [Expedite].[dbo].['All_Incidents'] where [INC Incident Number]='" + Label1.Text +"'"; ;
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using (dtpz = new DataTable())
            {

                sda.Fill(dtpz);

            }

        }
        String thesummary = dtpz.Rows[0][0].ToString();
        conn.Close();
        Response.Redirect("https://plazza.orange.com/discussion/create.jspa?containerType=14&containerID=2491&subject=" + Label1.Text+"/" + thesummary);
    }
}