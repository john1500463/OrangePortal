using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModifyUser : System.Web.UI.Page
{
    DataTable dt;
    SqlConnection conn;
    SqlCommand command;
    String Role;
    protected void Page_Load(object sender, EventArgs e)
    {
        Button Edit;
        Button Delete;
        conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [Username] as 'User Name' ,Rights as 'Role' , Email, FTID From [Expedite].[dbo].[Users]";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";


            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;

                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }

            dt.Columns.Add(new DataColumn("Edit", typeof(string)));
            dt.Columns.Add(new DataColumn("Delete", typeof(string)));
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;  

            
            int num = dt.Rows.Count;
            Debug.WriteLine(num);
            for (int i = 0; i < num; i++)
            {
                Edit = new Button();
                Delete = new Button();
                Edit.ID = i.ToString();
                Edit.Click += new EventHandler(this.EditingBtn_Click);
                Delete.ID = (i + num).ToString();
                Delete.Click += new EventHandler(this.DeleteBtn_Click);

                Edit.Text = "Edit";
                Delete.Text = "Delete";
                GridView1.Rows[i].Cells[4].Controls.Add(Edit);
                GridView1.Rows[i].Cells[5].Controls.Add(Delete);
                
                Role= GridView1.Rows[i].Cells[1].ToString();
                Debug.WriteLine(Role);
                
            }
          
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }
    void EditingBtn_Click(Object sender,
                          EventArgs e)
    {
        // When the button is clicked,
        // change the button text, and disable it.

        Button clickedButton = (Button)sender;
        int ID = Int32.Parse(clickedButton.ID);
        String FTID = dt.Rows[ID][3].ToString();
        Debug.WriteLine(FTID);
        // int id= (int)clickedButton.ID;
        //  String Y = dt.Rows[id][3].ToString();
        //  Debug.WriteLine(Y);
        Response.Redirect("EditUser.aspx?param1=" + FTID);

        // Display the greeting label text.

    }

    void DeleteBtn_Click(Object sender,
                         EventArgs e)
    {
        // When the button is clicked,
        // change the button text, and disable it.

        Button clickedButton = (Button)sender;
        int ID = Int32.Parse(clickedButton.ID);
        Debug.WriteLine(ID);
        ID = ID - dt.Rows.Count;
        String FTID = dt.Rows[ID][3].ToString();
        Debug.WriteLine(FTID);

        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        DataTable dt1 = new DataTable();
        conn.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.CommandText = "Delete From [Expedite].[dbo].[Users] Where [FTID]= '" + FTID + "';";

        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt1 = new DataTable())
            {

                sda.Fill(dt1);

            }
        }
        Response.Redirect("ModifyUser.aspx");


    }
}
    
