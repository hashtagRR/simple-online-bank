<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="home.Master" CodeFile="Transactions.aspx.cs" Inherits="Pages_Transactions" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="row">
        <h2>Make a Transaction</h2>
        					<div class="form-group">
							<label for="name" class="cols-sm-2 control-label">Receiver's Account number</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<asp:TextBox ID="ac_no" runat="server"></asp:TextBox>
								</div>
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
               <asp:Button ID="Button1" class="btn btn-primary btn-lg btn-block login-button" runat="server" Text="Submit" OnClick="Button1_Click" />
									</div>
						

    </div>
    <div class="row">
<h2>Transaction History</h2>
           
  <table class="table table-hover">
      <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
      <asp:GridView ID="dgv1" Class="table table-striped table-bordered table-hover"
  runat="server"></asp:GridView>
  </table>

    </div>
  </div>
</asp:Content>
