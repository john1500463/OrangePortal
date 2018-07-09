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
    public Boolean isdt;
    public Boolean isdt2;
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
            int indexofreason = 0;
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
                dt.Columns.Add("comment", typeof(string));
               GridView1.DataSource = dt;
                GridView1.DataBind();
                DropDownList1.Visible = false;
                updatedropdown(2);
                indexofreason = 5;
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
                    dt2.Columns.Add("Urgency Reason", typeof(string));
                    dt2.Columns.Add("comment", typeof(string));
                    GridView1.DataSource = dt2;
                    GridView1.DataBind();
                    updatedropdown(1);
                    indexofreason = 0;
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
                    //Debug.Write(command3.CommandText);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = command3;
                        using (dt3 = new DataTable())
                        {
                            sda.Fill(dt3);
                        }
                    }
                    dt3.Columns.Add("comment", typeof(string));
                    GridView1.DataSource = dt3;
                    GridView1.DataBind();
                    updatedropdown(2);
                    indexofreason = 2;
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
            if (indexofreason != 0)
            {
                GridView1.Rows[0].Cells[indexofreason].Controls.Add(DropDownList1);
                int temp =  GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells[temp-1].Controls.Add(TextBox2);
            }
            else
            {

                int temp = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells[temp-2].Controls.Add(DropDownList1);
                GridView1.Rows[0].Cells[temp - 1].Controls.Add(TextBox2);

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
            //Debug.Write("Header is "+GridView1.Columns[0].HeaderText.ToString());
            
            //Debug.Write("Header is " + GridView1.HeaderRow.Cells[0].Text.ToString());
            //Debug.Write(DateTime.Now.ToString());
            if (GridView1.HeaderRow.Cells[2].Text.ToString()=="AG Assigned Group Name") //exists only in all inc
            {
                command.CommandText = "INSERT INTO [Expedite].[dbo].[Expedite_time] (Incident_ID,Expedite_Date,Urgency_Reason,Comment) VALUES ('" + TextBox1.Text + "','" + DateTime.Now.ToString() + "','" + thereason + "' , '" + TextBox2.Text + "');";
                Debug.Write("first");
            }
            else{
                command.CommandText = "UPDATE [Expedite].[dbo].[Expedite_time] SET Urgency_Reason = '" + thereason + "', Comment='" + TextBox2.Text + "' Where [Incident_ID] = '" + TextBox1.Text + "';";
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
      //  if (num == 1)
      //  {
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
                if (selectedListItem != null)
                {
                    //Debug.Write("madeit");
                    selectedListItem.Selected = true;
                }
            }
            catch (Exception ex)
            {
                conn2.Close();
                Console.Write(ex.ToString());
            }


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