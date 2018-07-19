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

public partial class Urgency_Reasons : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fill_dropdown();
            TextBox1.Width = 500;
            TextBox1.Visible = false;
            Button_Save.Visible = false;
            Label_info.Visible = false;
            Label_Urgency_chosen.Visible = false;
            Button_newreason.Visible = false;
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
    }
    protected void Button_Update_Click(object sender, EventArgs e)
    {
        TextBox1.Text = DropDownList1.SelectedItem.Value;
        TextBox1.Visible = true;
        Button_Save.Visible = true;
        Label_Urgency_chosen.Text = ": " + DropDownList1.SelectedItem.Value;
        Label_Urgency_chosen.Visible = true;
        DropDownList1.Visible = false;
        Button_Update.Visible = false;
        Button_Delete.Visible = false;
        Button_newreason.Visible = true;
    }
    protected void Button_Delete_Click(object sender, EventArgs e)
    {

    }
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        changeurgencyreason(DropDownList1.SelectedItem.Value,TextBox1.Text);
        changeurgencyreason_exp(DropDownList1.SelectedItem.Value, TextBox1.Text);
        Label_info.Text = "Sucessfully Updated!";
        Label_info.Visible = true;
        TextBox1.Visible = false;
        Button_Save.Visible = false;
    }
    protected void changeurgencyreason(String From, String To)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt3 = new DataTable();
            conn.Open();
            SqlCommand command3 = new SqlCommand();
            command3.Connection = conn;
            command3.CommandText = "UPDATE [Expedite].[dbo].[UrgencyReasons] SET UrgencyReason='" + To + "' Where [UrgencyReason]='" + From + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command3;
                using (dt3 = new DataTable())
                {
                    sda.Fill(dt3);
                }

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
    protected void changeurgencyreason_exp(String From, String To)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt3 = new DataTable();
            conn.Open();
            SqlCommand command3 = new SqlCommand();
            command3.Connection = conn;
            command3.CommandText = "UPDATE [Expedite].[dbo].[UrgencyReasons] SET Urgency_Reason='" + To + "' Where [Urgency_Reason]='" + From + "';";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command3;
                using (dt3 = new DataTable())
                {
                    sda.Fill(dt3);
                }

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }

    protected void Button_newreason_Click(object sender, EventArgs e)
    {
        Response.Redirect("Urgency_Reasons.aspx");
    }
}