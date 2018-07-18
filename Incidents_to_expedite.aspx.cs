﻿using System;
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

public partial class Incidents_to_expedite : System.Web.UI.Page
{
    public Boolean isdt = false;
    public Boolean isdt2;
    String searchingid;
    public static DataTable thetable;
    public static DropDownList newDropDownList1;

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
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        //Debug.Write("entered");
           // DropDownList1.Visible = false;
            //TextBox2.Visible = false;
            //Button3.Visible = false;
                //[INC Incident Number],[INC Status],[Submit_Date], [AG Assigned Group Name], [AG Assignee],[Urgency_Reason],[Expedite_Date]
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
            
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != null)
        {
            //GridView1.Rows[0].Cells[0].Text = "ID";
            //GridView1.Rows[0].Cells[1].Text= "Hello";
            //Debug.Write(newDropDownList1.SelectedItem.Value);
            newDropDownList1.ClearSelection();
            updategrid(TextBox1.Text, " ");
            GridView1.Visible = true;
            Button3.Visible = true;
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
                command.CommandText = "Select [INC Incident Number],[INC Status],[Submit_Date], [AG Assigned Group Name], [AG Assignee],[Urgency_Reason],[Expedite_Date],[Comment] From [Expedite].[dbo].['All_Incidents'] as AL FULL OUTER JOIN [Expedite].[dbo].[Expedite_time] as ET ON AL.[INC Incident Number] =  ET.[Incident_ID] Where  AL.[INC Incident Number]='" + id + "' or ET.[Incident_ID]='"+id+"';";
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
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }
            newDropDownList1.Items.Add(new ListItem("-None-"));
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
            if (num==2) //exists only in all inc
            {
                command.CommandText = "INSERT INTO [Expedite].[dbo].[Expedite_time] (Incident_ID,Expedite_Date,Urgency_Reason,Comment,Expedite_By) VALUES ('" + TextBox1.Text + "','" + DateTime.Now.ToString() + "','" + thereason + "' , '" + TextBox2.Text + "','"+ x +"');";
                   // Debug.Write("first");
            }
            else{
                command.CommandText = "UPDATE [Expedite].[dbo].[Expedite_time] SET Urgency_Reason = '" + thereason +"',Expedite_By='" + x + "', Comment='" + TextBox2.Text + "' Where [Incident_ID] = '" + TextBox1.Text + "';";
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
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
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
        if (newDropDownList1.SelectedItem.Value != "-None-")
        {
            if (isexpedited(TextBox1.Text))
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
            updategrid(TextBox1.Text, "");
            GridView1.Visible = true;
            Button3.Visible = true;
        }
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
}