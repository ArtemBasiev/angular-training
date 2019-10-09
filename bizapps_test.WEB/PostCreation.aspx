<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PostCreation.aspx.cs" Inherits="bizapps_test.WEB.PostCreation" %>



<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Post creation</title>
    </asp:Content> 


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PostViewContainer">
      <p>
         <b>   <asp:Label Text="Choose post image:" runat="server"/>  </b> 
        <label >   <asp:FileUpload ID="ImageFileUpload"  runat="server" />  </label> 
      </p>  <br/>
        <p style="margin-bottom: 0; padding-bottom: 5px; padding-top: 5px;">
            <b>    <asp:Label Text="Input post title:" runat="server"/> </b>
          
            <asp:TextBox ID="textboxPostTitle" CssClass="textboxPostTitle" Width="400px" runat="server"/>
        </p>
        <asp:RequiredFieldValidator ControlToValidate="textboxPostTitle" ErrorMessage="Title must be specified"  ForeColor="Red" runat="server"/>
        <asp:RegularExpressionValidator ControlToValidate="textboxPostTitle" ErrorMessage="Title contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/>
        <br/>
        <b> <asp:Label Text="Input post content:" runat="server"/>  </b>
        
        <textarea class="divBodyHolder preBodyHolder" id="preBodyHolder"  runat="server">
          
        </textarea>
        <br/>
        <asp:Panel ID="CategoryCheckBoxPanel" CssClass="CategoryCheckBoxPanel" runat="server"></asp:Panel>
        
        <asp:Button ID="ButtonCreatePost" Text="Create post" CssClass="buttonOnWhitePanel" OnClick="ButtonCreatePost_Click" runat="server"/>
        <asp:Label ID="LabelMes" runat="server"/>
    </div>
</asp:Content>
