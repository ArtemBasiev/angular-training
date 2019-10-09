<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.Master" CodeBehind="CreateUser.aspx.cs" Inherits="bizapps_test.WEB.CreateUser" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Sign In</title>
</asp:Content> 


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PostViewContainer">
        <h2> Sign In</h2>
   
        <asp:TextBox ID="textboxUserName"  CssClass="textboxPostTitle" placeholder="User name..." Width="300px" runat="server"/><br/>
        <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="textboxUserName" ErrorMessage="User name must be entered"  ForeColor="Red" runat="server"/>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="textboxUserName" ErrorMessage="User name contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/> <br/>

        <asp:TextBox ID="textboxPassword" TextMode="Password" placeholder="Password..." CssClass="textboxPostTitle" Width="300px" runat="server"/><br/>
        <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="textboxPassword" ErrorMessage="Password must be entered"  ForeColor="Red" runat="server"/>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="textboxPassword" ErrorMessage="Password contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/><br/>
        
        
        <asp:Button ID="ButtonSignIn" Text="Sign In" CssClass="buttonOnWhitePanel" OnClick="ButtonSignIn_OnClick"   runat="server"/>
        <asp:Label ID="LabelMes" runat="server"/>
    </div>
</asp:Content>
