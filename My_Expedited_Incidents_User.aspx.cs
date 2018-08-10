﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;

public partial class My_Expedited_Incidents_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (((String)Session["Right"]) == "S")
        {
            Response.Redirect("Home_Page_Support.aspx");
        }
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [Incident_ID] as 'Incident ID' ,[INC Tier 2] as 'Tier 2',[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[INC DS Last Modified Date] as 'Last Modified Date',[Expedite_Date] as 'Expedited Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].['All_Incidents'] FULL OUTER JOIN  [Expedite].[dbo].[Expedite_time]on [Incident_ID]=[INC Incident Number] where Expedite_By='" + x + "' and [INC Status]!='Closed' ORDER BY " + DropDownList1.SelectedValue + ";";
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
                Label_info.Visible = true;
            }
            else
            {
                Label_info.Visible = false;
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
            //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                HyperLink hlContro = new HyperLink();
                hlContro.NavigateUrl = "./Incident_Details_User.aspx?ID=" + GridView1.Rows[i].Cells[0].Text;
                hlContro.Text = GridView1.Rows[i].Cells[0].Text;
                GridView1.Rows[i].Cells[0].Controls.Add(hlContro);
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {

    }
}