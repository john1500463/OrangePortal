<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expedited_Incidents_Support.aspx.cs" Inherits="Expedited_Incidents_Support" %>

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

  margin: 0;

  padding: 20px;

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

    position: absolute;

    z-index:0;

    right:10px;

    top: 10px;

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

   <li><a href='Home_Page_Support.aspx'><span>Home</span></a></li>

   <li><a href='My_Expedited_Incidents_Support.aspx'><span>My Expedited Incidents</span></a></li>
   <li ><a href='Expedited_Incidents_Support.aspx' style="background: #FF6501;border-color: #FF6501;"><span>Expedited Incidents</span></a>
   <li><a href='Help_Support.aspx'><span>Help</span></a></li>

   <li class='last' style="z-index:2;"><a href='Default.aspx'><span>Log Out</span></a></li>
    

</ul>

    <img src="download.png">


</div>

    <form id="form2" runat="server">
            <div style="margin-top:2%">  
                <p>
                <asp:Label ID="Label_Title" runat="server" Text="Expedited Incidents" Font-Bold="True" Font-Size="XX-Large" ForeColor="#FF6501" Style="margin-top:2%;margin-left:1%;"></asp:Label>
                    </p>
                <div style="position:absolute; right:2%">
                <asp:Label ID="Label4" runat="server" Text="Sort By: "></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Incident ID" Value="[INC Incident Number]"></asp:ListItem>
                    <asp:ListItem Text="Tier 2" Value="[INC Tier 2]"></asp:ListItem>
                    <asp:ListItem Text="Status" Value="[INC Status]"></asp:ListItem>
                    <asp:ListItem Text="Assigned Group" Value="[AG Assigned Group Name]" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Assignee" Value="[AG Assignee]"></asp:ListItem>
                    <asp:ListItem Text="Last Modified Date" Value="[INC DS Last Modified Date]"></asp:ListItem>
                    <asp:ListItem Text="Expedite Date" Value="[Expedite_Date]"></asp:ListItem>
                    <asp:ListItem Text="Urgency Reason" Value="[Urgency_Reason]"></asp:ListItem>
                </asp:DropDownList>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Reset" />
                    </div>
        </div>
        <div Style="margin-top:2%; margin-left:1%; display:inline-block;">
        <asp:Label ID="Label2" runat="server" Text="Expedite Date less than or equal to" Style="display:inline-block;"></asp:Label>
        <asp:Button ID="calendar1info" Text="..." runat="server" style="display:inline-block;" OnClick="calendar1info_Click"/>
            <asp:Label ID="Date1view" Text="" runat="server" style="position:relative; margin-left: 27%; display:block;" Visible="false"></asp:Label>
        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" style="position:relative; margin-right: 10%; display:block;" Visible="false"></asp:Calendar>
        </div>
            <div Style="margin-top:2%; margin-left:1%; display:inline-block;">
                <asp:Label ID="Label3" runat="server" Text="Last Modified Date less than or equal to" Style="display:inline-block"></asp:Label>
                <asp:Button ID="calendar2info" Text="..." runat="server" style="display:inline-block;" OnClick="calendar2info_Click"/>
                <asp:Label ID="Date2view" Text="" runat="server" style="position:relative; margin-left: 27%; display:block;" Visible="false"></asp:Label>
        <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged" style="position:relative; margin-right:20%; display:block" Visible="false"></asp:Calendar>
            </div>  
    <div style="margin-top: 30px;">
       
       
        <asp:GridView ID="GridView1" runat="server" CellPadding="6" ForeColor="#FF3300" Visible="False" BorderStyle="None" style="margin-bottom:1%; margin-left:1%;" >
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
        </div>
        <div style="margin-bottom:20%;margin-left:1%; margin-top:1%;">
            <asp:ImageButton ID="Button3" runat="server" width="100px" ImageUrl="Excel-Export.jpg" OnClick="Button3_Click"/>
        <p>
            <asp:Label ID="Label_Error" Text="No Incidents Exist" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />
        </p>   
        </div>
        
       
       </form>
    <div style="width: 100%; height:10%;background-color: #000000; position: fixed;right: 0;bottom: 0;left: 0;"> <br /> <font color="#FF6501" center  width="10"><center>For any portal issues, thanks to contact us on <a style="text-decoration:none; background-color: black;color:lightblue;">it.support4business@orange.com</a></b></center></font> </div>

</body>
</html>

