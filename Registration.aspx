<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Assignment.Registration" ValidateRequest = "false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/style.css"/>
    <link href="https://fonts.googleapis.com/css?family=Ubuntu" rel="stylesheet"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="path/to/font-awesome/css/font-awesome.min.css"/>
    <title>Registration</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LfwZGkeAAAAAO9s5qq45Y8BLC4tjEnKMUMwfuXa"></script>
    <script type="text/javascript">
        function vfname() {
            var str = document.getElementById('<%=tb_fname.ClientID %>').value;
            if (str.length < 3) {
                document.getElementById("lbl_fname").innerHTML = "First name should be at least 3 characters";
            }
            else {
                document.getElementById("lbl_fname").innerHTML = "";
            }
        }
        function vlname() {
            var str = document.getElementById('<%=tb_lname.ClientID %>').value;
            if (str.length < 3) {
                document.getElementById("lbl_lname").innerHTML = "Last name should be at least 3 characters";
            }
            else {
                document.getElementById("lbl_lname").innerHTML = "";
            }
        }
<%--        function vemail() {
            var mailformat = /^w+([.-]?w+)*@w+([.-]?w+)*(.w{2,3})+$/;
            var str = document.getElementById('<%=tb_email.ClientID %>').value;
            if (str != mailformat){
                document.getElementById("lbl_email").innerHTML = "y";
            }
            else {
                document.getElementById("lbl_email").innerHTML = "o";
            }
        }--%>
        function vpassword() {
            var str = document.getElementById('<%=tb_password.ClientID %>').value;

            if (str.length < 12) {
                document.getElementById("lbl_pwchecker").innerHTML = "Password length must be at least 12 characters";
                document.getElementById("lbl_pwchecker").style.color = "Red";
                return ("too_short");
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwchecker").style.color = "Red";
                return ("no_number");
            }

            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwchecker").innerHTML = "Password require at least 1 upper case";
                document.getElementById("lbl_pwchecker").style.color = "Red";
                return ("no_upper");
            }

            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwchecker").innerHTML = "Password require at least 1 lower case";
                document.getElementById("lbl_pwchecker").style.color = "Red";
                return ("no_upper");
            }

            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("lbl_pwchecker").innerHTML = "Password require at least 1 special character";
                document.getElementById("lbl_pwchecker").style.color = "Red";
                return ("no_upper");
            }
            document.getElementById("lbl_pwchecker").innerHTML = "Excellent!";
            document.getElementById("lbl_pwchecker").style.color = "mediumaquamarine";
        }
        function vcredit() {
            var str = document.getElementById('<%=tb_creditcard.ClientID %>').value;
            if (str.length < 8 || str.length > 8  ) {
                document.getElementById("lbl_creditcard").innerHTML = "Credit card info should be 8 digits";
            }
            else {
                document.getElementById("lbl_creditcard").innerHTML = "";
            }
        }
    </script>
</head>
<body>
    <div class="main" style="margin-top:50px; padding-bottom:380px; padding-left:30px; padding-right:30px">  
        <div class="sign" align="center" style="margin-bottom:-15px">Registration</div>
        <form class="form1" id="form1" runat="server">
            <asp:TextBox class="un" ID="tb_fname" runat="server" placeholder="First Name" onkeyup="javascript:vfname()"></asp:TextBox>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:-10px"><asp:Label ID="lbl_fname" runat="server" ForeColor="Red"></asp:Label></div>
            <br /><br />          
            <asp:TextBox class="un" ID="tb_lname" runat="server" placeholder="Last Name" onkeyup="javascript:vlname()"></asp:TextBox>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:-10px"><asp:Label ID="lbl_lname" runat="server" ForeColor="Red"></asp:Label></div>
            <br /><br />    
            <asp:TextBox class="un" ID="tb_dob" runat="server" placeholder="Date of Birth" TextMode="Date"></asp:TextBox>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:-27px"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_dob" ErrorMessage="Please select a date" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <br /><br />  
            <asp:TextBox class="un" ID="tb_email" runat="server" placeholder="Email"></asp:TextBox>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:-27px"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_email" ErrorMessage="Please enter your email" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <br /><br />  
            <asp:TextBox class="pass" ID="tb_password" runat="server" placeholder="Password" TextMode="Password" onkeyup="javascript:vpassword()"></asp:TextBox>
            <br /> 
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:-10px"><asp:Label ID="lbl_pwchecker" runat="server"></asp:Label></div>
            <br /> <br />  
            <asp:TextBox class="un" ID="tb_creditcard" runat="server" placeholder="Credit Card"  onkeyup="javascript:vcredit()"></asp:TextBox>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:-10px"><asp:Label ID="lbl_creditcard" runat="server" ForeColor="Red"></asp:Label></div>
            <br /><br /> 
            <asp:FileUpload class="un" ID="fu_photo" runat="server" placeholder="Proflie Picture"/>
            <div class="forgot" align="center" style="margin-top:-35px; margin-bottom:-10px"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fu_photo" ErrorMessage="Please upload an image" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <br /><br />
            <asp:Button class="submit" ID="btn_submit" runat="server" Text="Confirm" OnClick="btn_submit_Click"/>
            <asp:Label ID="lb_error1" runat="server"></asp:Label>
            <asp:Label ID="lb_error2" runat="server"></asp:Label>
        </form>
        <div class="forgot" align="center"><a href="Login.aspx" />Already own an account? Login here!</div>

    </div>
    <%--<form id="form1" runat="server">
        <div>
            <p><strong>Registration</strong></p>
            <asp:Label ID="lbl_fname" runat="server" Text="First Name: "></asp:Label>
            <asp:TextBox ID="tb_fname" runat="server" Height="20px" Width="140px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_fname" ErrorMessage="Please enter your first name" ForeColor="Red"></asp:RequiredFieldValidator>
            <br /><br />            
            <asp:Label ID="lbl_lname" runat="server" Text="Last Name: "></asp:Label>
            <asp:TextBox ID="tb_lname" runat="server" Height="20px" Width="140px"></asp:TextBox>
            <br /><br />    
            <asp:Label ID="lbl_dob" runat="server" Text="Date of Birth: "></asp:Label>
            <asp:TextBox ID="tb_dob" runat="server" Height="20px" Width="140px"></asp:TextBox>
            <br /><br />  
            <asp:Label ID="lbl_email" runat="server" Text="Email: "></asp:Label>
            <asp:TextBox ID="tb_email" runat="server" Height="20px" Width="140px"></asp:TextBox>
            <br /><br />  
            <asp:Label ID="lbl_password" runat="server" Text="Password: "></asp:Label>
            <asp:TextBox ID="tb_password" runat="server" onkeyup="javascript:pwvalidate()" TextMode="Password" Height="20px" Width="140px"></asp:TextBox>
            <asp:Label ID="lbl_pwchecker" runat="server"></asp:Label>
            <br /><br />  
            <asp:Label ID="lbl_ccard" runat="server" Text="Credit Card: "></asp:Label>
            <asp:TextBox ID="tb_creditcard" runat="server" Height="20px" Width="140px"></asp:TextBox>
            <br /><br /> 
            <asp:Label ID="lbl_photo" runat="server" Text="Photo: "></asp:Label>
            <asp:FileUpload ID="fu_photo" runat="server"/>
            <br /><br />
            <br /> 
            <asp:Button ID="btn_submit" runat="server" Text="Confirm" OnClick="btn_submit_Click"  Height="28px" Width="150px"/>
            <br />
            <asp:Label ID="lb_error1" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lb_error2" runat="server"></asp:Label>
    
        </div>
    </form>--%>
</body>
</html>
