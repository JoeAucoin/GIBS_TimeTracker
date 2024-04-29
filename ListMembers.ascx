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

<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" CssClass="table table-striped table-bordered table-list" 
    OnPageIndexChanged="gvUsers_PageIndexChanged" OnPageIndexChanging="gvUsers_PageIndexChanging" DataKeyNames="UserID" OnRowDataBound="gvUsers_RowDataBound" AllowCustomPaging="true">
   
    <Columns>
        <asp:BoundField HeaderText="ID" DataField="USERID" />

        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="60px">
            <ItemTemplate>
            <asp:HyperLink ID="HyperLinkEditUser" runat="server" 
                NavigateUrl="" >
                <asp:Image ID="Image1" ImageUrl="~/DesktopModules/GIBS_TimeTracker/images/edit-32.png" AlternateText="Edit" ToolTip="Edit" runat="server" /> </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Make ID Card" ItemStyle-Width="66px">
            <ItemTemplate>
            <asp:HyperLink ID="HyperLinkMakeIDCard" runat="server" 
                NavigateUrl=""  Target="_blank" ><asp:Image ID="Image2" AlternateText="Make ID Card" ImageUrl="~/Icons/Sigma/Rt_32x32_Standard.png" ToolTip="Make ID Card" runat="server" /></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Check In-Out" ItemStyle-Width="66px">
            <ItemTemplate>
            <asp:HyperLink ID="HyperLinkCheckInOut" runat="server" 
                NavigateUrl="" ><asp:Image ID="Image3" ImageUrl="~/Icons/Sigma/Refresh_32x32_Standard.png" AlternateText="Check in-Out" ToolTip="Check in-Out" runat="server" /></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Add Shift" ItemStyle-Width="66px">
            <ItemTemplate>
            <asp:HyperLink ID="HyperLinkAddShift" runat="server" 
                NavigateUrl="" ><asp:Image ID="Image4" ImageUrl="~/Icons/Sigma/Add_32X32_Standard.png" AlternateText="Add Shift" ToolTip="Add Shift" runat="server" /></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Photo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" Visible="false">
            <ItemTemplate><asp:Image ID="PhotoID" CssClass="hover-zoom" runat="server" Height="40px" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("IDPhoto"))%>'></asp:Image>       
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:BoundField HeaderText="FirstName" DataField="FirstName" />
        <asp:BoundField HeaderText="LastName" DataField="LastName" />
        <asp:BoundField HeaderText="Email" DataField="Email" />
        <asp:BoundField HeaderText="DisplayName" DataField="DisplayName" Visible="false" />
        <asp:BoundField HeaderText="Street" DataField="Street" Visible="false" />
        <asp:BoundField HeaderText="City" DataField="City" Visible="false" />
         <asp:TemplateField HeaderText="Address" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
                 <asp:Label ID="FullAddress" Text='<%# Eval("Street") + ", " + Eval("City") + ", " + GetStateLookup(Eval("State")) + " " + Eval("PostalCode") %>'
                    runat="server" />
             </ItemTemplate>
             </asp:TemplateField>

    </Columns>

    
    </asp:GridView>



<div style="text-align: center; margin: 0 auto;">
    <dnn:PagingControl ID="ctlPagingControl" runat="server" Visible="false" CssClass="dnnGridHeader" Mode="URL"
        BorderWidth="2px" BorderColor="Black"></dnn:PagingControl>
</div>


<asp:DataGrid ID="grdUsers" AutoGenerateColumns="false" Width="100%" CellPadding="2"
    GridLines="None" CssClass="table table-striped table-bordered table-list" runat="server"  OnItemDataBound="grdUsers_ItemDataBound"
    OnPageIndexChanged="dnnGrid_PageIndexChanged">
    
    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
    <ItemStyle CssClass="dnnGridItem" />
    <AlternatingItemStyle CssClass="dnnGridAltItem" />

    <Columns>
        <dnn:ImageCommandColumn CommandName="Edit" ImageURL="~/DesktopModules/GIBS_TimeTracker/images/edit-32.png" EditMode="URL" ItemStyle-Width="60px"
            HeaderText="Edit" KeyField="UserID" />
		<dnn:ImageCommandColumn CommandName="CheckInOut" ImageURL="~/Icons/Sigma/Refresh_32x32_Standard.png" EditMode="URL" ItemStyle-Width="60px"
        HeaderText="Check In-Out" KeyField="UserID" />      
		    <dnn:ImageCommandColumn CommandName="MakeID" ImageURL="~/Icons/Sigma/Rt_32x32_Standard.png" EditMode="URL" ItemStyle-Width="60px"
        HeaderText="Make ID Card" KeyField="UserID" />  


        
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
       
    </Columns>
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
</asp:DataGrid>
