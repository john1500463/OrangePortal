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
            }


        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }
}