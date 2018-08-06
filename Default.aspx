﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<head id="Head1" runat="server">



   <meta charset='utf-8'>

   <meta http-equiv="X-UA-Compatible" content="IE=edge">

   <meta name="viewport" content="width=device-width, initial-scale=1">

   <link rel="stylesheet" href="styles.css">

   <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

   <script src="script.js"></script>

   <title>Login</title>

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

  padding: 30px;

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

  top:30%;

  left: 2%;

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

.description {
    color:gray;
}

body {
overflow:hidden;
margin-left:0px;
margin-right:0px;
margin-top:0px;
}
a{
    text-decoration:none;
    background-color: black;
    color:lightblue;

}

img {

    position: relative;

    left: 92%;

    width: 7%;
    height:auto;
    top:-23%;



}

#cssmenu > ul > li.has-sub > a:hover:before {

}

*{
font-family: Arial;
}

    </style>

</head>

<body>



<div id='cssmenu' style="background-color: #000000">

  <div id='OrangeText'>

    

    Expedite Portal

  </div>


    <img src="download.png" alt="Flowers in Chania" >


</div>

    <form runat=server>

        <div style="position:relative; margin-top: 10%;margin-left:40%; margin-bottom: 10%; font-family: Arial;" aria-expanded="true">
 
            UserName<br>
            <asp:TextBox ID="UserName" runat="server" value=""></asp:TextBox>
            <br>
            <asp:Label class="description" ID="Label1" runat="server" Text="FTID (ex: ABCD1234) "></asp:Label>
            </br>
            <br />
            Password<br>
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            <br>
            <asp:Label class="description" ID="Label2" runat="server" Text="AD Password"></asp:Label>
            <br/>
            </br>
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click1" Font-Names="Arial"  />
           
            </br>
            </br>
            <asp:Label ID="message" runat="server" Font-Size="12pt" ForeColor="Red" Font-Bold="True" ></asp:Label>
           
            </div>
        
    </form>
    <div style="width: 100%; height:10%;background-color: #000000; position: fixed;right: 0;bottom: 0;left: 0;"> <br /> <font color="#FF6501" center  width="10"><center>For any portal issues, thanks to contact us on <a style="text-decoration:none; background-color: black;color:lightblue;">it.support4business@orange.com</a></b></center></font> </div>
</body>

</html>
