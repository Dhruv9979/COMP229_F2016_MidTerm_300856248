<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP229_F2016_MidTerm_300856248.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">

                <h1>Todo List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm">
                    <i class="fa fa-plus"></i> Add Todo
                </a>


                <div>
                    <label for="PageSizeDropDownList"> Records per page</label> 
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                        AutoPostBack="True" CssClass="btn btn-default btn-sm dropdown-toggle"
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>


                <asp:GridView ID="TodoGridView" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover" DataKeyNames="TodoID"
                    OnRowDeleting="TodoGridView_RowDeleting" AllowPaging="true" PageSize="3"
                    OnPageIndexChanging="TodoGridView_PageIndexChanging" AllowSorting="true"
                    OnSorting="TodoGridView_Sorting" OnRowDataBound="TodoGridView_RowDataBound"
                    PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="TodoID" HeaderText="Todo " Visible="true" />
                         
                        
                        <asp:BoundField DataField="TodoDescription" HeaderText="TodoDescription" Visible="true" />
                        
                        <asp:BoundField DataField="TodoNotes" HeaderText="Notes" Visible="true" />
                        
                        <asp:BoundField DataField="Completed" HeaderText="Completed" Visible="true" />


                        <asp:CommandField HeaderText="Edit" EditText="<i class='fa fa-info fa-lg'></i> Edit"
                            ShowEditButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-info btn-sm" />

                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                    </asp:GridView>
                </div>
            </div>
         </div>
</asp:Content>
