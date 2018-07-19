using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User : System.Web.UI.Page
{
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Button Edit;
        Button Delete;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
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
                    Edit.ID = "ID" + i;
                    Edit.Click += new EventHandler(this.GreetingBtn_Click);
                    
                    Edit.Text = "Edit";
                    Delete.Text = "Delete";
                    GridView1.Rows[i].Cells[4].Controls.Add(Edit);
                    GridView1.Rows[i].Cells[5].Controls.Add(Delete);
                }
              

            //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";

        }
        // Response.Write("ya3");
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    void GreetingBtn_Click(Object sender,
                          EventArgs e)
    {
        // When the button is clicked,
        // change the button text, and disable it.

        Button clickedButton = (Button)sender;
        Debug.WriteLine(clickedButton.ID);
       // int id= (int)clickedButton.ID;
      //  String Y = dt.Rows[id][3].ToString();
      //  Debug.WriteLine(Y);
        //Response.Redirect("ExpeditePage.aspx?param1=" + Y);

        // Display the greeting label text.
        
    }


}