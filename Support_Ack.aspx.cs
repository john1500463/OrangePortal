using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Support_Ack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select * From [Expedite].[dbo].[Expedite_time] where Expedite_By='" + x + "';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            //dt.Columns.Add(new DataColumn("Acknowledge", typeof(bool)));
           /* DataColumn dc1 = new DataColumn("PageName");
            dt.Columns.Add(dc1);

            foreach (var item in RoleName)
            {
                DataColumn dc = new DataColumn(item.RoleName, typeof(bool));
                dt.Columns.Add(dc);
            }

            foreach (var page in pageName)
            {
                DataRow dr = dt.NewRow();
                dr["PageName"] = page.PAGE_NAME;

                foreach (var role in RoleName)
                {
                    dr[role.RoleName] = true;
                }
                dt.Rows.Add(dr);
            }    
            */
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
}