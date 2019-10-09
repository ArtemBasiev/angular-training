<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoryCreation.aspx.cs" Inherits="bizapps_test.WEB.CategoryCreation" %>


    
   
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Category creation</title>
    </asp:Content> 

 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PostViewContainer">
        <h2 style="margin: 0;">
            Category creation  
        </h2>  <br/>
        <asp:BulletedList ID="CategoryList" CssClass="CategoryList" DisplayMode="LinkButton" OnClick="CategoryList_OnClick" runat="server"/>
        <p style="margin-bottom: 0; padding-bottom: 5px; padding-top: 5px;">
            <b>    <asp:Label Text="Input category name:" runat="server"/> </b>
          
            <asp:TextBox ID="textboxCategoryName" CssClass="textboxPostTitle" Width="200px" runat="server"/>
        </p>
        <asp:RequiredFieldValidator ControlToValidate="textboxCategoryName" ErrorMessage="Category name must be specified"  ForeColor="Red" runat="server"/>
        <asp:RegularExpressionValidator ControlToValidate="textboxCategoryName" ErrorMessage="Category name contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/>
        <br/>
        
        <asp:Button ID="ButtonCreateCategory" Text="Create category" CssClass="buttonOnWhitePanel" OnClick="cr_kat_Click" runat="server"/>
        <asp:Label ID="LabelMes" runat="server"/>
    </div>
</asp:Content>



