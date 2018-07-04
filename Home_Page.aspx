<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home_Page.aspx.cs" Inherits="Home_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<style>

#up 
{    
  height: 100px;
  width: 100%;
  background-color: black;                   
}

#bottomnav 
{
    position:absolute;
  height: 100px;
  width: 99%;
  background-color: black;
  bottom:0%;
}

#buttom 
{  
  margin-top: 100px;
  margin-left: 550px;
  margin-right: 100px;
  margin-bottom: 25px;
}

.navbar 
{
  overflow:auto;
  width:100%;
  max-width:105%;
  background-color: black;
  font-family: Arial, Helvetica, sans-serif;
}

.navbar a 
{
  float: left;
  font-size: 16px;
  color: white;
  text-align: center;
  padding: 20px 22px;
  text-decoration: none;
}

.dropdown 
{
  float: left;
  overflow: hidden;
}

.dropdown .dropbtn 
{
  font-size: 16px;    
  border: none;
  outline: none;
  color: white;
  padding: 22px 22px;
  background-color: inherit;
  font-family: inherit;
  margin: 0;
}

.navbar a:hover, .dropdown:hover .dropbtn 
{
  background-color: darkorange;
}

.dropdown-content 
{
  display: none;
  position: absolute;
  background-color: black;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

.dropdown-content a 
{
  float: none;
  color: white;
  padding: 20px 15px;
  text-decoration: none;
  display: block;
  text-align: left;
}

.dropdown-content a:hover 
{
  background-color: darkorange;
}

.dropdown:hover .dropdown-content 
{
  display: block;
}

</style>
</head>

<body>

<div id="up" aria-expanded="true">  <font color= darkorange  size="6"  width="10"><b>Expedite Portal</b></font>&nbsp;<a href="Default.aspx"><i class="fa fa-caret-down"></i></a> 
  
<div class="navbar">
    <a href="Home_Page.aspx">Home</a>
 <a href="My_Expedited_Incidents.aspx">My Expedited Incidents</a>

<div class="dropdown">
 <button class="dropbtn">Expedited Incidents 
    </button>
 
<div class="dropdown-content">
 <a href="Support_Ack.aspx">Expedited Incidents</a>
 <a href="#">SITA Expedited Incidents</a>
 <a href="#">Incidents to Expedite</a>
 <a href="#">Urgency Reason Statistics</a>
 <a href="CSM_entity.aspx">CSM expedite Incidents </a>

</div>
</div> 
    
 <a href="#features">Feature Requests</a>
 <a href="#help">Help</a>
 <a href="Default.aspx">Log Out</a> 
 </div> 
</div>
     <div aria-expanded="true"> <img src="download.png" alt="Orange.com"    style=" position: absolute; top:1.38%; left:91%; height: 101px; width: 107px; "> </div> 
</form> 
</div>
    <form id="form1" runat="server" aria-expanded="true">
        <p>
            &nbsp;</p>
    <div style="margin-top: 50px;" aria-expanded="true">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> &nbsp&nbsp&nbsp&nbsp&nbsp <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
       
        
        
        &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="Clear" />
       
        
        
        </div>
        <p aria-expanded="true">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </p>
        <p aria-expanded="true">
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#FF3300" GridLines="None" AutoGenerateSelectButton="False" Visible="False">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="DarkOrange" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Expedite" Visible="False" />
        </p>
        
       
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        
       
       </form>

<div id="bottomnav" aria-expanded="true"> <br /> <font color=darkorange  center  width="10"><center><b >For portal issues, contact us on
 <a href="mailto:it.support4business@orange.com">it.support4business@orange.com</a></b></center></font> </div>

</body>
</html>
