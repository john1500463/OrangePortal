using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FTID"] != null)
        {
            Session["FTID"] = null;
        }
    }

   
    protected void Button1_Click1(object sender, EventArgs e)
    {
        bool InternalValid = false;
        string cn_S = "";
        string givenName_S = "";
        string sn_S = "";
        string description_S = "";
        PrincipalContext pc = new PrincipalContext(ContextType.Domain, "ad.francetelecom.fr");
        InternalValid = pc.ValidateCredentials(UserName.Text.ToString(), Password.Text.ToString());
        // String UserName = X.Text.ToUpper();      
        try
        {
            var context = new PrincipalContext(ContextType.Domain, "ad.francetelecom.fr", UserName.Text.ToString(), Password.Text.ToString());
            var searchPrinciple = new UserPrincipal(context);
            searchPrinciple.SamAccountName = UserName.Text;
            var searcher = new PrincipalSearcher();
            searcher.QueryFilter = searchPrinciple;
            var results = searcher.FindAll();
            foreach (var result in searcher.FindAll())
            {
                DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                try
                {
                    foreach (String property in de.Properties.PropertyNames)
                    {
                        String value = de.Properties[property][0].ToString();
                        switch (property.ToString())
                        {
                            case "cn":
                                cn_S = de.Properties[property][0].ToString();
                                break;
                            case "givenName":
                                givenName_S = de.Properties[property][0].ToString();
                                break;
                            case "sn":
                                sn_S = de.Properties[property][0].ToString();
                                break;
                            case "description":
                                description_S = de.Properties[property][0].ToString();
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.ToString());
        }

        if (InternalValid == true)
        {
            //  String x = "heeeeeeeeeeh";
            //  message.text = x;
            //  Response.Write(message.text);

            // String x=message.Text.ToString();
            // x = "dddd";

            //  Response.Write(x);
          //  Response.Write("<script LANGUAGE='JavaScript' >alert('Welcome To Expedite Portal')</script>");
          //  Server.Transfer("Default2.aspx", true);



           // Response.Redirect("Main.aspx");
            Session["FTID"] = cn_S;
            Session["Email"] = description_S;
            Response.Redirect("Home_Page.aspx");
            
          /*  Response.Write(cn_S);
            Response.Write(description_S);
            Response.Write(cn_S); */
            

        }
        else
        {
            message.Text = "Thanks to enter correct username or password";
        }
            //Response.Write("<script LANGUAGE='JavaScript' >alert('Thanks to enter correct usernema or password')</script>"); }
        
    }
}