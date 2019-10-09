<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangeUser.aspx.cs" Inherits="bizapps_test.WEB.ChangeUser" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Изменение пользователя</title>
    </asp:Content> 


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
         <asp:HyperLink ID="linkDefault" NavigateUrl="~/Default.aspx" Text="Выйти" runat="server" style="float: left;"/> 
        <asp:HyperLink ID="HyperLinkMainPage" NavigateUrl="~/MainPage.aspx" Text="На главную" runat="server" style="float: left; margin-left:30px;"/> 
  <br/>  <asp:Label ID="headlabel" runat="server" Text="Изменение пользователя"></asp:Label>   
</asp:Content>
 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div aria-orientation="horizontal" style="display: block; position: relative; left: 0px; height:400px; width:40%; height:500px; float: left; top: 0px; text-align: left; padding-left: 20px; padding-top: 20px;" id="div_deleteuser">
                 <asp:Button ID="ButtonDeleteUser" runat="server" Text="Удалить аккаунт" OnClick="ButtonDeleteUser_Click" ></asp:Button>
      </div>
      <div aria-orientation="horizontal" style="display: block; position: static; left: 0px; float:inherit; width:50%; top: 0px; text-align:center; padding-left: 40px; padding-top: 20px;" id ="div_changeuser">
                   <asp:Label ID="Label2" runat="server" Text="Изменение пользователя"></asp:Label> <br/><br/><br/>
                 <asp:Label ID="Label3" runat="server" Text="Введите логин"></asp:Label><br/><br/>
                 <asp:TextBox id="TextBoxUserName"  runat="server"/><br/>
          <asp:Label ID="Label4" runat="server" Text="Введите пароль"></asp:Label><br/><br/>
                 <asp:TextBox id="TextBoxUserPassword"  runat="server"/><br/>
          <asp:Label ID="Label5" runat="server" Text="Введите название блога"></asp:Label><br/><br/>
                 <asp:TextBox id="TextBoxBlogName"  runat="server"/><br/>
                 <asp:Label ID="LabelMes" runat="server" Font-Size="12px" ></asp:Label><br/><br/>
                 <asp:Button id="ButtonChangeUser" Text="Сохранить изменения" runat="server" OnClick="ButtonChangeUser_Click" /><br/>
     </div>
</asp:Content>