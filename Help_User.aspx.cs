using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

public partial class Help_User : System.Web.UI.Page
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
    }
    protected void downloaddoc_click(object sender, EventArgs e)
    {
        string filename = "Expedite Portal User Guide.docx";
        string strURL = filename;
        WebClient req = new WebClient();
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.ClearContent();
        response.ClearHeaders();
        response.Buffer = true;
        response.ContentType = "application/ms-word";
        response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
        byte[] data = req.DownloadData(Server.MapPath(strURL));
        response.BinaryWrite(data);
        response.End();
    }
}