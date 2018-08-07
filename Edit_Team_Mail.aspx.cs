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
            //DropDownList1.Items.Add(new ListItem("Type group","none"));

            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }}
        
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditTeamMail.aspx?param=" + DropDownList1.SelectedValue + "");
    }
}