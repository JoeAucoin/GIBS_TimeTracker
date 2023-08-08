<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckInOutReport.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.CheckInOutReport" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>



<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css" />
<script type="text/javascript">

    $(function () {
        $("#txtStartDate").datepicker({
            numberOfMonths: 2,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });
        $("#txtEndDate").datepicker({
            numberOfMonths: 2,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });
    });

</script>


<asp:Label ID="LabelDebug" runat="server" CssClass="dnnFormSuccess" Text=""></asp:Label>

<div style=" float:right">
<asp:Button ID="btnGetSchedule" runat="server" Text="Button" ResourceKey="btnGetLoginReport" onclick="btnGetSchedule_Click" CssClass="dnnPrimaryAction" /></div>
<div class="dnnForm" id="form-demo">
    <fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblStartDate" runat="server" CssClass="dnnFormLabel" AssociatedControlID="txtStartDate" Text="Start Date" />
            <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblEndDate" runat="server" CssClass="dnnFormLabel" AssociatedControlID="txtEndDate" Text="End Date" />
            <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" />
        </div>		
    </fieldset>
</div>	


<asp:GridView ID="gv_Report" runat="server" HorizontalAlign="Center" OnRowDataBound="gv_Report_DataBound"  
    AutoGenerateColumns="False" CssClass="dnnGrid" OnSorting="gv_Report_Sorting"   
    resourcekey="gv_ReportDetails" EnableViewState="False" DataKeyNames="TimeTrackerID" >
    <HeaderStyle CssClass="dnnGridHeader" />
    <RowStyle CssClass="dnnGridItem" />
<AlternatingRowStyle CssClass="dnnGridAltItem" />     
<PagerStyle CssClass="pgr" />  
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>
        
        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>      
                                <asp:HyperLink ID="HyperLink1" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/DesktopModules/GIBS_TimeTracker/images/edit-32.png" AlternateText="Edit Record" /></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>
         

        <asp:TemplateField HeaderText="Photo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">    
            <ItemTemplate><asp:Image ID="PhotoID" CssClass="hover-zoom" runat="server" Height="40px" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("IDPhoto"))%>'></asp:Image>       
            </ItemTemplate>
        </asp:TemplateField>	

    <asp:BoundField HeaderText="TimeTrackerID" Visible="false" DataField="TimeTrackerID" SortExpression="TimeTrackerID" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
      <asp:BoundField HeaderText="WorkDate" DataField="WorkDate" SortExpression="WorkDate"  ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
      
      
        <asp:BoundField HeaderText="Name" DataField="DisplayName" SortExpression="DisplayName" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

		<asp:BoundField HeaderText="StartTime" DataField="StartTime" SortExpression="StartTime" DataFormatString="{0:hh:mm tt}" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="EndTime" DataField="EndTime" SortExpression="EndTime" DataFormatString="{0:hh:mm tt}" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
		<asp:BoundField HeaderText="Hours" DataField="TotalTime" SortExpression="TotalTime" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Email" DataField="Email" Visible="false" SortExpression="Email" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
        


    </Columns>
</asp:GridView>	