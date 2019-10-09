<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AutorizationPage.aspx.cs" Inherits="bizapps_test.WEB.AutorizationPage" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>LogIn</title>
    </asp:Content> 



        <asp:Content ID="Content1" CssClass="container center-block" CssStyle="float: none; margin: 0 auto;" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         
                 <div class="container col-lg-5 " id="div_form_aut">
                     
                   <div class="form-group ">
                       <asp:TextBox ID="TextboxLogin"  CssClass="form-control" placeholder="Login"   runat="server" />
                       <asp:Label ID="LoginHelp" CssClass="help-block"  runat="server"></asp:Label>
                   </div>
              
                  <div class="form-group ">
                     <input  type="password" id="PasswordBox"  class="form-control" placeholder="Password" runat="server" />
                      <asp:Label ID="PasswordHelp" CssClass="help-block"  runat="server"></asp:Label>
                  </div>
             
                 <div class="form-group  col-lg-3">
                     <asp:Button ID="ButtonLogin" CssClass="form-control btn-success" OnClick="ButtonLogin_Click" Text="Login" runat="server"/>
                 </div>     
             </div>
         
             
   
        </asp:Content>

