<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment.Login" ValidateRequest = "false"%>

<!DOCTYPE html>

<html>
<head runat="server">
    <link rel="stylesheet" href="css/style.css"/>
    <link href="https://fonts.googleapis.com/css?family=Ubuntu" rel="stylesheet"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="path/to/font-awesome/css/font-awesome.min.css"/>
    <title>Login</title>
    <script src="https://www.google.com/recaptcha/api.js?render=SiteKey"></script>
</head>
<body>
    <div class="main"  style="padding-left:30px; padding-right:30px">  
        <p class="sign" align="center" style="margin-bottom:10px">Login</p>
        <form class="form1" id="form1" runat="server">
            <asp:TextBox class="un" ID="tb_userid" runat="server" placeholder="Email"/>
            <%--<div class="forgot" align="center" style="margin-top:-35px; margin-bottom:20px"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_userid" ErrorMessage="Please enter your email" ForeColor="Red"></asp:RequiredFieldValidator></div>--%>
            <asp:TextBox class="pass" ID="tb_pwd" runat="server" placeholder="Password"  TextMode="Password"/>
            <%--<div class="forgot" align="center" style="margin-top:-35px; margin-bottom:20px"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_pwd" ErrorMessage="Please enter your password" ForeColor="Red"></asp:RequiredFieldValidator></div>--%>
<%--            <p>Email : <asp:TextBox ID="tb_userid" runat="server" Height="25px" Width="159px"/> </p>--%>
<%--            <p>Password : <asp:TextBox ID="tb_pwd" runat="server" Height="25px" Width="138px"  TextMode="Password"/> </p>--%>
<%--            <p><asp:Button ID="btnSubmit" runat="server" Text="Login" Onclick="LoginMe" Height="28px" Width="215px" />--%>
            <asp:Button class="submit" ID="btnSubmit" runat="server" align="center" Text="Login" Onclick="LoginMe"/>
            <div class="forgot" align="center"><asp:Label ID="lblMessage" runat="server" Enableviewstate="False" ForeColor="Red"/></div>
            <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
            <asp:Label ID="lbl_gScore" runat="server" Enableviewstate="False" />
            <div class="forgot" align="center" style="margin-top:-15px"><asp:Label ID="lbl_lockout" runat="server" ForeColor="Red"></asp:Label></div>
        </form>
        <div class="forgot" align="center"><a href="Registration.aspx" />Don't have an account? Register here!</div>
    </div>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('SiteKey', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
