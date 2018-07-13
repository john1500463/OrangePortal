﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expedite_multiple_incidents.aspx.cs" Inherits="Expedite_multiple_incidents" %>

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
        width: 156px;
        height: 23px;
    }

.dropdown 
{
  float: left;
  overflow: hidden;
}

.dropdown .dropbtn 
{
    border-style: none;
        border-color: inherit;
        border-width: medium;
        font-size: 16px;    
        outline: none;
        color: white;
        padding: 22px 22px;
        background-color: black;
        font-family: inherit;
        margin: 0;
        width: 209px;
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
  width:210px;
   min-width: 140px;
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
#Clear_Button 
{
     border-radius: 25px;
}
#Expedite_Button 
{
     border-radius: 25px;
}

.dropdown-content a:hover 
{
  background-color: darkorange;
  width:85.5%;
}

.dropdown:hover .dropdown-content 
{
  display: block;
}

#Search_Button
{
     border-radius: 25px;
}

</style>
</head>

<body>

<div id="up" aria-expanded="true">  <font color= darkorange  size="6"  width="10"><b>Expedite Portal</b></font>&nbsp;
<div class="navbar">
    <a href="Home_Page.aspx">Home</a>
 <a href="My_Expedited_Incidents.aspx">My Expedited Incidents</a>

<div class="dropdown">
 <button class="dropbtn">Expedited Incidents 
    </button>
 
<div aria-expanded="true" class="dropdown-content">
 <a href="Expedited_Incidents.aspx">Expedited Incidents</a>
 <a href="Sita.aspx">SITA Expedited Incidents</a>
 <a href="Incidents_to_expedite.aspx">Incidents to Expedite</a>
 <a href="Urgency_Reason_Stats.aspx">Urgency Reason Statistics</a>
 <a href="CSM_entity.aspx">CSM expedite Incidents </a>
<a href="Expedite_multiple_incidents.aspx">Expedite multiple incidents</a>

</div>
</div> 
    
 <a href="#features">Feature Requests</a>
 <a href="#help">Help</a>
 <a href="Default.aspx">Log Out</a> 
 </div> 
</div>
     <div aria-expanded="true"> <img src="download.png" alt="Orange.com"    style=" position: absolute; top:1.38%; left:91%; height: 101px; width: 107px; "> </div> 
</form> 
</div>    <form id="form2" runat="server">
    <div style="margin-top: 50px;">

        <asp:TextBox ID="TextBox_id" runat="server"></asp:TextBox>
        <asp:Button ID="Search_Button" runat="server" OnClick="Search_Button_Click" Text="Search" />

        <asp:Button ID="Clear_Button" runat="server" OnClick="Clear_Button_Click" Text="Clear" />

        <asp:Label ID="Label1" runat="server" Text="Urgency Reason:" style=" margin-left:20px"></asp:Label>

        <asp:DropDownList ID="DropDownList1" runat="server" style=" margin-left:5px">
        </asp:DropDownList>
        <asp:Button ID="Expedite_Button" runat="server" OnClick="Expedite_Button_Click" Text="Expedite" />

        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#FF3300" GridLines="None" >
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

        </div>
        
        <p>
            &nbsp;</p>
        
       </form>

</body>
</html>