<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="bizapps_test.WEB.MainPage" %>



<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
   
    <title>Главная страница </title>
    </asp:Content> 


<asp:Content ID="Content1" CssClass="container" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
          
               <asp:GridView ID="main_gridview"    OnPageIndexChanging="main_gridview_PageIndexChanging" CssClass="postgridview" ShowHeader="False" GridLines="None" Width="100%" CellPadding="0"    AutoGenerateColumns="false"  AllowPaging="true" runat="server"   PageSize="3">
                   <Columns>
                    <asp:TemplateField>
                       <ItemTemplate>
                           <div class=" blogpanel">
                              <img src=<%# Eval("PostImage") %> class="postimage" >
                              <h2>  <%# Eval("Title") %>  </h2> 
                               <p>  <%# Eval("CreationDate") %>  </p>
                              <button id="ButtonShowMore" OnServerClick="ButtonShowMore_OnServerClick" class="ButtonShowMore" runat="server">
                                  Read More
                                  <i class="glyphicon  glyphicon-arrow-right"></i>
                              </button> 
                               <br/>
                               <asp:HiddenField ID="PostId" Value=<%# Eval("PostId") %>  runat="server" />
                              <p> Categories:  <%# Eval("Categories") %>  </p>
                           </div>
                          
                       </ItemTemplate>

                   </asp:TemplateField>

                   </Columns>
               <%--   <PagerTemplate>
                     <table>
                           <tr>
                             
                              <td>
                                   <asp:Button CommandName="Page" CommandArgument="Prev" Text="Previous" runat="server" />
                               </td>
                               <td>
                                <asp:Repeater runat="server" ItemType="System.Int32" SelectMethod="GetPages" OnItemCommand="OnRepeaterCommand">
                                       <ItemTemplate>
                                           <asp:LinkButton CommandName="Page" CommandArgument="<%# Item %>" Text="<%# Item %>" runat="server" />
                                       </ItemTemplate>
                                   </asp:Repeater>
                            </td>
                              <td>
                                   <asp:Button CommandName="Page" CommandArgument="Next" Text="Next" runat="server" />
                               </td>
                               
                       </tr>
                       </table>
                  </PagerTemplate>--%>

                <PagerStyle HorizontalAlign ="Center" Height="50px"   />
               </asp:GridView>
          
</asp:Content>



