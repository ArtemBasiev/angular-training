<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemPost.ascx.cs" Inherits="bizapps_test.WEB.SpecialItems.ItemPost" %>
<div>
    <asp:Label ID="LabelPostTitle" runat="server"/><br/>
    <asp:Literal ID="LiteralBody"  runat="server"/><br/>
     <asp:Button ID="ButtonDelete" Text="Удалить" OnClick="ButtonDelete_Click" runat="server"/>
    <asp:Button ID="ButtonChange" Text="Редактировать" OnClick="ButtonChange_Click" runat="server"/> <br/>
    <asp:Label ID="LabelCategory" runat="server"/><br/><br/><br/>
</div>