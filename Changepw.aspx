<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Changepw.aspx.cs" Inherits="Assignment.Changepw" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link rel="stylesheet" href="css/style.css"/>
    <link href="https://fonts.googleapis.com/css?family=Ubuntu" rel="stylesheet"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="path/to/font-awesome/css/font-awesome.min.css"/>
    <title>Change Password</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LfwZGkeAAAAAO9s5qq45Y8BLC4tjEnKMUMwfuXa"></script>
</head>
<body>
    <div class="main"  style="padding-left:30px; padding-right:30px">  
        <p class="sign" align="center" style="margin-bottom:10px">Change Password</p>
        <form class="form1" id="form1" runat="server">
            <asp:TextBox class="un" ID="tb_useremail" runat="server" placeholder="Email"/>
            <asp:TextBox class="un" ID="tb_currpw" runat="server" placeholder="Current Password" TextMode="Password"/>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:10px;"><asp:Label ID="lbl_currpw" runat="server" Forecolor="Red"></asp:Label></div>
            <asp:TextBox class="un" ID="tb_newpw" runat="server" placeholder="New Password" TextMode="Password"/>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:20px;"><asp:Label ID="lbl_newpw" runat="server" Forecolor="Red"></asp:Label></div>
            <asp:Button class="submit" ID="btn_update" runat="server" align="center" Text="Confirm"  OnClick="btn_update_Click"/>
        </form>
    </div>
</body>
</html>
