﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Home_Page_Support : System.Web.UI.Page
{

    public class CryptoEngine
    {
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (((String)Session["Right"]) == "else")
        {
            Response.Redirect("Home_Page_User.aspx");
        }
        // GridView1.Visible = false;
        //TextBox1.Text = "";
    }

    /* void refresh_grid1()
     {
         SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
         String x = TextBox1.Text.ToString(); 
         try
         {
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand command = new SqlCommand();
             command.Connection = conn;
            command.CommandText = "Select * From [dbo].['All_Incidents'] where [INC Incident Number]='" + x + "';";
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
           //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";
         }
         catch (Exception ex)
         {
             conn.Close();
             Console.Write(ex.ToString());
         }

     } */
    void refresh_grid1()
    {
        GridView1.Visible = false;
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        String x = TextBox1.Text.ToString();
        if (x.Count() < 8)
        {
            
            Label1.Text = "Please enter a valid Incident";
        }
        else
        {
            string y = x.Substring(0, 8);
            if (y != "INC00100")
            {
                Label1.Text = "Please enter a valid Incident";
            }
            else
            {
                
                try
                {
                    DataTable dt = new DataTable();

                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = "Select [Incident_ID],[Submit_Date],[Expedite_By],[Expedite_Date],[Urgency_Reason] From [dbo].[Expedite_time] where [Incident_ID]='" + x + "';";


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
                        command.CommandText = "Select [INC Incident Number] as 'Incident Number',[INC Status] as 'Status',[INC Tier 2] as 'Tier 2',[INC DS Submit Date] as 'Submit Date',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified' From [dbo].['All_Incidents'] where [INC Incident Number]='" + x + "';";
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
                            DateTime LastModifiedExe = GetLastModifiedDateExeDateTime();
                            DateTime LastModifiedExePlusThirty = LastModifiedExe.AddMinutes(60);
                            String Time = (LastModifiedExePlusThirty - DateTime.Now).ToString();
                            Time = Time.Substring(0, 9);
                            Label1.Text = "The Incident will be available in " + Time;

                            Button3.Visible = false;
                            GridView1.Visible = false;
                            // Label1.Text = "";

                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Visible = true;
                            clickable_incidents();
                            Button3.Visible = true;
                            Label1.Text = "";
                            Label2.Text = "";

                        }
                        //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";

                    }
                    else
                    {
                        command.CommandText = "Select [INC Incident Number] as 'Incident Number',[INC Status] as 'Status',[INC Tier 2] as 'Tier 2',[INC DS Submit Date] as 'Submit Date',[AG Assigned Group Name] as 'Assigned Group',[AG Assignee] as 'Assignee',[INC DS Last Modified Date] as 'Last Modified'  ,[Expedite].[dbo].[Expedite_time].[Expedite_Date] as 'Expedite Date',[Expedite].[dbo].[Expedite_time].[Urgency_Reason] 'Urgency Reason'From [dbo].['All_Incidents'] ,[Expedite].[dbo].[Expedite_time] where [INC Incident Number] = '" + x + "' and [Expedite].[dbo].[Expedite_time].[Incident_ID] = '" + x + "';";
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            sda.SelectCommand = command;

                            using (dt = new DataTable())
                            {

                                sda.Fill(dt);

                            }
                        }




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
                            Label1.Text = "Incident doesn't exist";

                            Button3.Visible = false;
                            GridView1.Visible = false;
                            // Label1.Text = "";

                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Visible = true;
                            clickable_incidents();
                            Button3.Visible = false;
                            Label1.Text = "Incident " + x + " Already Expedited";
                            Label2.Text = "";

                        }
                        //  Label2.Text = "<script  LANGUAGE='JavaScript' > <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> <asp:Button ID='Button2' runat='server' Text='Expedite' OnClick='Button2_Click' /> </script>";

                    }
                    conn.Close();
                }


                // Response.Write("ya3");
                catch (Exception ex)
                {
                    conn.Close();
                    Console.Write(ex.ToString());
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //String x = TextBox1.Text.ToString();
        Label1.Text = "";
        Label2.Text = "";
        refresh_grid1();


        //  Label1.Text = x;
        // string connetionString;
        //SqlConnection cnn;
        //connetionString = @"Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$";
        //cnn = new SqlConnection(connetionString);
        /*  cnn.Open();

        string strSelect = "Select [INC Status] From [dbo].['All_Incidents'] where [INC Incident Number]='"+x+"';";
         // string strSelect = "Select * From [dbo].['All_Incidents'] ;";
               SqlCommand cmd = new SqlCommand(strSelect, cnn);
               SqlDataReader myReader = cmd.ExecuteReader();
               myReader.Read();
             //  Label1.Text = myReader.GetValue(0).ToString(); */
        // refresh_grid1();

        //  myReader.Close();
        //   cnn.Close(); 
    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ExpeditePage.aspx");  
    //}
    protected void Button2_Click1(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        TextBox1.Text = "";
        Button3.Visible = false;
        Label1.Text = "";
        Label2.Text = "";

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label2.Text = "";
        String Y = TextBox1.Text.ToString();

        //If already Exp
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

        DataTable dt = new DataTable();
        conn.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.CommandText = "Select [Incident_ID],[Submit_Date],[Expedite_By],[Expedite_Date],[Urgency_Reason] From [dbo].[Expedite_time] where [Incident_ID]='" + Y + "';";

        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;

            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }
        }


        conn.Close();
        if (dt.Rows.Count != 0)
        {
            Label2.Text = "Already Expedited";

        }

        else
        {
            
            Response.Redirect("ExpeditePageSupport.aspx?param1=" + Y);
        }
    }
    protected void clickable_incidents()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HyperLink hlContro = new HyperLink();
            String Incident = GridView1.Rows[i].Cells[0].Text;
            hlContro.NavigateUrl = "./Incident_Details_Support.aspx?ID=" + Incident;
            hlContro.Text = GridView1.Rows[i].Cells[0].Text;
            GridView1.Rows[i].Cells[0].Controls.Add(hlContro);
        }

    }
    DateTime GetLastModifiedDateTime()
    {
        return System.IO.File.GetLastWriteTime("C:/Users/wkzw7370/Downloads/Project Code/WebSite2/OrangePortal/NewExpedite.xls");
    }
    DateTime GetLastModifiedDateExeDateTime()
    {
        DataTable dt;
        SqlCommand command = new SqlCommand();
        SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
        conn.Open();
        command.Connection = conn;
        command.CommandText = "select * from [expedite].[dbo].[Last_Update_Time]";
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            sda.SelectCommand = command;
            using (dt = new DataTable())
            {

                sda.Fill(dt);

            }

        }
        conn.Close();
        return (DateTime)dt.Rows[0][0];
    }
}