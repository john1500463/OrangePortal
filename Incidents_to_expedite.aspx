﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Incidents_to_expedite.aspx.cs" Inherits="Incidents_to_expedite" EnableViewState="true"%>


<!DOCTYPE html>





<head id="Head1" runat="server">



   <meta charset='utf-8'>

   <meta http-equiv="X-UA-Compatible" content="IE=edge">

   <meta name="viewport" content="width=device-width, initial-scale=1">

   <link rel="stylesheet" href="styles.css">

   <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

   <script src="script.js"></script>

   <title>Home</title>

    <style>

        #cssmenu,

#cssmenu ul,

#cssmenu li,

#cssmenu a {

  border: none;

  line-height: 1;

  margin: 0;

  padding: 0;

}

#cssmenu {

  height: 140px;

  display: block;

  width: 100%;

  border-color: #080808;

  margin: 0;

  padding: 0;

}

#cssmenu > ul {

  list-style: inside none;

  margin: 0;

  padding: 0;

}

#cssmenu > ul > li {

  list-style: inside none;

  display: inline-block;

  position: relative;

  margin: 0.25%;

  padding: 1%;

  padding-top:1.3%;

}

#cssmenu.align-center > ul {

  text-align: center;

}

#cssmenu.align-center > ul > li {

  float: none;

  margin-left: -3px;

}

#cssmenu.align-center ul ul {

  text-align: left;

}

#cssmenu.align-center > ul > li:first-child > a {

  border-radius: 0;

}

#cssmenu > ul > li > a {

  outline: none;

  display: block;

  position: relative;

  text-align: center;

  text-decoration: none;

  text-shadow: 1px 1px 0 rgba(0, 0, 0, 0.4);

  font-weight: 700;

  font-size:  16px;

  font-family: Arial, Helvetica, sans-serif;

  color: #ffffff;

  padding: 12px 20px;

}

#cssmenu > ul > li:first-child > a {

        }

#cssmenu > ul > li > a:after {

  content: "";

  position: absolute;

  top: -1px;

  bottom: -1px;

  right: -2px;

  z-index: 99;

  border-color: #3c3c3c;

}

#cssmenu ul li.has-sub:hover > a:after {

  top: 0;

  bottom: 0;

}

#cssmenu > ul > li.has-sub > a:before {

  content: "";

  position: absolute;

  top: 18px;

  right: 6px;

}

#cssmenu > ul > li.has-sub:hover > a:before {

  top: 19px;

}

#cssmenu > ul > li.has-sub:hover > a {

  padding-bottom: 10px;

  z-index: 999;

  border-color: #3f3f3f;

}

#cssmenu ul li.has-sub:hover > ul,

#cssmenu ul li.has-sub:hover > div {

  display: block;

}

#cssmenu > ul > li.has-sub > a:hover,

#cssmenu > ul > li.has-sub:hover > a {

  background: #FF6501;

  border-color: #FF6501;

}

#cssmenu ul li > ul,

#cssmenu ul li > div {

  display: none;

  width: auto;

  position: absolute;

  top: 65.5px;

  background: #3f3f3f;

  border-radius: 0 0 5px 5px;

  z-index: 999;

  padding: 10px 0;

}

#cssmenu ul li > ul {

  width: 200px;

}

#cssmenu ul ul ul {

  position: absolute;

}

#cssmenu ul ul li:hover > ul {

  left: 100%;

  top: -10px;

}

#cssmenu ul li > ul li {

  display: block;

  list-style: inside none;

  position: relative;

  margin: 0;

  padding: 0;

}

#cssmenu ul li > ul li a {

  outline: none;

  display: block;

  position: relative;

  font: 10pt Arial, Helvetica, sans-serif;

  color: #ffffff;

  text-decoration: none;

  text-shadow: 1px 1px 0 rgba(0, 0, 0, 0.5);

  margin: 0;

  padding: 8px 20px;

}

#OrangeText

{

  position: relative;

  color: #FF6501;

  top:10%;

  margin-left: 0.5%;

  font-size: 38pt;

}

#cssmenu,

#cssmenu ul ul > li:hover > a,

#cssmenu ul ul li a:hover {

  background: #3c3c3c;

  background: -moz-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #3c3c3c), color-stop(100%, #222222));

  background: -webkit-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: -o-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: -ms-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: linear-gradient(top, #3c3c3c 0%, #222222 100%);

}

#cssmenu > ul > li > a:hover {

  background: #FF6501;

  color: #ffffff;

}

#cssmenu ul ul a:hover {

  color: #FF6501;

}



img {

    position: relative;

    z-index:0;

    left: 91.5%;

    top: -80%;

    width: 7%;

    min-width:0.05%;

    max-width:50%;

    max-height:110px;

}

body {
overflow:auto;
margin-left:0px;
margin-right:0px;
margin-top:0px;
}

#cssmenu > ul > li.has-sub > a:hover:before {

}

a {
    text-decoration:none;
    color:black;
}

a:hover {
    color:#FF6501;
}
*{
font-family: Arial;
}
    </style>

</head>

<body>



<div id='cssmenu'>
    
  <div id='OrangeText'>

    Expedite Portal
      </div>

    



<ul>

   <li><a href='Home_Page.aspx'><span>Home</span></a></li>

   <li><a href='My_Expedited_Incidents.aspx'><span>My Expedited Incidents</span></a></li>

   <li class='active has-sub'><a href='#'><span>Expedited Incidents </span></a>

      <ul>

         <li class='has-sub'><a href='Expedited_Incidents.aspx'><span>Expedited Incidents</span></a>

         </li>

         <li class='has-sub'><a href='Sita.aspx'><span>SITA Expedited Incidents</span></a>

         </li>

         <li class='has-sub'><a href='Incidents_to_expedite.aspx'><span>Incidents To Expedite</span></a>

         </li>

         <li class='has-sub'><a href='Expedite_Extraction.aspx'><span>Expedite Extraction</span></a>

         </li>

         <li class='has-sub'><a href='Urgency_Reason_Stats.aspx'><span>Urgency Reason Statistics</span></a>

         </li>

         <li class='has-sub'><a href='CSM_entity.aspx'><span>CSM Expedite Incidents</span></a>

         </li>

         <li class='has-sub'><a href='Expedite_multiple_incidents.aspx'><span>Expedite Multiple Incidents</span></a>

         </li>

      </ul>

   </li>

           <li class='active has-sub'><a href='#'><span>Users</span></a>

      <ul>

         <li class='has-sub'><a href='User.aspx'><span>Add New User</span></a>

         </li>

         <li class='has-sub'><a href='ModifyUser.aspx'><span>Modify User</span></a>

         </li>

      </ul>

   </li>
    <li><a href='Urgency_Reasons.aspx'><span>Urgency Reasons</span></a></li>

   <li><a href='#'><span>Help</span></a></li>

   <li class='last' style="z-index:2;"><a href='Default.aspx'><span>Log Out</span></a></li>
    

</ul>

    <img src="download.png">


</div>
    <form id="form2" runat="server">
    <div style="margin-top: 50px;">
        <p>
                <asp:Label ID="Label_Title" runat="server" Text="Incidents To Expedite" Font-Bold="True" Font-Size="XX-Large" ForeColor="#FF6501" Style="margin-top:2%;margin-left:1%;"></asp:Label>
                    </p>
        <asp:Label ID="Label1" runat="server" Text="Incident ID " style="margin-left:1%;"></asp:Label>

        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Clear" />

        <asp:DropDownList ID="DropDownList1" runat="server" style=" margin-left:20px" >
        </asp:DropDownList>
        <p aria-expanded="true">
        <asp:Label ID="Textbox_message" runat="server" Text="message" style=" margin-left:1%"></asp:Label>
            </p>
        <asp:GridView ID="GridView1" runat="server" CellPadding="10" ForeColor="#FF3300" AutoGenerateSelectButton="False" Visible="False" BorderStyle="Solid" BorderWidth="2px" style="margin-left:1%;">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="#FF6501" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>

        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

        <asp:Button ID="Button3" OnClick="Button3_Click" runat="server" Text="Expedite" style="margin-left:1%;margin-top:1%;"/>

        </div>
        
           
        
       
       </form>

<div style="width: 100%; height:15%;background-color: #000000; position: fixed;right: 0;bottom: 0;left: 0;"><br /><font color="#FF6501" center  width="10"><center>For any portal issues, thanks to contact us on <a style="text-decoration:none; background-color: black;color:lightblue;" href="oniness@orange.com">it.support4business@orange.com</a><br /> <asp:label ID="Label_ModifiedDateExcel" text="7amada" runat="server" Font-Size="10pt" /><br /><asp:label ID="Label_ModifiedDateExe" text="7amada" runat="server" Font-Size="10pt" /></center></font> </div>

</body>
</html>

