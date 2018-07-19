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
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
public partial class ExpeditePageUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] == null)
        {
            //    Response.Redirect("Default.aspx");
        }
        String Incident = Request.QueryString["param1"];
        Label1.Text = Incident;

        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$";
        cnn = new SqlConnection(connetionString);
        // cnn.Open();

        string strSelect = "Select * From [Expedite].[dbo].[UrgencyReasons]";
        // string strSelect = "Select * From [dbo].['All_Incidents'] ;";
        //   SqlCommand cmd = new SqlCommand(strSelect, cnn);
        SqlDataAdapter adpt = new SqlDataAdapter(strSelect, cnn);

        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "UrgencyReason";
        DropDownList1.DataValueField = "ID";
        DropDownList1.DataBind();
        //  Label1.Text = myReader.GetValue(0).ToString();




        /*   SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");
           conn.Open();
           SqlCommand command = new SqlCommand();
           command.Connection = conn;
           command.CommandText = "Select * From [Expedite].[dbo].[UrgencyReasons]";
           DataTable dt = new DataTable();
        //   SqlDataAdapter dr = new SqlDataAdapter();
       //    DropDownList1.Items.Clear();

           using (SqlDataAdapter dr = new SqlDataAdapter())
           {
               dr.SelectCommand = command;
               using (dt = new DataTable())
               {

                   dr.Fill(dt);

               }
            //   DropDownList1.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
           }
            
           DropDownList1.DataSource = dt;
        //   DropDownList1.DataBind();
           DropDownList1.DataTextField = "UrgencyReason";
          DropDownList1.DataValueField = "ID ";
           DropDownList1.DataBind();
            
           conn.Close();
       } */


        //   Label3.Text = (string)(Session["Email"]);

        /*    String Email = (string)(Session["Email"]);
            String FTID =  (string)(Session["FTID"]);
            SqlConnection conn = new SqlConnection("Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$");

            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "Select * From [dbo].[Expedite_time] where [Expedite_BY]='" + FTID + "';";
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

            */
        Label2.Visible = false;

    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "Select Reason-----")
        {
            // System.Windows.Forms.MessageBox.Show("Please Select A Reason!");
            Label2.Visible = true;
            Label2.Text = "Please Select a Reason!";
        }
        else
        {
            Label2.Text = "";
            Label2.Visible = false;
            String Incident = Request.QueryString["param1"];
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=10.238.110.196;Initial Catalog=Expedite;User ID=sa;Password=Orange@123$";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            string strSelect = "Select [INC DS Submit Date] from [dbo].['All_Incidents'] where [INC Incident Number]='" + Incident + "';";
            // string strSelect = "Select * From [dbo].['All_Incidents'] ;";
            SqlCommand cmd = new SqlCommand(strSelect, cnn);
            SqlDataReader myReader = cmd.ExecuteReader();
            myReader.Read();
            //  Label1.Text = myReader.GetValue(0).ToString(); */
            // refresh_grid1();

            MailMessage mail = new MailMessage();
            //
            SmtpClient SmtpServer = new SmtpClient("mx-us.equant.com");
            mail.From = new MailAddress("george-itsupport@orange.com");
            mail.To.Add("george.delacroix@orange.com");
            mail.Body = "The ticket with Incident number " + Incident + " Expoditued";
            mail.Subject = "Bngrb keda :D ";
            SmtpServer.Send(mail);

            String SubmitDate = myReader.GetValue(0).ToString();
            String UrgenyReason = DropDownList1.SelectedItem.Text;
            String FTID = (string)(Session["FTID"]);
            string strSelect2 = "insert into [Expedite].[dbo].[Expedite_time](Incident_ID,Submit_Date,Expedite_By,Expedite_Date,Urgency_Reason) values ('"
                + Incident + "','" + SubmitDate + "','" + FTID + "', GETDATE() ,'" + UrgenyReason + "');";
            SqlCommand cmd1 = new SqlCommand(strSelect2, cnn);
            myReader.Close();
            cmd1.ExecuteNonQuery();



            cnn.Close();

            // Response.Write("<script LANGUAGE='JavaScript' >alert('The Incident has been Expedited')</script>");
            Response.Redirect("Home_Page_User.aspx");
        }

    }
    
}