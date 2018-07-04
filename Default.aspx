<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <Style>
        #message {
font-weight: bold;
color:red
        }
    </Style>
<body>
    <form id="form1" runat="server">
    <div style="height: 100px; width: 1265px; background-color:   black">
   <font color="orange"  size="6"  width="10"><b>Expedite Portal</b></font>
       <img style="height: 80px;width: 100px; margin-top:10px; margin-right:10px"src="download.png" align="Right" />
         
    
    </div>
        <div style="margin-top: 83px;margin-left: 550px;margin-right: 100px; margin-bottom: 25px;">
 
            UserName*<br><br>
        <asp:TextBox ID="UserName" runat="server" value=""></asp:TextBox>
                
              
                 <br>
  FTID Username (ex: ABCD1234)
                         </br></br> <asp:Label ID="message" runat="server" Text="" ></asp:Label></br></br>
            Password*<br><br>
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>    <br>
  FTID Password</br></br></br>
        <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click1"  />
           
            </div>
        <div style="height: 100px;width: 1350px;background-color: Black;margin-top: 150px;"> <br /> <font color="orange"  center  width="10"><center><b >For portal issues, contact us on
<a href="mailto:it.support4business@orange.com">it.support4business@orange.com</a></b></center></font> </div>
    </form>
</body>
</html>
