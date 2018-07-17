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

public partial class Expedite_multiple_incidents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        //DropDownList1.Visible = false;
        //Expedite_Button.Visible = false;
        updatedropdown();
    }
    protected void updatedropdown()
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
        DropDownList1.Items.Add(new ListItem("-None-"));
        for (int i = 0; i < ar.Count; i++)
        {
            DropDownList1.Items.Add(new ListItem(ar[i].ToString()));
        }
    }
    protected void expedite(String id)
    {
        bool isexpedited = false;
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
                //Debug.Write("not expedited");
                isexpedited = false;
            }
            else
            {
                isexpedited = true;
               // Debug.Write("expedited");
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
        Debug.Write("first0");

        try
        {
            String thereason = DropDownList1.SelectedItem.Value;
            DataTable dt2 = new DataTable();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = conn;
            if (!isexpedited) //exists only in all inc
            {
                command2.CommandText = "INSERT INTO [Expedite].[dbo].[Expedite_time] (Incident_ID,Expedite_Date,Urgency_Reason,Expedite_By) VALUES ('" + id + "','" + DateTime.Now.ToString() + "','" + thereason + "','"+ x +"');";
                //Debug.Write("first");
            }
            else
            {
                command2.CommandText = "UPDATE [Expedite].[dbo].[Expedite_time] SET Urgency_Reason = '" + thereason + "',Expedite_By='"+ x +"' Where [Incident_ID] = '" + id + "';";
                Debug.Write(" reason " + thereason + " id " + id);
            }
        using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command2;
                using (dt2 = new DataTable())
                {
                    sda.Fill(dt2);
                }
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
    protected void Expedite_Button_Click(object sender, EventArgs e)
    {
        String idstring = TextBox_id.Text;
        //Debug.Write(idsstring);
        String idstring2 = idstring.Replace(" ", "");
        String[] ids = idstring2.Split(',');
        foreach (String id in ids)
        {
            //Debug.Write(id + " ");
            expedite(id);
        }

 
    }
    protected void Search_Button_Click(object sender, EventArgs e)
    {
        ArrayList arr = new ArrayList();
        String idstring = TextBox_id.Text;
        String idstring2 = idstring.Replace(" ", "");
        String[] ids = idstring2.Split(',');
        foreach (String id in ids)
        {
            arr.Add(id);
        }

        showgrid(arr);
    }
    protected void showgrid(ArrayList arr)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            String cmdstring = "Select [INC Incident Number] as 'Incident Number',[INC Status] as 'Status',[INC DS Submit Date] as 'Submit Date',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified' From [dbo].['All_Incidents'] where [INC Incident Number]='" + arr[0] + "'";
                
            foreach(String idstring in arr){
                cmdstring +=" or [INC Incident Number]='" +idstring+ "'";
            }
            cmdstring += ";";
            command.CommandText = cmdstring;
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";

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
                //Label1.Text = "Incident doesn't exist";
                GridView1.Visible = false;
                // Label1.Text = "";

            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;

            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }
    protected void Clear_Button_Click(object sender, EventArgs e)
    {
        TextBox_id.Text = "";
        ListItem selectedListItem = DropDownList1.Items.FindByValue("-None-");
        if (selectedListItem != null)
        {
            //Debug.Write("madeit");
            //DropDownList1.Visible = false;
            DropDownList1.SelectedItem.Selected = false;
            selectedListItem.Selected = true;
            //DropDownList1.Visible = true;
        }
    }
}