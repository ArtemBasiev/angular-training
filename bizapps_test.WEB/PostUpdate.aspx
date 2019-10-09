<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PostUpdate.aspx.cs" Inherits="bizapps_test.WEB.PostUpdate" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title>Post change</title>
</asp:Content> 


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PostViewContainer">
        <p>
            <b>   <asp:Label Text="Choose post image:" runat="server"/>  </b> 
            <label >   <asp:FileUpload ID="ImageFileUpload" runat="server" />  </label> 
        </p>  <br/>
        <p style="margin-bottom: 0; padding-bottom: 5px; padding-top: 5px;">
            <b>    <asp:Label Text="Input post title:" runat="server"/> </b>
          
            <asp:TextBox ID="textboxPostTitle" CssClass="textboxPostTitle" Width="400px" runat="server"/>
        </p>
        <asp:RequiredFieldValidator ControlToValidate="textboxPostTitle" ErrorMessage="Title must be specified"  ForeColor="Red" runat="server"/>
        <asp:RegularExpressionValidator ControlToValidate="textboxPostTitle" ErrorMessage="Title contains invalid characters" ValidationExpression="[\w|,.!? ]*" ForeColor="Red" runat="server"/>
        <br/>
        <b> <asp:Label Text="Input post content:" runat="server"/>  </b>
        
        <textarea class="divBodyHolder preBodyHolder" id="preBodyHolder"  runat="server"> </textarea> <br/>
          
        <asp:Panel ID="CategoryCheckBoxPanel" CssClass="CategoryCheckBoxPanel" runat="server"></asp:Panel>
        
        <asp:Button ID="ButtonCreatePost"  Text="Save changes" CssClass="buttonOnWhitePanel" OnClick="ButtonUpdatePost_Click" runat="server"/>

        <button id="ButtonDelete"  class="buttonOnWhitePanel" style="background-color: #c63632;" data-toggle="modal" data-target="#ModalDelete">Delete post</button>
        <div id="ModalDelete" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal">x</button>
                        <h4 class="modal-title">Are you sure you want to delete this post? You cannot undo this action.</h4>
                    </div>
                    <div class="modal-body">
                        <button class="btn btn-danger" CausesValidation="False" OnServerClick="ButtonDeletePost_OnClick" runat="server">Conform</button>
                        <button class="btn btn-info" data-dismiss="modal"> Cancel </button>
                    </div>

                </div>
            </div>
        </div>

        <asp:Label ID="LabelMes" runat="server"/>
    </div>
</asp:Content>