<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListMembers.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.ListMembers" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>

<style type="text/css">
.gridViewPager td
{
	padding-left: 4px;
	padding-right: 4px;
	padding-top: 1px;
	padding-bottom: 2px;
}
.CommandButtonLetter 
{ font-size:1.3em; }
</style>

<div class="dnnForm" id="form-search">
    <div style="float: right;">
       
        <br />
        <asp:Label ID="lblDebug" runat="server" Text="" /></div>
    <fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblCriteria" runat="server" ControlName="txtSearch" Suffix=":" />
            <asp:TextBox ID="txtSearch" runat="server" Text="" />&nbsp; 
           
<asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/DesktopModules/GIBS_TimeTracker/images/search.png" AlternateText="Search" ImageAlign="Bottom" Width="30px" ToolTip="Search"
                OnClick="btnSearch_Click"></asp:ImageButton>
            
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblSearchField" runat="server" ControlName="ddlSearchType" Suffix=":" />
            <asp:DropDownList ID="ddlSearchType" runat="server" >
            <asp:ListItem Text="First Name" Value="FirstName" />
            <asp:ListItem Text="Last Name" Value="LastName" Selected="True" />
       
            <asp:ListItem Text="City" Value="City" />
             <asp:ListItem Text="Email" Value="Email" />
            
            </asp:DropDownList>
        </div>



        <div class="dnnFormItem">
            <dnn:Label ID="lblOrderBy" runat="server" ControlName="ddlOrderBy" Suffix=":" />
            <asp:DropDownList ID="ddlOrderBy" runat="server" >
            <asp:ListItem Text="First Name" Value="FirstName" />
            <asp:ListItem Text="Last Name" Value="LastName" Selected="True" />
           
            <asp:ListItem Text="City" Value="City" />
                <asp:ListItem Text="Email" Value="Email" />
           
            </asp:DropDownList>
        </div>
    </fieldset>
         <div style="float: right;padding-bottom:10px;"> <asp:Button ID="btnAddNewUser" resourcekey="btnAddNewUser" runat="server" CssClass="btn btn-primary"
            Text="Button" OnClick="btnAddNewUser_Click" />
         </div>
</div>
<asp:Panel ID="plLetterSearch" runat="server" HorizontalAlign="Center">
    <asp:Repeater ID="rptLetterSearch" runat="server">
        <ItemTemplate>
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="CommandButtonLetter" NavigateUrl='<%# FilterURL(Container.DataItem.ToString(),"1") %>'
                Text='<%# Container.DataItem %>'>
            </asp:HyperLink>&nbsp;&nbsp;
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>



<asp:DataGrid ID="grdUsers" AutoGenerateColumns="false" Width="100%" CellPadding="2"
    GridLines="None" CssClass="table table-striped table-bordered table-list" runat="server" 
    OnPageIndexChanged="dnnGrid_PageIndexChanged">
    <PagerStyle CssClass="NormalBold" />
    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
    <ItemStyle CssClass="dnnGridItem" />
    <AlternatingItemStyle CssClass="dnnGridAltItem" />

    <Columns>
        <dnn:ImageCommandColumn CommandName="Edit" ImageURL="~/DesktopModules/GIBS_TimeTracker/images/edit-32.png" EditMode="URL" ItemStyle-Width="60px"
            HeaderText="Edit" KeyField="UserID" />
		        <dnn:ImageCommandColumn CommandName="CheckInOut" ImageURL="~/Icons/Sigma/Refresh_32x32_Standard.png" EditMode="URL" ItemStyle-Width="60px"
            HeaderText="Check In-Out" KeyField="UserID" />      
        
<asp:TemplateColumn HeaderText="Photo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">    
            <ItemTemplate><asp:Image ID="PhotoID" CssClass="hover-zoom" runat="server" Height="40px" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("IDPhoto"))%>'></asp:Image>       
            </ItemTemplate>
        </asp:TemplateColumn>

        <dnn:TextColumn DataField="UserName" HeaderText="Username" Visible="true" />
        <dnn:TextColumn DataField="FirstName" HeaderText="First Name" />
        <dnn:TextColumn DataField="LastName" HeaderText="Last Name" />
        <dnn:TextColumn DataField="Email" HeaderText="Email" Visible="true" />
        <dnn:TextColumn DataField="DisplayName" HeaderText="DisplayName" Visible="false" />
        <dnn:TextColumn DataField="Street" HeaderText="Street" Visible="false" />
        <asp:TemplateColumn HeaderText="Address">
            <ItemTemplate>
                <asp:Label ID="FullAddress" Text='<%# Eval("Street") + ", " + Eval("City") + ", " + GetStateLookup(Eval("State")) + " " + Eval("PostalCode") %>'
                    runat="server" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
</asp:DataGrid>
<div style="text-align: center; margin: 0 auto;">
    <dnn:PagingControl ID="ctlPagingControl" runat="server" Visible="false" CssClass="dnnGridHeader"
        BorderWidth="2px" BorderColor="Black"></dnn:PagingControl>
</div>