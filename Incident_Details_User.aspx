﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Incident_Details_User.aspx.cs" Inherits="Incident_Details_User" %>
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

  margin-left:0.5%;

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


td {
    border: 3px solid black;
}

tr:nth-child(even) {background-color: #f2f2f2;}
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

   <li><a href='Home_Page_User.aspx'><span>Home</span></a></li>

   <li><a href='My_Expedited_Incidents_User'><span>My Expedited Incidents</span></a></li>  

   <li class='last' style="z-index:2;"><a href='Default.aspx'><span>Log Out</span></a></li>
    

</ul>

    <img src="download.png">


</div>
    
    <form id="form1" runat="server">
    <div style="margin-bottom:10%;">
     <asp:Table ID="Table1" runat="server" BorderStyle="Solid" CellPadding="10" GridLines="Both" style="margin-left:1%; margin-top:4%;" BackColor="White">
            </asp:Table>
    </div>
    </form>
    <div style="width: 100%; height:15%;background-color: #000000; position: fixed;right: 0;bottom: 0;left: 0;"> <br /> <font color="#FF6501"  center  width="10"><center><b><br />For portal issues, contact us <a style="text-decoration:none; background-color: black;color:lightblue;" href="oniness@orange.com">it.support4business@orange.com</a></b></center></font> </div>
</body>
</html>
