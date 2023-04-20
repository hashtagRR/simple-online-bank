<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="home.Master" CodeFile="Bills.aspx.cs" Inherits="Pages_Bills" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="row">
        <h2>Make a Bill Payement</h2>
        					<div class="form-group">
                                <label for="email" class="cols-sm-2 control-label">Payment Type</label>
							<div class="dropdown">
                                <asp:DropDownList ID="DL1" runat="server">
                                    <asp:ListItem Text="Electricity" Value="Electricity" />
                                    <asp:ListItem Text="Water" Value="Water" />
                                    <asp:ListItem Text="Telephone" Value="Telephone" />
                                    <asp:ListItem Text="Dish TV" Value="Dish TV" />
                                    <asp:ListItem Text="Credit Card" Value="Credit Card" />
                                </asp:DropDownList>
</div>
						</div>

						<div class="form-group">
							<label for="email" class="cols-sm-2 control-label">Amount</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-money fa" aria-hidden="true"></i></span>
									<asp:TextBox ID="amount" runat="server"></asp:TextBox>
								</div>
							</div>
						</div>
      
        	<div class="form-group ">
               <asp:Button ID="Button1" class="btn btn-primary btn-lg btn-block login-button" runat="server" Text="Submit" OnClick="Button1_Click"  />
									</div>
						

    </div>
    <div class="row">
<h2>Bill Payments History</h2>
   <table class="table table-hover">
      <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
      <asp:GridView ID="dgv1" Class="table table-striped table-bordered table-hover"
  runat="server"></asp:GridView>
  </table>

    </div>
  </div>
</asp:Content>
