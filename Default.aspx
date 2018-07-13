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
        body {
        overflow:hidden;
        }
        .NavBar {
            display:block;
            top:-10px;
            left:-10px;
            width:100%;
            height:99px;
            background-color:black;
        }
        #Label2 {
            position: absolute;
            color: orange;
            top: 33px;
            left: 75px;
            height: 58px;
            width: 317px;
        }
    </Style>
<body>
    <form id="form1" runat="server">
        <div class="NavBar">
        <asp:Image ID="Image1" runat="server" Height="85px" Width="78px" ImageAlign="Right" ImageUrl="~/download.png" BorderColor="Black" />
            </div>
        <div id ="Label2">
        <asp:Label ID="Label3" runat="server" Text="Expedite Portal" Font-Bold="True" Font-Italic="True" Font-Size="35pt" ForeColor="#FF6501"></asp:Label>
        
        </div>
       
        <div style="margin-top: 83px;margin-left: 550px;margin-right: 100px; margin-bottom: 25px; font-family: Arial;" aria-expanded="true">
 
            UserName<br>
            <asp:TextBox ID="UserName" runat="server" value=""></asp:TextBox>
            <br>
            <asp:Label ID="Label1" runat="server" Text="FTID Username (ex: ABCD1234) "></asp:Label>
            </br>
            <br />
            Password<br>
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            <br>FTID Password<br />
            </br>
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click1"  />
           
            </br>
            </br>
            <asp:Label ID="message" runat="server" Text="" ></asp:Label>
           
            </div>
        <div style="height: 100px;width: 100%;background-color: Black;margin-top: 150px;"> <br /> <font color="orange"  center  width="10"><center><b>For portal issues, contact us oniness@orange.com">it.support4business@orange.com</a></b></center></font> </div>
    </form>
</body>
</html>
