<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #image-bac {
            width: 800px;
            height: 430px;
            margin-left: auto;
            margin-right: auto;
            background-image: url('images/login.jpg');
            background-size: cover;
        }

        #container {
            width: 253px;
            height: 430px;
            padding-top: 30px;
            margin-left: 531px;
        }
    </style>
    <h2></h2><br /><br />
  
     <div id="image-bac">
    <div id="form" >
            <div id="container">
            <h1>User Login</h1>
	<div class="form-group input-group">
		<div class="input-group-prepend">
		    <span class="input-group-text"> <i class="fa fa-user"></i> </span>
		 </div>
        <asp:TextBox runat="server" ID="un" type="text" class="form-control" placeholder="User Name"></asp:TextBox>
    </div> <!-- form-group// -->
    
    <div class="form-group input-group">
    	<div class="input-group-prepend">
		    <span class="input-group-text"> <i class="fa fa-lock"></i> </span>
		</div>
        <asp:TextBox runat="server" ID="pw" type="password" class="form-control" placeholder="Password"></asp:TextBox>
    </div> <!-- form-group// -->
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <div class="form-group">
        <asp:Button ID="Button2" class="btn btn-success" runat="server" Text="Login" OnClick="Btn_login" />
        <asp:Button ID="Button1" class="btn btn-success" runat="server" Text="Clear" OnClick="Btn_clear" />
    </div> <!-- form-group// -->  
                </div>
    
         </div>
       </div>


</asp:Content>
