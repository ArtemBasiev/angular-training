<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoryUpdateDelete.aspx.cs" Inherits="bizapps_test.WEB.CategoryUpdateDelete" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Category update</title>
</asp:Content> 

 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PostViewContainer">
        <h2 style="margin: 0;">
            Category update  
        </h2>  <br/>
        <p style="margin-bottom: 0; padding-bottom: 5px; padding-top: 5px;">
            <b>    <asp:Label Text="Input category name:" runat="server"/> </b>
          
            <asp:TextBox ID="textboxCategoryName" CssClass="textboxPostTitle" Width="200px" runat="server"/>
        </p>
        <asp:RequiredFieldValidator ControlToValidate="textboxCategoryName" ErrorMessage="Category name must be specified"  ForeColor="Red" runat="server"/>
        <asp:RegularExpressionValidator ControlToValidate="textboxCategoryName" ErrorMessage="Category name contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/>
        <br/>
        
        <asp:Button ID="ButtonSaveChanges" Text="Save changes" CssClass="buttonOnWhitePanel" OnClick="ButtonUpdateCategory_Click" runat="server"/>
        <button id="ButtonDelete"  class="buttonOnWhitePanel" style="background-color: #c63632;" data-toggle="modal" data-target="#ModalDelete">Delete category</button>
        <div id="ModalDelete" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal">x</button>
                        <h4 class="modal-title">Are you sure you want to delete this category? You cannot undo this action.</h4>
                    </div>
                    <div class="modal-body">
                        <button class="btn btn-danger" CausesValidation="False" OnServerClick="ButtonDeleteCategory_Click" runat="server">Conform</button>
                        <button class="btn btn-info" data-dismiss="modal"> Cancel </button>
                    </div>

                </div>
            </div>
        </div>
        <asp:Label ID="LabelMes" runat="server"/>
    </div>
</asp:Content>