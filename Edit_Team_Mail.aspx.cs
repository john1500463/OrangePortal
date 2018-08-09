using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrangePortal_Edit_Team_Mail : System.Web.UI.Page
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
        Label2.Visible = false;
        if(!Page.IsPostBack)
        {SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        try
        {

            DataTable dt;

            conn.Open();
            //Get All Indients with CSM 
         


            string strSelect = "SELECT Distinct [GRP Group Name] as 'GroupName' FROM [Expedite].[dbo].[Group_Mail]";
            // string strSelect = "Select * From [dbo].['All_Incidents'] ;";
            //   SqlCommand cmd = new SqlCommand(strSelect, cnn);
            SqlDataAdapter adpt = new SqlDataAdapter(strSelect, conn);

            dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataTextField = "GroupName";
            DropDownList1.DataValueField = "GroupName";
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.Items.Add(new ListItem("Type Team Name ..", "none"));
            DropDownList1.SelectedIndex = DropDownList1.Items.Count-1;

            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }

        Label_ModifiedDateExcel.Text = "Last Modified Date of Excel " + GetLastModifiedDate();
        Label_ModifiedDateExe.Text = "Last Modified Date of Script " + GetLastModifiedDateExe();
    }
        
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "none")
        {
            Response.Redirect("EditTeamMail.aspx?param=" + DropDownList1.SelectedValue + "");
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "Please Select A Group";
        }
    }
    string GetLastModifiedDate()
    {
        return System.IO.File.GetLastWriteTime("D:/Expedite/NewExpedite.xls").ToString();
    }

    string GetLastModifiedDateExe()
    {
        SqlCommand command = new SqlCommand();
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