<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="bizapps_test.WEB.Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Выберите пользователя</title>
    </asp:Content> 



<asp:Content ID="Content2" CssClass="container centered" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   
        <div class="container centered"  id="div_links">
                <asp:HyperLink runat="server" Class="alert-link" NavigateUrl="~/UserCreation.aspx"><asp:Label ID="Label1" runat="server" Text="Создать пользователя"></asp:Label><br/></asp:HyperLink><br/>
                <asp:HyperLink runat="server" NavigateUrl="~/CategoryCreation.aspx" ><asp:Label ID="Label2" runat="server" Text="Создать категорию"></asp:Label><br/></asp:HyperLink><br/>
      </div>
        </asp:Content>

<asp:Content ID="Content1" CssClass="container center-block" CssStyle="float: none; margin: 0 auto;" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="container"  id="div_list_user">
           <form class="center-block" runat="server">
                 <asp:Label ID="LabelUsers" CssClass="text-danger" runat="server" Text="Пользователи" CssStyle="float:none"></asp:Label><br/>
                 <asp:BulletedList ID="UserList"  runat="server"  DisplayMode="LinkButton" OnClick="UserList_Click">
                     
                 </asp:BulletedList>

           </form>
              
          
      </div>
   
    </asp:Content>
