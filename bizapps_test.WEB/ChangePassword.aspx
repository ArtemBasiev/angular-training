<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.Master" CodeBehind="ChangePassword.aspx.cs" Inherits="bizapps_test.WEB.ChangePassword" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Change Password</title>
</asp:Content> 


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PostViewContainer">
        <h2> Change password</h2>
   
        <asp:TextBox ID="textboxNewPassword" TextMode="Password" CssClass="textboxPostTitle" placeholder="Password..." Width="300px" runat="server"/><br/>
        <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="textboxNewPassword" ErrorMessage="Password must be entered"  ForeColor="Red" runat="server"/>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="textboxNewPassword" ErrorMessage="Password contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/> <br/>

        <asp:TextBox ID="textboxPasswordRepeat" TextMode="Password" placeholder="Repeat password..." CssClass="textboxPostTitle" Width="300px" runat="server"/><br/>
        <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="textboxPasswordRepeat" ErrorMessage="Password must be entered"  ForeColor="Red" runat="server"/>
        <asp:CompareValidator Display="Dynamic" ControlToValidate="textboxPasswordRepeat" ErrorMessage="Password are not equal" ForeColor="Red" ControlToCompare="textboxNewPassword" runat="server"/>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="textboxPasswordRepeat" ErrorMessage="Password contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/><br/>
        
        
        <asp:Button ID="ButtonChangePassword" Text="Change password" CssClass="buttonOnWhitePanel" OnClick="ButtonChangePassword_OnClick"   runat="server"/>
        <asp:Label ID="LabelMes" runat="server"/>
    </div>
</asp:Content>