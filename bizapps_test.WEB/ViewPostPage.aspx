<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="True" CodeBehind="ViewPostPage.aspx.cs" Inherits="bizapps_test.WEB.ViewPostPage" %>



<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
   
    <title>View Post </title>
    <script>
        function displayEditComment(sender) {
            $(".button-open-modal").attr('disabled', false);
            if ($(sender).is(".button-open-modal")) {
                $(sender).attr('disabled', true); 
                $(sender).parent().prev(".divCommentEdit").toggle(function() {
                    if ($(this).is(":visible")) {
                        $(".divCommentEdit").hide();
                        $(this).show();
                        $(this).prev(".CommentText").hide();

                        //var i;
                        ////var commenttexts = new Array();
                        ////commenttexts = $(".divCommentEdit").prev("p").find("label");
                        //var teaxtareas = new Array();
                        //teaxtareas = $(".divCommentEdit").find(".EditCommentTextArea");
                        //for (i = 0; i < teaxtareas.length; i++) {

                        //    teaxtareas[i].innerText(teaxtareas[i].parent().prev("p").find("label").Text());
                        //};
                       
                       
                    } else {
                        return false;
                        //$(this).hide();
                        //$(this).prev(".CommentText").show();
                    };
                });

            } else {
               
                if ($(sender).is(".button-cancel")) {
                    ($(sender).parent()).parent().toggle(function() {
                        if ($(this).is(":visible")) {
                            $(this).prev(".CommentText").hide();
                            $(this).show();
                            


                        } else {
                            $(this).hide();
                            $(this).prev(".CommentText").show();
                        };
                    });
                }
            }
           
        };

        function displayReplyComment(sender) {
            $(".button-open-modal").attr('disabled', false);
            if ($(sender).is(".button-open-modal")) {
                $(sender).attr('disabled', true); 
                $(sender).parent().next(".divcommentreply").toggle(function() {
                    if ($(this).is(":visible")) {
                        $(".divcommentreply").hide();
                        $(this).show();                                             
                    };
                });

            } else {
               
                if ($(sender).is(".button-cancel")) {
                    ($(sender).parent()).parent().toggle(function() {
                        if ($(this).is(":visible")) {
                            $(this).show();
                            


                        } else {
                            $(this).hide();
                        };
                    });
                }
            }
           
        };

        function hideValidator(sender) {

                $(sender).parent().toggle(function() {
                        $(this).hide();

                });

          
           
        };

    </script>
</asp:Content> 



<asp:Content ID="Content1" CssClass="container" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   <asp:ScriptManager EnablePartialRendering="true" ID="CommentScriptManager" runat="server">
        <Scripts>
       
        </Scripts>
    </asp:ScriptManager>
    
    <%----------------------------------------------------------  post view-------------------------------------------%>
    <div class="PostViewContainer">
        <img class="postimage" id="imgpostview" src="Images/img-post-default.jpg" runat="server"/>
        <p style="text-align: justify;">   
            <h2 id="PostTitle" runat="server"></h2>
            <asp:Label ID="labelDate" CssClass="text-right" runat="server"></asp:Label>
        </p>
        
        

        <div class="divBodyHolder" id="divBodyHolder" runat="server">
     
        </div>
    <span id="markPostCategories"  runat="server">      
    </span> <br/>
        
      <asp:Button ID="ButtonChangePost" Text="Change post" Visible="False"  CssClass="buttonOnWhitePanel" OnClick="ButtonChangePost_OnClick" runat="server"/> <br/><br/>
        
        <%----------------------------------------------------------  comment container-------------------------------------------%>
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="container comment-container" id="commentcontainer" runat="server">
            <textarea id="CommentTextArea" placeholder="Join to discussion..." class="commentarea" runat="server"></textarea><br/>
            <p  class="comment-validator-container"> <asp:RequiredFieldValidator ValidationGroup="MainCommentTextArea" ControlToValidate="CommentTextArea"  CssClass="comment-validator" Display="Dynamic"  ErrorMessage="Comment Text is not set" ForeColor="White" runat="server">
                <span style="float: left;" class="glyphicon glyphicon-warning-sign"></span> <span style="float: left; margin-left: 10px;">Comment Text is not set</span> <button class="btn-closevalidator" onclick="hideValidator($(this));" type="button"><i class="glyphicon glyphicon-remove"></i></button>  </asp:RequiredFieldValidator>
                <span style="margin: 0;" Visible="False" id="servermessagebox" class="server-messagebox" runat="server">
                    <span style="float: left;" class="glyphicon glyphicon-warning-sign"></span> <span style="float: left; margin-left: 10px;" id="servermessageboxtext" runat="server"></span> <button  class="btn-closevalidator" onclick="hideValidator($(this));" type="button"><i class="glyphicon glyphicon-remove"></i></button>
                </span>
            </p>
           
            <asp:Button ID="ButtonPostComment" Text="Post" ValidationGroup="MainCommentTextArea" Visible="False"  CssClass="button-save-edit" OnClick="ButtonPostComment_OnClick" runat="server" />
            <p class="panel-edit"> 
                <asp:Label ID="LabelCommentUser" CssClass="commentarea-message" Text="Sign in to comment" runat="server"/>
               
            </p>  
            <asp:GridView ID="CommentGridview"  OnRowCommand="CommentGridview_OnRowCommand" OnRowCancelingEdit="CommentGridview_OnRowCancelingEdit"   OnRowUpdating="CommentGridview_OnRowUpdating"   OnRowDeleting="CommentGridview_OnRowDeleting" OnRowDataBound="CommentGridview_OnRowDataBound"   ShowHeader="False" GridLines="None" Width="100%"  CellPadding="0"    AutoGenerateColumns="false" runat="server"  >
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <div style="margin-top: 20px; width: 100%;">
                                <%----------------------------------------------------------  comment item-------------------------------------------%>
                                <div class="delete-button-activator">
                                     <p style="margin: 0; margin-bottom: 2px;">
                                     <label class="CommentUserName" id="CommentUserName" runat="server"><%# Eval("UserName") %></label> <label class="CommentDate"><%# Eval("CreationDate") %> </label> 
                                    </p>
                                    <p style="margin: 0; margin-bottom: 2px; width: 100%;" class="CommentText" > <asp:Label  ID="CommentText" Text=<%# Eval("CommentText") %> runat="server"></asp:Label>  </p>
                                    <%-------------------------------------------------  div with control for editing-------------------------------------------%>
                                   <div id="divCommentEdit" class="divCommentEdit">
                                        <textarea id="EditCommentTextArea"  placeholder="Edit comment..." class="commentarea" runat="server"><%# Eval("CommentText") %></textarea>
                                       <p  class="comment-validator-container"> <asp:RequiredFieldValidator  ValidationGroup="EditCommentTextArea" ControlToValidate="EditCommentTextArea"  CssClass="comment-validator" Display="Dynamic"  ErrorMessage="Comment Text is not set" ForeColor="White" runat="server">
                                               <span style="float: left;" class="glyphicon glyphicon-warning-sign"></span> <span style="float: left; margin-left: 10px;">Comment Text is not set</span> <button class="btn-closevalidator" onclick="hideValidator($(this));" type="button"><i class="glyphicon glyphicon-remove"></i></button>  </asp:RequiredFieldValidator>
                                           <span style="margin: 0;" Visible="False" id="Span1" class="server-messagebox" runat="server">
                                               <span style="float: left;" class="glyphicon glyphicon-warning-sign"></span> <span style="float: left; margin-left: 10px;" id="servermessagetextedit" runat="server"></span> <button  class="btn-closevalidator" onclick="hideValidator($(this));" type="button"><i class="glyphicon glyphicon-remove"></i></button>
                                           </span>
                                       </p>
                                         <asp:LinkButton ID="ButtonSaveCommentEdit" ValidationGroup="EditCommentTextArea"  Text="Save Edit"   CssClass="button-save-edit" CommandName="Update" runat="server" />
                                         <p  class="panel-edit"> 
                                             <asp:Label ID="LabelCommentUserEdit" CssClass="commentarea-message" Text="Sign in to comment" runat="server"/>
                                             <asp:LinkButton ID="ButtonCancelEdit"  CausesValidation="False" CommandName="Cancel" OnClientClick="displayEditComment($(this));" Text="Cancel"  CssClass="button-cancel" runat="server"/>      
                                         </p>
                                     
                                   </div>
                                    <%-------------------------------------------------  buttons for access to comment features-------------------------------------------%>
                                   <p style="margin: 0;" >
                                        <button class="button-open-modal" type="button"  id="openEditForm" Visible="False" onclick="displayEditComment($(this));"  runat="server">Edit</button>
                                        <asp:LinkButton CssClass="button-open-modal" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this comment? You cannot undo this action.')" CommandName="Delete"  ID="openDeleteCommentModal" Visible="False" Text="Delete"    runat="server"/>
                                       <button class="button-open-modal" type="button"  id="openReplyForm" Visible="False" onclick="displayReplyComment($(this));"  runat="server">Reply</button>
                                   </p>
                                  <%-------------------------------------------------  div with control for replying-------------------------------------------%>
                                      <div id="divCommentReply" class="divcommentreply">
                                        <textarea id="ReplyTextArea"  placeholder="Join the discussion..." class="commentarea" runat="server"></textarea>
                                       <p  class="comment-validator-container"> <asp:RequiredFieldValidator  ValidationGroup="ReplyTextArea" ControlToValidate="ReplyTextArea"  CssClass="comment-validator" Display="Dynamic"  ErrorMessage="Comment Text is not set" ForeColor="White" runat="server">
                                               <span style="float: left;" class="glyphicon glyphicon-warning-sign"></span> <span style="float: left; margin-left: 10px;">Comment Text is not set</span> <button class="btn-closevalidator" onclick="hideValidator($(this));" type="button"><i class="glyphicon glyphicon-remove"></i></button>  </asp:RequiredFieldValidator>
                                           <span style="margin: 0;" Visible="False" id="Span2" class="server-messagebox" runat="server">
                                               <span style="float: left;" class="glyphicon glyphicon-warning-sign"></span> <span style="float: left; margin-left: 10px;" id="Span3" runat="server"></span> <button  class="btn-closevalidator" onclick="hideValidator($(this));" type="button"><i class="glyphicon glyphicon-remove"></i></button>
                                           </span>
                                       </p>
                                         <asp:LinkButton ID="ButtonReply" ValidationGroup="ReplyTextArea" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"   CssClass="button-save-edit" CommandName="Reply" runat="server" />
                                         <p  class="panel-edit">
                                       
                                             <asp:Label ID="LabelCommentUserReply" CssClass="commentarea-message" Text="Sign in to comment" runat="server"/>
                                             <asp:LinkButton ID="ButtonCancelReplying"  CausesValidation="False" OnClientClick="displayReplyComment($(this));" CommandName="Cancel" Text="Cancel"  CssClass="button-cancel" runat="server"/>      
                                         </p>
                                     
                                     </div>
                                    <asp:HiddenField ID="CommentId" Value=<%# Eval("CommentId") %>  runat="server" />
                                </div>
                                <%-----------------------------------------------------------  gridview with dependent comments-------------------------------------------%>
                                <asp:GridView ID="DependentCommentGridview"   ShowHeader="False" GridLines="None" Width="100%" CellPadding="0"    AutoGenerateColumns="false" runat="server"  >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="dependentcomment-container">
                                                    <p style="margin: 0; margin-bottom: 2px;"> <label class="CommentUserName"><%# Eval("UserName") %></label> <label class="CommentDate"><%# Eval("CreationDate") %> </label>  </p>
                                                    <p style="margin: 0; margin-bottom: 2px;"> <%# Eval("CommentText") %> </p>
                                                    <br/>
                                                    <asp:HiddenField ID="DependentCommentId" Value=<%# Eval("CommentId") %>  runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <%-----------------------------------------------------------------------------------------------------------------------------------------%>

                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div> 
            </ContentTemplate>
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="ButtonPostComment" EventName="Click"/>
                 <asp:AsyncPostBackTrigger ControlID="CommentGridview" EventName="RowUpdating"/>
                 <asp:AsyncPostBackTrigger ControlID="CommentGridview" EventName="RowDeleting"/>
                 <asp:AsyncPostBackTrigger ControlID="CommentGridview" EventName="RowCancelingEdit"/>
             </Triggers>
        </asp:UpdatePanel>
        

         
    </div>

  
    

          
</asp:Content>
