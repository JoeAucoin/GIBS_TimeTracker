<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.Edit" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/smoothness/jquery-ui.css" />

 

<script type="text/javascript">




    jQuery(function ($) {
        $("#<%= txtTelephone.ClientID %>").mask("(999) 999-9999? x99999");
        $("#<%= txtWorkPhone.ClientID %>").mask("(999) 999-9999? x99999");
        $("#<%= txtCellPhone.ClientID %>").mask("(999) 999-9999");
        HyperLinkPhotoID
        $("#<%= txtZip.ClientID %>").mask("99999?-9999");

        $("#<%= txtFirstName.ClientID %>").Watermark("First Name");
        $("#<%= txtMiddleName.ClientID %>").Watermark("MI");
        $("#<%= txtLastName.ClientID %>").Watermark("Last Name");

    });


    $(function () {
        $("#txtStartDate").datepicker({
            numberOfMonths: 1,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });
        $("#txtEndDate").datepicker({
            numberOfMonths: 2,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });
    });

    function UseData() {
        $.Watermark.HideAll();   //Do Stuff   $.Watermark.ShowAll();
    }

    // BIND DNN Tabs
    jQuery(function ($) { $('#tabs-client').dnnTabs(); });

</script>

<script type="text/javascript">
    function SelectAll(id) {
        document.getElementById(id).focus();
        document.getElementById(id).select();
    }
</script>

<asp:Label ID="lblDebug" runat="server" Visible="false" />
<asp:HiddenField ID="hidUserId" runat="server" />
<div class="pull-right">
    <asp:HyperLink ID="HyperLinkMakeIDCard" runat="server" 
                NavigateUrl="" Text="Make ID Card" Target="_blank" CssClass="dnnSecondaryAction" ></asp:HyperLink>&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLinkPhotoID" runat="server" Visible="false" CssClass="dnnSecondaryAction">Manage PhotoID</asp:HyperLink>&nbsp;&nbsp;
		<asp:HyperLink ID="HyperLinkAddCheckInOut" runat="server" 
                NavigateUrl="" Text="Add a CheckInOut" Target="_blank" CssClass="dnnSecondaryAction" ></asp:HyperLink>&nbsp;&nbsp;
<asp:Image ID="ImageIDClient" runat="server" Height="70" CssClass="hover-zoom" /> 
</div>
<h2><asp:Label ID="LabelName" runat="server" Text=""></asp:Label></h2>

 

<div style=" text-align:center; padding-bottom:6px;">
    <asp:Label ID="lblStatus" runat="server" Text="" CssClass="NormalRed"></asp:Label><br /></div>

  <div class="dnnForm" id="tabs-client">
        <ul class="dnnAdminTabNav">
            <li><a href="#UserRecord">User Information</a></li>

            <li><a href="#History">Check In-Out History</a></li>
        </ul>



    <div id="UserRecord" class="dnnClear">
         <div style="float:right;">
            <asp:LinkButton ID="cmdUpdate" resourcekey="cmdUpdateRecord" ValidationGroup="UserForm"
                OnClientClick="UseData();" runat="server" OnClick="cmdUpdate_Click"
                CssClass="dnnPrimaryAction"></asp:LinkButton><br />

                <br />
            <asp:LinkButton ID="cmdSendCredentials" Text="Send Password Reset" runat="server" CssClass="dnnSecondaryAction" Visible="false" 
                OnClick="cmdSendCredentials_Click"></asp:LinkButton>
            <asp:Label ID="lblSendCredentials" runat="server" Text="" CssClass="NormalBold NormalRed" />
        </div>
        <div class="dnnForm" id="form-edit-user">
            <fieldset>


                <div class="dnnFormItem">
                    <dnn:Label ID="lblFirstName" runat="server" ControlName="txtFirstName" Suffix=":" />
                    <asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="UserForm" CssClass="dnnFormRequired"
                        Width="19%" /><asp:RequiredFieldValidator runat="server" ID="reqFirstName" CssClass="dnnFormMessage dnnFormError"
                        resourcekey="reqFirstName" ControlToValidate="txtFirstName" ErrorMessage="First Name Required!" Display="Dynamic"
                        ValidationGroup="UserForm" />
                    <asp:TextBox ID="txtMiddleName" runat="server" Width="4%" />
                    <asp:TextBox ID="txtLastName" runat="server" ValidationGroup="UserForm" CssClass="dnnFormRequired"
                        Width="19%" />
                    
                    <asp:RequiredFieldValidator runat="server" ID="reqLastName" resourcekey="reqLastName"
                        CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtLastName" ErrorMessage="Last Name Required!" Display="Dynamic"
                        ValidationGroup="UserForm" />
                </div>


 
                <div class="dnnFormItem">
                    <dnn:Label ID="lblEmail" runat="server" ControlName="txtEmail" Suffix=":"></dnn:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblStreet" runat="server" ControlName="txtStreet" Suffix=":"></dnn:Label>
                    <asp:TextBox ID="txtStreet" runat="server"></asp:TextBox>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblStreet2" runat="server" ControlName="txtStreet2" Suffix=":"></dnn:Label>
                    <asp:TextBox ID="txtStreet2" runat="server"></asp:TextBox>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblCityStateZip" runat="server" ControlName="txtCity" Suffix=":">
                    </dnn:Label>
                    <asp:TextBox ID="txtCity" runat="server" Width="19%"></asp:TextBox>&nbsp;<asp:DropDownList
                        ID="ddlState" runat="server" Width="15%">
                    </asp:DropDownList>
                    &nbsp;<asp:TextBox ID="txtZip" runat="server" Width="8%"></asp:TextBox>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblTelephone" runat="server" ControlName="txtTelephone" Suffix=":">
                    </dnn:Label>
                    <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblWorkPhone" runat="server" ControlName="txtWorkPhone" Suffix=":" />
                    <asp:TextBox ID="txtWorkPhone" runat="server" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblCellPhone" runat="server" ControlName="txtCellPhone" Suffix=":">
                    </dnn:Label>
                    <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                </div>
                
            </fieldset>
        </div>

</div>


      <div id="History" class="dnnClear">
          


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
         

        <asp:TemplateField HeaderText="Photo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" Visible="false">    
            <ItemTemplate><asp:Image ID="PhotoID" CssClass="hover-zoom" runat="server" Height="40px" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("IDPhoto"))%>'></asp:Image>       
            </ItemTemplate>
        </asp:TemplateField>	

    <asp:BoundField HeaderText="TimeTrackerID" Visible="false" DataField="TimeTrackerID" SortExpression="TimeTrackerID" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
      <asp:BoundField HeaderText="WorkDate" DataField="WorkDate" SortExpression="WorkDate"  ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
      
      
        <asp:BoundField HeaderText="Name" DataField="DisplayName" SortExpression="DisplayName" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                <asp:BoundField HeaderText="Location" DataField="Location" Visible="true" SortExpression="Location" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
		<asp:BoundField HeaderText="StartTime" DataField="StartTime" SortExpression="StartTime" DataFormatString="{0:hh:mm tt}" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="EndTime" DataField="EndTime" SortExpression="EndTime" DataFormatString="{0:hh:mm tt}" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
		<asp:BoundField HeaderText="Hours" DataField="TotalTime" SortExpression="TotalTime" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Email" DataField="Email" Visible="false" SortExpression="Email" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
        


    </Columns>
</asp:GridView>	


          </div>


      </div>

<div style="float:right;">

    <asp:Button ID="ButtonReturnToList" runat="server" Text="Return to List" OnClick="ButtonReturnToList_Click" CssClass="dnnPrimaryAction" />

</div>