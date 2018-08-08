using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddTeamMail : System.Web.UI.Page
{
    SqlCommand command;
    SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

    DataTable dt;   
    protected void Page_Load(object sender, EventArgs e)
    {
        Label3.Visible = false;
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
       
        if (!Page.IsPostBack) { 
        conn.Open();
        command = new SqlCommand();
        command.Connection = conn;
        command.CommandText = "SELECT distinct [GRP Group Name] as 'GroupName' FROM [Expedite].[dbo].[Staff] WHERE [GRP Group Name] NOT IN(SELECT [GRP Group Name] FROM [Expedite].[dbo].[Group_Mail]);";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }
        }
        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "GroupName";
        DropDownList1.DataValueField = "GroupName";
        DropDownList1.DataBind();
        DropDownList1.Items.Add(new ListItem("Type Team Name ..", "none"));
        DropDownList1.SelectedIndex = DropDownList1.Items.Count - 1;

        conn.Close();
        }

        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "none")
        {
            String Mail = TextBox1.Text;
            conn.Open();
            if (Mail == "")
            {
                Label3.Visible = true;
                Label3.Text = "Mail Is Empty";
            }
            if (!Mail.Contains("@"))
            {
                Label3.Visible = true;
                Label3.Text = "Mail Doesn't Contain @";
            }
            else
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "INSERT INTO [Expedite].[dbo].[Group_Mail]([GRP Group Name] ,[mail])Values('" + DropDownList1.SelectedValue + "','" + Mail + "')";
                Debug.WriteLine(command.CommandText);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = command;

                    using (dt = new DataTable())
                    {

                        sda.Fill(dt);

                    }
                }
                Response.Redirect("AddTeamMail.aspx");
            }
        }
        else
        {
            Label3.Visible = true;
            Label3.Text = "Please Select A Group";
        }
    }
    string GetLastModifiedDate()
    {
        return System.IO.File.GetLastWriteTime("D:/Expedite/NewExpedite.xls").ToString();
    }

    string GetLastModifiedDateExe()
    {
        command = new SqlCommand();
        DataTable dt12;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        conn.Open();
        command.Connection = conn;
        command.CommandText = "select * from [expedite].[dbo].[Last_Update_Time]";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using (dt12 = new DataTable())
            {

                sda.Fill(dt12);

            }

        }
        conn.Close();
        return dt12.Rows[0][0].ToString();
    }
}