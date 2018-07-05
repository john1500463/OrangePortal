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

public partial class Incidents_to_expedite : System.Web.UI.Page
{
    public bool isdt;
    public bool isdt2;
    String searchingid;
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList1.Visible = false;
        TextBox2.Visible = false;
        Button3.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != null)
        {
            isdt = true;
            String searchid = TextBox1.Text;
            searchingid = searchid;
            //Debug.Write(searchid);
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
            String x = (string)(Session["FTID"]);
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "Select [INC Incident Number],[INC Status],[Submit_Date], [AG Assigned Group Name], [AG Assignee],[Urgency_Reason],[Expedite_Date] From [Expedite].[dbo].['All_Incidents'] INNER JOIN  [Expedite].[dbo].[Expedite_time] ON [Expedite].[dbo].['All_Incidents'].[INC Incident Number] =  [Expedite].[dbo].[Expedite_time].[Incident_ID] Where [Expedite].[dbo].[Expedite_time].[Incident_ID]='" + searchid + "';";
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
                updatedropdown();
                DropDownList1.Visible = true;
                GridView1.Visible = true;
                TextBox2.Visible = true;
                Button3.Visible = true;
            }
            catch(Exception ex)
            {
            conn.Close();
            Console.Write(ex.ToString());
            }
            try
            {
                //Debug.Write("onit");
                DataTable dt2 = new DataTable();
                if (GridView1.Rows.Count == 0)
                {
                    isdt = false;
                    isdt2 = true;
                    //Debug.Write("entered");
                    SqlCommand command2 = new SqlCommand();
                    command2.Connection = conn;
                    command2.CommandText = "Select [INC Incident Number],[INC Status], [AG Assigned Group Name], [AG Assignee] From [Expedite].[dbo].['All_Incidents'] Where [Expedite].[dbo].['All_Incidents'].[INC Incident Number]='" + searchid + "';";
                    //Debug.Write(command2.CommandText);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = command2;
                        using (dt2 = new DataTable())
                        {
                            sda.Fill(dt2);
                        }
                    }
                    GridView1.DataSource = dt2;
                    GridView1.DataBind();
                    updatedropdown();
                    GridView1.Visible = true;
                    DropDownList1.Visible = true;
                    TextBox2.Visible = true;
                    Button3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }
            try
            {
                //Debug.Write("onit");
                DataTable dt3 = new DataTable();
                if (GridView1.Rows.Count == 0)
                {
                    isdt = false;
                    isdt2 = false;
                    //Debug.Write("entered");
                    SqlCommand command3 = new SqlCommand();
                    command3.Connection = conn;
                    command3.CommandText = "Select [Incident_ID], [Submit_Date],[Urgency_Reason],[Expedite_Date] From [Expedite].[dbo].[Expedite_time] Where [Expedite_time].[Incident_ID]='" + searchid + "';";
                    Debug.Write(command3.CommandText);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = command3;
                        using (dt3 = new DataTable())
                        {
                            sda.Fill(dt3);
                        }
                    }
                    GridView1.DataSource = dt3;
                    GridView1.DataBind();
                    updatedropdown();
                    GridView1.Visible = true;
                    TextBox2.Visible = true;
                    Button3.Visible = true;
                    DropDownList1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }

        }
    }
    protected void post_reason()
    {
        String thereason = DropDownList1.SelectedItem.Value;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            if (isdt2)
            {
                Debug.Write("dt2");
            }
            if (isdt2 && !isdt) //exists only in all inc
            {
                command.CommandText = "INSERT INTO [Expedite].[dbo].[Expedite_time] (Incident_ID,Expedite_Date,Urgency_Reason,Comment) VALUES ('" + searchingid + "','" + DateTime.Now.ToString() + "','" + thereason + "' , '" + TextBox2.Text + "');";
                //Debug.Write("first");
            }
            else{
                command.CommandText = "UPDATE [Expedite].[dbo].[Expedite_time] SET Urgency_Reason = '"+ thereason + "', Comment='" + TextBox2.Text + "' Where [Incident_ID] = '" + searchingid + "';";
                //Debug.Write(thereason);
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

    protected void updatedropdown(){
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
            for (int i = 0; i<ar.Count; i++)
            {
                DropDownList1.Items.Add(new ListItem(ar[i].ToString()));
            }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //post_comment();
        post_reason();
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