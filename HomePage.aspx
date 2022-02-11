<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Assignment.HomePage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link rel="stylesheet" href="css/style.css"/>
    <link href="https://fonts.googleapis.com/css?family=Ubuntu" rel="stylesheet"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="path/to/font-awesome/css/font-awesome.min.css"/>
    <title>Home Page</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LfwZGkeAAAAAO9s5qq45Y8BLC4tjEnKMUMwfuXa"></script>
</head>
<body>
    <p class="sign" style="margin-bottom:-90px" align="center">Home Page</p>
    <div class="main" align="middle-center">  
        <form class="form1" id="form2" runat="server">
            <img src="image/success.png" style="width:150px;height:150px; margin-left:125px; margin-top:15px">
            <p class="forgot" align="center" style="color:lightpink; font-weight:bold;"><asp:Label ID="lblMessage" runat="server" Enableviewstate="False"/></p>
            <asp:Button class="submit" ID="btnLogout" runat="server" align="center" Text="Logout" Onclick="LogoutMe"/>
        </form>
        <div class="forgot" align="center"><a href="Changepw.aspx" />Change Password</div>
    </div>
</body>
</html>
