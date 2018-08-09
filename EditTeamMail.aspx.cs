using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrangePortal_EditTeamMail : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
    DataTable dt;
    SqlDataAdapter adpt;
    String TeamName;
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
        Label5.Visible = false;
        try
        {
            conn.Open();
            //Get All Indients with CSM 

            TeamName = Request.QueryString["param"];

            string strSelect = "SELECT * FROM [Expedite].[dbo].[Group_Mail] where [GRP Group Name]='" + TeamName + "' ";

            adpt = new SqlDataAdapter(strSelect, conn);
            dt = new DataTable();
            adpt.Fill(dt);

            TextBox1.Enabled = false;
            TextBox2.Enabled = false;
            TextBox1.Text = TeamName;
            TextBox2.Text = dt.Rows[0][1].ToString();

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
        try
        {
            conn.Open();
            //Get All Indients with CSM 

            String TeamName = Request.QueryString["param"];
            String NewMail = TextBox3.Text;
            if (NewMail == "") {
                Label5.Visible = true;
                Label5.Text = "Empty New Mail";
            }
            else if (!NewMail.Contains("@"))
            {
                Label5.Visible = true;
                Label5.Text = "Email Doesn't Contain @";
            }
            else {
                string strSelect = "Update [Expedite].[dbo].[Group_Mail] SET [mail]='" + NewMail + "' Where  [GRP Group Name]='" + TeamName + "' ";

            adpt = new SqlDataAdapter(strSelect, conn);
            dt = new DataTable();
            adpt.Fill(dt);
            conn.Close();
            Response.Redirect("Home_Page.aspx");
            }


        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
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