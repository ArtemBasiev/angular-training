<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserCreation.aspx.cs" Inherits="bizapps_test.WEB.UserCreation" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Создание пользователя</title>
    </asp:Content> 


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
         <asp:HyperLink ID="linkDefault" NavigateUrl="~/Default.aspx" Text="Выйти" runat="server" style="float: left;"/> 
   <br/> <asp:Label ID="headlabel" runat="server" Text="Создание пользователя"></asp:Label>   
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div aria-orientation="horizontal" style="display: block; position: relative; left: 0px; float:inherit; top: 0px; text-align:center; padding-left: 20px; padding-top: 20px;" id="div_list_user">
                 <asp:Label ID="LabelLogin" runat="server" Text="Введите логин"></asp:Label><br/><br/>
                 <asp:TextBox id="LoginText"  runat="server"/><br/><br/><br/>

                 <asp:Label ID="LabelPassword" runat="server" Text="Введите пароль"></asp:Label><br/><br/>
                 <asp:TextBox id="TextPassword"  runat="server"/><br/><br/><br/>

                 <asp:Label ID="LabelBlog" runat="server" Text="Введите название блога"></asp:Label><br/><br/>
                 <asp:TextBox id="TextBlog"  runat="server"/><br/><br/><br/>
                 <asp:Label ID="LabelMes" runat="server" Font-Size="12px" ></asp:Label><br/><br/>
                 <asp:Button id="ButtonCreateUser" Text="Создать пользователя" runat="server" OnClick="ButtonCreateUser_Click" /><br/>
      </div>
</asp:Content>