using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

public partial class Incidents_to_expedite : System.Web.UI.Page
{
    public Boolean isdt = false;
    public Boolean isdt2;
    String searchingid;
    public static DataTable thetable;
    public static DropDownList newDropDownList1;
    public static String theidnow;

    public static DataTable readdt()
    {
        return thetable;
    }
    public static void changedt(DataTable dt)
    {
        thetable = dt;
    }
    public static DropDownList getdropdown(){
        return newDropDownList1;
    }
    public static void setdropdown(DropDownList dl)
    {
        newDropDownList1 = dl;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Label2.Visible = false;
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
          Textbox_message.Visible = false;
            newDropDownList1 = new DropDownList();
            //newDropDownList1.EnableViewState = true;
            fill_dropdown();
            thetable = new DataTable();
            //thetable.Clear();
            thetable.Columns.Add(new DataColumn("Incident ID", typeof(string))); //0
            thetable.Columns.Add(new DataColumn("Status", typeof(string))); //1
            thetable.Columns.Add(new DataColumn("Submit Date", typeof(string))); //2
            thetable.Columns.Add(new DataColumn("Assigned Group", typeof(string))); //3
            thetable.Columns.Add(new DataColumn("Assignee", typeof(string))); //4
            thetable.Columns.Add(new DataColumn("Urgency Reason", typeof(string))); //5
            thetable.Columns.Add(new DataColumn("Expedite Date", typeof(string))); //6
            thetable.Columns.Add(new DataColumn("Comment", typeof(string))); //6
           /* DataRow dr = thetable.NewRow();
            dr["Incident ID"] = "";// i;
            dr["Status"] = "";//
            dr["Submit Date"] = "";//
            dr["Assigned Group"] = "";//
            dr["Assignee"] = "";//
            dr["Urgency Reason"] = "";//
            dr["Expedite Date"] = "";//
            dr["Comment"] = "";//
            thetable.Rows.Add(dr);*/
            thetable.Rows.Add("Row", typeof(string));
            GridView1.DataSource = thetable;
            GridView1.DataBind();
            GridView1.Rows[0].Cells[5].Controls.Add(newDropDownList1);
            GridView1.Rows[0].Cells[7].Controls.Add(TextBox2);
            GridView1.Visible = false;
            DropDownList1.Visible = false;
            Button3.Visible = false;
            //GridView1.Visible = false;
            Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
            Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
            
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == null || String.IsNullOrEmpty(TextBox1.Text) || String.IsNullOrWhiteSpace(TextBox1.Text))
        {
            Textbox_message.Visible = true;
            Textbox_message.ForeColor = System.Drawing.Color.Red;
            Textbox_message.Font.Bold = true;
            Textbox_message.Text = "Please Enter an ID";
        }
        else if (TextBox1.Text.Count() < 8)
        {
            Textbox_message.Visible = true;
            Textbox_message.ForeColor = System.Drawing.Color.Red;
            Textbox_message.Font.Bold = true;
            Textbox_message.Text = "Wrong Ticket Format";
        }
        else
        {
            string y = TextBox1.Text.Substring(0, 8);
            if (y != "INC00100")
            {
                Textbox_message.Visible = true;
                Textbox_message.ForeColor = System.Drawing.Color.Red;
                Textbox_message.Font.Bold = true;
                Textbox_message.Text = "Wrong Ticket Format";
            }
            else {

                theidnow = TextBox1.Text;
                //GridView1.Rows[0].Cells[0].Text = "ID";
                //GridView1.Rows[0].Cells[1].Text= "Hello";
                //Debug.Write(newDropDownList1.SelectedItem.Value);
                newDropDownList1.ClearSelection();
                updategrid(TextBox1.Text, " ");
                GridView1.Visible = true;
                Button3.Visible = true;
                clickable_incidents();
                Textbox_message.Visible = false;
            }


        }
        if (TextBox1.Text != null && !String.IsNullOrEmpty(TextBox1.Text) && !String.IsNullOrWhiteSpace(TextBox1.Text))
        {
            
            // Debug.Write(get_submit_date(TextBox1.Text));
        }
       

        
    }
    protected void preselectdropdown()
    {
        string theid = TextBox1.Text;
        string thereason = "";
        SqlConnection conn2 = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x2 = (string)(Session["FTID"]);
        Debug.Write("thereasonis" + thereason);
        try
        {
            DataTable dt2 = new DataTable();
            conn2.Open();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = conn2;
            command2.CommandText = "SELECT [Urgency_Reason] FROM [Expedite].[dbo].[Expedite_time] Where [Incident_ID] = '" + theid + "';";
            using (SqlDataAdapter sda2 = new SqlDataAdapter())
            {
                sda2.SelectCommand = command2;

                using (dt2 = new DataTable())
                {
                    sda2.Fill(dt2);
                }

            }
            thereason = dt2.Rows[0][0].ToString();
            //Debug.Write("thereasonis"+thereason);
            newDropDownList1.ClearSelection();
            ListItem selectedListItem = newDropDownList1.Items.FindByValue(thereason);
            if (selectedListItem != null)
            {
                selectedListItem.Selected = true;
            }
            conn2.Close();
        }
        catch (Exception ex)
        {
            conn2.Close();
            Console.Write(ex.ToString());
        }
    }

    protected void updategrid(String id, String reason)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
            String x = (string)(Session["FTID"]);
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "Select [INC Incident Number],[INC Status],[INC DS Submit Date], [AG Assigned Group Name], [AG Assignee],[Urgency_Reason],[Expedite_Date],[Comment] From [Expedite].[dbo].['All_Incidents'] as AL FULL OUTER JOIN [Expedite].[dbo].[Expedite_time] as ET ON AL.[INC Incident Number] =  ET.[Incident_ID] Where  AL.[INC Incident Number]='" + id + "' or ET.[Incident_ID]='" + id + "';";
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = command;
                    using (dt = new DataTable())
                    {
                        sda.Fill(dt);
                    }
                }

                if (dt.Rows[0][5].ToString() != "") //preselected reason (expidited)
                {
                    preselectdropdown();
                }
                GridView1.Rows[0].Cells[0].Text = id;
                GridView1.Rows[0].Cells[1].Text = dt.Rows[0][1].ToString();
                GridView1.Rows[0].Cells[2].Text = dt.Rows[0][2].ToString();
                GridView1.Rows[0].Cells[3].Text = dt.Rows[0][3].ToString();
                GridView1.Rows[0].Cells[4].Text = dt.Rows[0][4].ToString();
                GridView1.Rows[0].Cells[6].Text = dt.Rows[0][6].ToString();
                if(dt.Rows[0][6].ToString() !=""){
                TextBox2.Text = dt.Rows[0][7].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }
    }

    protected void fill_dropdown()
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
            newDropDownList1.Items.Add(new ListItem("Type a reason..","-None-"));
            for (int i = 0; i < ar.Count; i++)
            {
                newDropDownList1.Items.Add(new ListItem(ar[i].ToString()));
            }
    }
    protected void myListDropDown_Change(object sender, EventArgs e)
    {
        Debug.Write("changed");
    }
     void post_reason(int num)
    {
        //updatedropdown(1);
        //Debug.Write(TextBox1.Text);
        String thereason = newDropDownList1.SelectedItem.Value;
        //String thereason = getdropdown().SelectedItem.Value;
        //DropDownList1.Visible = true;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        String idtoexp = GridView1.Rows[0].Cells[0].Text;
        //Debug.Write("my id is" + x);
        //BindingSource bs = (BindingSource)GridView1.DataSource;
        //DataTable tCxC = (DataTable)bs.DataSource;
        //Debug.Write("value is" + getdropdown().SelectedItem.Value);
        //Debug.Write("Value is " + ((DataColumn)readdt().Columns[0]).ColumnName);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            //Debug.Write("Header iss " + GridView1.HeaderRow.Cells[0].Text.ToString());
            command.Connection = conn;
            //Debug.Write("Header is "+GridView1.Columns[0].HeaderText.ToString());
            //Debug.Write("Header is " + GridView1.HeaderRow.Cells[2].Text.ToString());
            //Debug.Write("Value is " + ((DataColumn)readdt().Columns[0]).ColumnName);
            String thesubmitdate = get_submit_date(theidnow);
            if (num==2) //exists only in all inc
            {
                command.CommandText = "INSERT INTO [Expedite].[dbo].[Expedite_time] (Incident_ID,Expedite_Date,Urgency_Reason,Comment,Expedite_By,Submit_Date) VALUES ('" + theidnow + "','" + DateTime.Now.ToString() + "','" + thereason + "' , '" + TextBox2.Text + "','" + x + "',convert (datetime,'"+thesubmitdate+"'));";
                expedite_mailnotification(theidnow, thereason);
            }
            else{
                command.CommandText = "UPDATE [Expedite].[dbo].[Expedite_time] SET Urgency_Reason = '" + thereason +"', Comment='" + TextBox2.Text + "' Where [Incident_ID] = '" + theidnow + "';";
                //Debug.Write(" reason " + thereason + " comment " + TextBox2.Text + " id " + searchingid);
           }
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
            DropDownList1.Visible = false;
            insert_expedite_time_to_allinc(theidnow);
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
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
    protected void post_comment()
    {
        String thecomment = TextBox2.Text;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
            String x = (string)(Session["FTID"]);
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "UPDATE [Expedite].[dbo].[Expedite_time] SET Comment = '" + thecomment + "' Where [Incident_ID] = '" + searchingid + "';";
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
                DropDownList1.Visible = false;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }
    }

    int GetColumnIndexByName(GridViewRow row, string columnName)
    {
        int columnIndex = 0;
        foreach (DataControlFieldCell cell in row.Cells)
        {
            if (cell.ContainingField is BoundField)
                if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                    break;
            columnIndex++; // keep adding 1 while we don't have the correct name
        }
        return columnIndex;
    }

    protected void updatedropdown(int num){
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
            DropDownList1.Items.Add(new ListItem("Type a reason..","-None-"));
            for (int i = 0; i < ar.Count; i++)
            {
                DropDownList1.Items.Add(new ListItem(ar[i].ToString()));
            }
       // }
            //setdropdown(DropDownList1);
        if (num == 2)
        {
            string theid = TextBox1.Text;
            string thereason = "";
            SqlConnection conn2 = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
            String x2 = (string)(Session["FTID"]);
            try
            {
                DataTable dt2 = new DataTable();
                conn2.Open();
                SqlCommand command2 = new SqlCommand();
                command2.Connection = conn2;
                command2.CommandText = "SELECT [Urgency_Reason] FROM [Expedite].[dbo].[Expedite_time] Where [Incident_ID] = '"+theid+"';";
                using (SqlDataAdapter sda2 = new SqlDataAdapter())
                {
                    sda2.SelectCommand = command2;

                    using (dt2 = new DataTable())
                    {
                        sda2.Fill(dt2);
                    }

                }
                thereason = dt2.Rows[0][0].ToString();
                //Debug.Write("The reason for" +theid+" is "+dt3.Rows[0][0].ToString());
                ListItem selectedListItem = DropDownList1.Items.FindByValue(thereason);
                //Debug.Write("value is " +selectedListItem.Value);
                if (selectedListItem != null)
                {
                    //Debug.Write("madeit");
                    selectedListItem.Selected = true;
                }
                conn2.Close();
            }
            catch (Exception ex)
            {
                //Debug.Write("Here");
                conn2.Close();
                Console.Write(ex.ToString());
            }


        }
        setdropdown(DropDownList1);
        getdropdown().Visible = true;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Debug.WriteLine("id is " + theidnow);
        if (newDropDownList1.SelectedItem.Value != "-None-")
        {
            if (isexpedited(theidnow))
            {
                post_reason(1);
                GridView1.Visible = false;
                Textbox_message.Visible = true;
                Textbox_message.ForeColor = System.Drawing.Color.Green;
                Textbox_message.Text = "Updated Sucessfully";
            }
            else
            {
                post_reason(2);
                GridView1.Visible = false;
                Textbox_message.Visible = true;
                Textbox_message.ForeColor = System.Drawing.Color.Green;
                Textbox_message.Text = "Expedited Sucessfully";
            }
        }
        else
        {
            Textbox_message.Visible = true;
            Textbox_message.ForeColor = System.Drawing.Color.Red;
            Textbox_message.Text = "Please Choose an Urgency Reason!";
            updategrid(theidnow, "");
            GridView1.Visible = true;
            Button3.Visible = true;
            clickable_incidents();
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
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [AG Assigned Group Name],[AG Assignee],[INC CI Email Address],[Expedited_mail],[AG Assigned Group Name] FROM [Expedite].[dbo].['All_Incidents'],[Expedite].[dbo].[Expedite_time] where [INC Incident Number]='" + Incident + "';";
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
        mail.To.Add(submitter_mail);
        ArrayList teamlist = getteamlist(assigned_group);
        foreach (String team_member in teamlist)
        {
            mail.To.Add(team_member);
        }
        if (!String.IsNullOrEmpty(Session["Email"].ToString()) && (Session["Email"]) != null && !String.IsNullOrWhiteSpace(Session["Email"].ToString()))
        {
            mail.To.Add(Session["Email"].ToString());
        }
        mail.CC.Add("it.support4business@orange.com");
        mail.Body = "The ticket with Incident number " + Incident + " has been expedited." + "\n" + "Group: " + group_name + "\n" + "Assignee: " + assignee_name + "\n" + "Urgency Reason: " + Urgency_reason;
        mail.Subject = "Incident " + Incident + " Expedited";
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
    protected bool isexpedited(String id)
    {
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
            conn.Close();
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        return false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        GridView1.Visible = false;
        TextBox2.Text = "";
        DropDownList1.Visible = false;
        TextBox2.Visible = false;
        Button3.Visible = false;
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
        conn.Close();
        return dt.Rows[0][0].ToString();
    }
}