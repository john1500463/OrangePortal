using System;
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
    bool isdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList1.Visible = false;
        TextBox2.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != null)
        {
            String searchid = TextBox1.Text;
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


               //dt.Columns.Add(new DataColumn("Comments", typeof(String)));
               //dt.Columns.Add(new DataColumn("Comments", typeof(System.Windows.Forms.TextBox)));
               // System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox =  TextBox1 ;
               //var Textbox1 = new System.Windows.Forms.TextBox();
               // dt.Rows[0][7] = new System.Windows.Forms.TextBox();
               //TextBox dynamicTextBox = new TextBox();
               //dt.Rows[0][0] = dynamicTextBox;
               //DataControlField asd = (DataControlField)titleColumn;
               // GridView1.DataSource = dt;
             
               //dv.DataBindings.Add(dt);
               
        //foreach (DataColumn dc in dt.Columns) {

        //dv.Columns.Add(new DataGridViewTextBoxColumn());

        //}
        //foreach(DataRow dr in dt.Rows) {

        //    dv.Rows.Add(dr.ItemArray);

        //    }
                //GridView1.Columns.Add(titleColumn);

              // TemplateField tf = new TemplateField();
               //tf.HeaderTemplate = new GridViewLabelTemplate(DataControlRowType.Header, "Col1", "Int32");
               //tf.ItemTemplate = new GridViewLabelTemplate(DataControlRowType.DataRow, "Col1", "Int32");
               //GridView1.Columns.Add(tf);

        //var bindingSource = new BindingSource();
        //bindingSource.DataSource = dt;
        //dv.DataSource = bindingSource;
        //bindingSource.ResetBindings(true);

               //dt.Columns.Add("MyStringColumn", typeof(string));
               //dt.Columns.Add("MyTextBoxColumn", typeof(TextBox));
               //dt.Rows.Add("Row 1 Text", textBox1);
            
               
               GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
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
                    //Debug.Write("entered");
                    SqlCommand command2 = new SqlCommand();
                    command2.Connection = conn;
                    command2.CommandText = "Select [INC Incident Number],[INC Status], [AG Assigned Group Name], [AG Assignee] From [Expedite].[dbo].['All_Incidents'] Where [Expedite].[dbo].['All_Incidents'].[INC Incident Number]='" + searchid + "';";
                    Debug.Write(command2.CommandText);
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
                    GridView1.Visible = true;

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.Write(ex.ToString());
            }
            DropDownList1.Visible = true;
            TextBox2.Visible = true;
            
        }
    }

    protected DataTable setupview(DataTable dt)
    {
        //DataColumn test = new DataColumn();
        //Debug.Write(dt.Rows[0][1]);
        //if(dt.Rows[0][
       // TemplateField tf = new TemplateField();
        //tf.HeaderTemplate = new GridViewLabelTemplate(DataControlRowType.Header, "Col1", "Int32");
        //tf.ItemTemplate = new GridViewLabelTemplate(DataControlRowType.DataRow, "Col1", "Int32");
        //TextBox commenttextbox = new TextBox();

        //GridView1.Rows.Add(commenttextbox);
       // dt.Columns.Add(commenttextbox);
        return null;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
    }
}