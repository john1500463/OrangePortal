using System;
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
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
<<<<<<< HEAD
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
=======
using System.Web.UI;
using System.Web.UI.WebControls;

>>>>>>> 12da84a36095d0f443d20bae281807521504f151
public partial class Expedited_Incidents : System.Web.UI.Page
{   
    // THE SELECT YOU'LL NEED
    //Select [INC Incident Number],[INC Tier 2],[INC Status], [AG Assigned Group Name], [AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason] From [Expedite].[dbo].['All_Incidents'] as AL INNER JOIN [Expedite].[dbo].[Expedite_time] as ET ON AL.[INC Incident Number] =  ET.[Incident_ID];
    public static DataTable thetable;
    DataTable dt;
    String Inc1;
    String Inc2;
    int num;
    protected void exporttoxls(DataTable dt)
    {

        //Create a dummy GridView and Bind the data source we have.
        GridView grdExportData = new GridView();

        grdExportData.AllowPaging = false;
        grdExportData.DataSource = dt;
        grdExportData.DataBind();

        //Clear the response and add the content types and headers to it.
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MyReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        //We need this string writer and HTML writer in order to render the grid inside it.
        StringWriter swExportData = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(swExportData);

        //Lets render the Grid inside the HtmlWriter and then automatically we will have it converted into eauivalent string.
        grdExportData.RenderControl(hw);

        //Write the response now and you will get your excel sheet as download file
        Response.Output.Write(swExportData.ToString());
        Response.Flush();
        Response.End();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            //Response.Redirect("Default.aspx");
        }
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
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
            if (!Page.IsPostBack)
            {
                thetable = new DataTable();
                thetable = dt;
            }
            GridView1.Visible = true;
            dt.Columns.Add(new DataColumn("Esclate 1", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 2", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 3", typeof(string)));
            System.Web.UI.WebControls.Button Esclate1;
            System.Web.UI.WebControls.Button Esclate2;
            System.Web.UI.WebControls.Button Esclate3;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            num = dt.Rows.Count;

            DataTable dt1 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=1; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt1 = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }



            DataTable dt2 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=2; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt2 = new DataTable())
                {

                    sda.Fill(dt2);

                }
            }

            DataTable dt3 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=3; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt3 = new DataTable())
                {

                    sda.Fill(dt3);

                }
            }



            for (int i = 0; i < num; i++)
            {


                //  Esclate2.Click += new EventHandler(this.EditingBtn_Click);

                //  Delete.Click += new EventHandler(this.DeleteBtn_Click);
                Esclate1 = new System.Web.UI.WebControls.Button();
                Esclate2 = new System.Web.UI.WebControls.Button();
                Esclate3 = new System.Web.UI.WebControls.Button();
                Esclate1.ID = (i).ToString();
                Esclate2.ID = (i + num).ToString();
                Esclate3.ID = (i + num + num).ToString();
                Esclate1.Click += new EventHandler(this.EditingBtn_Click);
                Esclate2.Click += new EventHandler(this.Esclate2_Click);
                Esclate3.Click += new EventHandler(this.Esclate3_Click);
                  for (int j = 0; j < dt1.Rows.Count;j++ )
                {
                    Inc1 = (String) dt.Rows[i][0] ;
                    Inc2 = (String) dt1.Rows[j][0] ;
                    
                    if (Inc1 == Inc2)
                    {
                        Esclate1.BackColor = System.Drawing.Color.Red; 
                    
                    }
                }
                  for (int j = 0; j < dt2.Rows.Count; j++)
                  {
                      Inc1 = (String)dt.Rows[i][0];
                      Inc2 = (String)dt2.Rows[j][0];
                     
                      if (Inc1 == Inc2)
                      {
                          Esclate2.BackColor = System.Drawing.Color.Green;

                      }
                  
                  }

                  for (int j = 0; j < dt3.Rows.Count; j++)
                  {
                      Inc1 = (String)dt.Rows[i][0];
                      Inc2 = (String)dt3.Rows[j][0];
                      
                      if (Inc1 == Inc2)
                      {
                          Esclate3.BackColor = System.Drawing.Color.Blue;

                      }

                  }


                Esclate1.Text = "Esclate 1";
                Esclate2.Text = "Esclate 2";
                Esclate3.Text = "Esclate 3";
                GridView1.Rows[i].Cells[9].Controls.Add(Esclate1);
                GridView1.Rows[i].Cells[10].Controls.Add(Esclate2);
                GridView1.Rows[i].Cells[11].Controls.Add(Esclate3);

            }
            

        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }

    protected void Calendar1_SelectionChanged(object sender, System.EventArgs e)
    {
        String SelectedData = Calendar1.SelectedDate.ToShortDateString();
        string startdated = (Convert.ToDateTime(SelectedData)).ToString("yyyy/MM/dd");
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Submit Date]) <='" + startdated + "';";
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [Expedite_Date]) <='" + startdated + "';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            dt.Columns.Add(new DataColumn("Esclate 1", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 2", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 3", typeof(string)));
            System.Web.UI.WebControls.Button Esclate1;
            System.Web.UI.WebControls.Button Esclate2;
            System.Web.UI.WebControls.Button Esclate3;
            GridView1.DataSource = dt;
            GridView1.DataBind();
                    num = dt.Rows.Count;

            DataTable dt1 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=1; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt1 = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }



            DataTable dt2 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=2; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt2 = new DataTable())
                {

                    sda.Fill(dt2);

                }
            }

            DataTable dt3 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=3; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt3 = new DataTable())
                {

                    sda.Fill(dt3);

                }
            }



            for (int i = 0; i < num; i++)
            {


                //  Esclate2.Click += new EventHandler(this.EditingBtn_Click);

                //  Delete.Click += new EventHandler(this.DeleteBtn_Click);
                Esclate1 = new System.Web.UI.WebControls.Button();
                Esclate2 = new System.Web.UI.WebControls.Button();
                Esclate3 = new System.Web.UI.WebControls.Button();
                Esclate1.ID = (i).ToString();
                Esclate2.ID = (i + num).ToString();
                Esclate3.ID = (i + num + num).ToString();
                Esclate1.Click += new EventHandler(this.EditingBtn_Click);
                Esclate2.Click += new EventHandler(this.Esclate2_Click);
                Esclate3.Click += new EventHandler(this.Esclate3_Click);
                  for (int j = 0; j < dt1.Rows.Count;j++ )
                {
                    Inc1 = (String) dt.Rows[i][0] ;
                    Inc2 = (String) dt1.Rows[j][0] ;
                    
                    if (Inc1 == Inc2)
                    {
                        Esclate1.BackColor = System.Drawing.Color.Red; 
                    
                    }
                }
                  for (int j = 0; j < dt2.Rows.Count; j++)
                  {
                      Inc1 = (String)dt.Rows[i][0];
                      Inc2 = (String)dt2.Rows[j][0];
                     
                      if (Inc1 == Inc2)
                      {
                          Esclate2.BackColor = System.Drawing.Color.Green;

                      }
                  
                  }

                  for (int j = 0; j < dt3.Rows.Count; j++)
                  {
                      Inc1 = (String)dt.Rows[i][0];
                      Inc2 = (String)dt3.Rows[j][0];
                      
                      if (Inc1 == Inc2)
                      {
                          Esclate3.BackColor = System.Drawing.Color.Blue;

                      }

                  }


                Esclate1.Text = "Esclate 1";
                Esclate2.Text = "Esclate 2";
                Esclate3.Text = "Esclate 3";
                GridView1.Rows[i].Cells[9].Controls.Add(Esclate1);
                GridView1.Rows[i].Cells[10].Controls.Add(Esclate2);
                GridView1.Rows[i].Cells[11].Controls.Add(Esclate3);

            }
            

        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    
    }
    protected void Calendar2_SelectionChanged(object sender, System.EventArgs e)
    {
        String SelectedData2 = Calendar2.SelectedDate.ToShortDateString();
        string startdated2= (Convert.ToDateTime(SelectedData2)).ToString("yyyy/MM/dd");
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }

            dt.Columns.Add(new DataColumn("Esclate 1", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 2", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 3", typeof(string)));
            System.Web.UI.WebControls.Button Esclate1;
            System.Web.UI.WebControls.Button Esclate2;
            System.Web.UI.WebControls.Button Esclate3;
            GridView1.DataSource = dt;
            GridView1.DataBind();
                     num = dt.Rows.Count;

            DataTable dt1 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=1; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt1 = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }



            DataTable dt2 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=2; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt2 = new DataTable())
                {

                    sda.Fill(dt2);

                }
            }

            DataTable dt3 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=3; ";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt3 = new DataTable())
                {

                    sda.Fill(dt3);

                }
            }



            for (int i = 0; i < num; i++)
            {


                //  Esclate2.Click += new EventHandler(this.EditingBtn_Click);

                //  Delete.Click += new EventHandler(this.DeleteBtn_Click);
                Esclate1 = new System.Web.UI.WebControls.Button();
                Esclate2 = new System.Web.UI.WebControls.Button();
                Esclate3 = new System.Web.UI.WebControls.Button();
                Esclate1.ID = (i).ToString();
                Esclate2.ID = (i + num).ToString();
                Esclate3.ID = (i + num + num).ToString();
                Esclate1.Click += new EventHandler(this.EditingBtn_Click);
                Esclate2.Click += new EventHandler(this.Esclate2_Click);
                Esclate3.Click += new EventHandler(this.Esclate3_Click);
                  for (int j = 0; j < dt1.Rows.Count;j++ )
                {
                    Inc1 = (String) dt.Rows[i][0] ;
                    Inc2 = (String) dt1.Rows[j][0] ;
                    
                    if (Inc1 == Inc2)
                    {
                        Esclate1.BackColor = System.Drawing.Color.Red; 
                    
                    }
                }
                  for (int j = 0; j < dt2.Rows.Count; j++)
                  {
                      Inc1 = (String)dt.Rows[i][0];
                      Inc2 = (String)dt2.Rows[j][0];
                     
                      if (Inc1 == Inc2)
                      {
                          Esclate2.BackColor = System.Drawing.Color.Green;

                      }
                  
                  }

                  for (int j = 0; j < dt3.Rows.Count; j++)
                  {
                      Inc1 = (String)dt.Rows[i][0];
                      Inc2 = (String)dt3.Rows[j][0];
                      
                      if (Inc1 == Inc2)
                      {
                          Esclate3.BackColor = System.Drawing.Color.Blue;

                      }

                  }


                Esclate1.Text = "Esclate 1";
                Esclate2.Text = "Esclate 2";
                Esclate3.Text = "Esclate 3";
                GridView1.Rows[i].Cells[9].Controls.Add(Esclate1);
                GridView1.Rows[i].Cells[10].Controls.Add(Esclate2);
                GridView1.Rows[i].Cells[11].Controls.Add(Esclate3);

            }
            

        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }
    
    protected void TextBox1_TextChanged(object sender, System.EventArgs e)
    {

    }
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        
        TextBox1.Text = "";
    }

    protected void Button3_Click(object sender, System.EventArgs e)
    {
        //Debug.Write("When posting: " + GridView1.Rows[0].Cells[0].Text);
        //Debug.Write("When posting: " + thetable.Rows[0][0].ToString());
        exporttoxls(thetable);
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        String ManagerName = TextBox1.Text.ToString();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "Select [INC Incident Number] as 'Incident ID',[INC Tier 2] as 'Tier 2' ,[INC Status] as 'Status',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified Date',[Expedite_Date] as 'Expedite Date',[Urgency_Reason] as 'Urgency Reason' From [Expedite].[dbo].[Expedite_time] as A inner join [Expedite].[dbo].['All_Incidents'] as B  on A.[Incident_ID]=B.[INC Incident Number] and [INC Status]!='Closed' and [INC Status]!='Resolved' and [AG Assignee Manager Name]='" + ManagerName + "';";
            //   command.CommandText = "Select * From [dbo].['All_Incidents'];";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }

            dt.Columns.Add(new DataColumn("Esclate 1", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 2", typeof(string)));
            dt.Columns.Add(new DataColumn("Esclate 3", typeof(string)));
            System.Web.UI.WebControls.Button Esclate1;
            System.Web.UI.WebControls.Button Esclate2;
            System.Web.UI.WebControls.Button Esclate3;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            num = dt.Rows.Count;

            DataTable dt1 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=1; ";

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt1 = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }



            DataTable dt2 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=2; ";

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt2 = new DataTable())
                {

                    sda.Fill(dt2);

                }
            }

            DataTable dt3 = new DataTable();

            command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "SELECT  [Incident Number] FROM [Expedite].[dbo].[Esclation] Where [NumberOfEsclation]=3; ";

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt3 = new DataTable())
                {

                    sda.Fill(dt3);

                }
            }



            for (int i = 0; i < num; i++)
            {


                //  Esclate2.Click += new EventHandler(this.EditingBtn_Click);

                //  Delete.Click += new EventHandler(this.DeleteBtn_Click);
                Esclate1 = new System.Web.UI.WebControls.Button();
                Esclate2 = new System.Web.UI.WebControls.Button();
                Esclate3 = new System.Web.UI.WebControls.Button();
                Esclate1.ID = (i).ToString();
                Esclate2.ID = (i + num).ToString();
                Esclate3.ID = (i + num + num).ToString();
                Esclate1.Click += new EventHandler(this.EditingBtn_Click);
                Esclate2.Click += new EventHandler(this.Esclate2_Click);
                Esclate3.Click += new EventHandler(this.Esclate3_Click);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Inc1 = (String)dt.Rows[i][0];
                    Inc2 = (String)dt1.Rows[j][0];

                    if (Inc1 == Inc2)
                    {
                        Esclate1.BackColor = System.Drawing.Color.Red;

                    }
                }
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    Inc1 = (String)dt.Rows[i][0];
                    Inc2 = (String)dt2.Rows[j][0];

                    if (Inc1 == Inc2)
                    {
                        Esclate2.BackColor = System.Drawing.Color.Green;

                    }

                }

                for (int j = 0; j < dt3.Rows.Count; j++)
                {
                    Inc1 = (String)dt.Rows[i][0];
                    Inc2 = (String)dt3.Rows[j][0];

                    if (Inc1 == Inc2)
                    {
                        Esclate3.BackColor = System.Drawing.Color.Blue;

                    }

                }


                Esclate1.Text = "Esclate 1";
                Esclate2.Text = "Esclate 2";
                Esclate3.Text = "Esclate 3";
                GridView1.Rows[i].Cells[9].Controls.Add(Esclate1);
                GridView1.Rows[i].Cells[10].Controls.Add(Esclate2);
                GridView1.Rows[i].Cells[11].Controls.Add(Esclate3);

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

     System.Web.UI.WebControls.Button clickedButton = ( System.Web.UI.WebControls.Button)sender;
         int IncNum = Int32.Parse(clickedButton.ID);
        
        
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt1 = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Insert into [Expedite].[dbo].[Esclation]([Incident Number],[DateEsclated],[NumberOfEsclation]) Values ('" + dt.Rows[IncNum][0] + "',GETDATE(),'1')";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }
            Response.Redirect("Expedited_Incidents.aspx");
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }


    void Esclate2_Click(Object sender,
                        EventArgs e)
    {
        // When the button is clicked,
        // change the button text, and disable it.

        System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
        int IncNum = Int32.Parse(clickedButton.ID);
        IncNum= IncNum - num;

        
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt1 = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Insert into [Expedite].[dbo].[Esclation]([Incident Number],[DateEsclated],[NumberOfEsclation]) Values ('" + dt.Rows[IncNum][0] + "',GETDATE(),'2')";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }
            Response.Redirect("Expedited_Incidents.aspx");
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }


    void Esclate3_Click(Object sender,
                        EventArgs e)
    {
        // When the button is clicked,
        // change the button text, and disable it.

        System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sender;
        int IncNum = Int32.Parse(clickedButton.ID);
        IncNum = IncNum - num- num;

        
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        try
        {
            DataTable dt1 = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            //command.CommandText = "Select [INC Incident Number],[INC Tier 2] ,[INC Status],[AG Assigned Group Name],[AG Assignee],[INC DS Last Modified Date],[Expedite_Date],[Urgency_Reason]From [Expedite].[dbo].[Expedite_time] as A ,[Expedite].[dbo].['All_Incidents'] as B  where A.[Incident_ID]=B.[INC Incident Number] and convert(date, [INC DS Last Modified Date]) <='" + startdated2 + "';";
            command.CommandText = "Insert into [Expedite].[dbo].[Esclation]([Incident Number],[DateEsclated],[NumberOfEsclation]) Values ('" + dt.Rows[IncNum][0] + "',GETDATE(),'3')";
            
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt1);

                }
            }
            Response.Redirect("Expedited_Incidents.aspx");
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }

    }


    protected void notifymanagers() {
        ArrayList all_managers_emails = new ArrayList();
        ArrayList all_inc = new ArrayList();
        ArrayList unique_emails = new ArrayList();

        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = (string)(Session["FTID"]);
        try
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT [Incident_ID],[AG M Email Address] FROM [Expedite].[dbo].['All_Incidents'],[Expedite].[dbo].[Expedite_time] where [Incident_ID]=[INC Incident Number]";
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                using (dt = new DataTable())
                {

                    sda.Fill(dt);

                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                all_managers_emails.Add(dt.Rows[i][1]);
                all_inc.Add(dt.Rows[i][0]);
            }

            String temp = all_managers_emails[0].ToString();
            unique_emails.Add(temp);
            foreach (String mail in all_managers_emails)
            {
                if (!unique_emails.Contains(mail))
                {
                    unique_emails.Add(mail);
                }
            }

            foreach (String email in unique_emails)
            {
                ArrayList alltheinc = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (((String)(dt.Rows[i][1])) == email)
                    {
                        alltheinc.Add(dt.Rows[i][0]);
                    }
                }
                sendmailtomanager(email,alltheinc);
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.Write(ex.ToString());
        }
    }

    protected void sendmailtomanager(String manager_email, ArrayList IncidentsIDs)
    {
        String body = "The following Incidents are expedited: \n";

        foreach (String id in IncidentsIDs)
        {
            body += id + "\n";
        }
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("mx-us.equant.com");
        mail.From = new MailAddress("expedite_portal@orange.com");
        mail.To.Add(manager_email);
        mail.Body = body;
        mail.Subject = "Expedtied Incidents";
        SmtpServer.Send(mail);
        //Debug.Write(body + " EMAIL IS " + manager_email);
    }
    protected void Notify_Click(object sender, System.EventArgs e)
    {
        notifymanagers();
    }
}