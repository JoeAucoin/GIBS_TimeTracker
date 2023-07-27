<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.Settings" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>

<div class="dnnForm" id="form-settings">

    <fieldset>

<dnn:sectionhead id="sectGeneralSettings" cssclass="Head" runat="server" text="General Settings" section="GeneralSection"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="GeneralSection" runat="server">   
            
                    
                    		
	<div class="dnnFormItem">					
	<dnn:label id="lblNumPerPage" runat="server" controlname="ddlNumPerPage" suffix=":"></dnn:label>
	<asp:DropDownList ID="ddlNumPerPage" runat="server">
		<asp:ListItem Text="2" Value="2"></asp:ListItem>
		<asp:ListItem Text="5" Value="5"></asp:ListItem>
		<asp:ListItem Text="10" Value="10"></asp:ListItem>
		<asp:ListItem Text="20" Value="20"></asp:ListItem>
			<asp:ListItem Text="30" Value="30"></asp:ListItem>
		<asp:ListItem Text="40" Value="40"></asp:ListItem>
		<asp:ListItem Text="50" Value="50"></asp:ListItem>
		<asp:ListItem Text="100" Value="100"></asp:ListItem>

		</asp:DropDownList>			
				
	</div>	

	<div class="dnnFormItem">
    
        <dnn:label id="lblAddUserRole" runat="server" controlname="ddlRoles" suffix=":"></dnn:label>
        <asp:DropDownList ID="ddlRoles" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
        </asp:DropDownList>
	</div>

	<div class="dnnFormItem">
        <dnn:label id="lblEmailFrom" runat="server" controlname="txtEmailFrom" suffix=":"></dnn:label>
        <asp:TextBox ID="txtEmailFrom" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox>
	</div>

	<div class="dnnFormItem">
        <dnn:label id="lblEmailBCC" runat="server" controlname="txtEmailBCC" suffix=":"></dnn:label>
        <asp:TextBox ID="txtEmailBCC" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox>
	</div>

	<div class="dnnFormItem">
        <dnn:label id="lblEmailSubject" runat="server" controlname="txtEmailSubject" suffix=":"></dnn:label>
        <asp:TextBox ID="txtEmailSubject" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox>
	</div>

	<div class="dnnFormItem">
        <dnn:label id="lblEmailMessage" runat="server" controlname="txtEmailMessage" suffix=":"></dnn:label>
        <asp:TextBox ID="txtEmailMessage" runat="server" Columns="30" Height="120px" TextMode="MultiLine"></asp:TextBox>
	</div>

	<div class="dnnFormItem">
    
        <dnn:label id="lblReportsUserRole" runat="server" controlname="ddlReportsRoles" suffix=":"></dnn:label>
        <asp:DropDownList ID="ddlReportsRoles" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
        </asp:DropDownList>
	</div>

	<div class="dnnFormItem">
    
        <dnn:label id="lblMergeUserRole" runat="server" controlname="ddlMergeRoles" suffix=":"></dnn:label>
        <asp:DropDownList ID="ddlMergeRoles" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
        </asp:DropDownList>
	</div>	

		<div class="dnnFormItem">
<dnn:label id="lblIDCardImagePath" runat="server" ResourceKey="lblIDCardImagePath" controlname="txtIDCardImagePath" suffix=":" />
        <asp:textbox id="txtIDCardImagePath" width="300" runat="server" />
	</div>	

    <div class="dnnFormItem">					
        <dnn:label id="lblShowSendPassword" runat="server" controlname="cbxShowSendPassword" suffix=":"></dnn:label>
        <asp:CheckBox ID="cbxShowSendPassword" runat="server" />
    </div>

    <div class="dnnFormItem">					
        <dnn:label id="lblEmailNewUserCredentials" runat="server" controlname="cbxEmailNewUserCredentials" suffix=":"></dnn:label>
        <asp:CheckBox ID="cbxEmailNewUserCredentials" runat="server" />
    </div>

	<div class="dnnFormItem">					
        <dnn:label id="lblShowDonationHistory" runat="server" controlname="cbxShowDonationHistory" suffix=":"></dnn:label>
        <asp:CheckBox ID="cbxShowDonationHistory" runat="server" />
    </div>

	<div class="dnnFormItem">					
        <dnn:label id="lblEnableAddNewDonor" runat="server" controlname="cbxEnableAddNewDonor" suffix=":"></dnn:label>
        <asp:CheckBox ID="cbxEnableAddNewDonor" runat="server" />
    </div>		



 </div>
        

     			


    </fieldset>

</div>