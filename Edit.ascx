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


    //$(function () {
    //    $("#txtDonationDate").datepicker({
    //        numberOfMonths: 2,
    //        showButtonPanel: false,
    //        showCurrentAtPos: 1
    //    });

    //});

    //$(function () {
    //    $("#txtPledgeDate").datepicker({
    //    dateFormat: "yy-mm-dd",
    //        numberOfMonths: 1,
    //        showButtonPanel: false,
    //        showCurrentAtPos: 0
    //    });
    //    $("#txtPledgeDateEnd").datepicker({
    //        dateFormat: "yy-mm-dd",
    //        numberOfMonths: 0,
    //        showButtonPanel: false,
    //        showCurrentAtPos: 0
    //    });

    //});

    function UseData() {
        $.Watermark.HideAll();   //Do Stuff   $.Watermark.ShowAll();
    }

</script>

<script type="text/javascript">
    function SelectAll(id) {
        document.getElementById(id).focus();
        document.getElementById(id).select();
    }
</script>

<asp:Label ID="lblDebug" runat="server" Visible="false" />
<asp:HiddenField ID="hidUserId" runat="server" />

<h2><asp:Label ID="LabelName" runat="server" Text=""></asp:Label></h2>

 <div> <asp:HyperLink ID="HyperLinkPhotoID" runat="server" Visible="false">Manage PhotoID</asp:HyperLink></div>

<div style=" text-align:center; padding-bottom:6px;">
    <asp:Label ID="lblStatus" runat="server" Text="" CssClass="NormalRed"></asp:Label><br /></div>


    <div id="UserRecord" class="dnnClear">
         <div style="float:right;">
            <asp:LinkButton ID="cmdUpdate" resourcekey="cmdUpdateRecord" ValidationGroup="UserForm"
                OnClientClick="UseData();" runat="server" OnClick="cmdUpdate_Click"
                CssClass="dnnPrimaryAction"></asp:LinkButton><br />

                <br />
            <asp:LinkButton ID="cmdSendCredentials" Text="Send Password Reset Link" runat="server" CssClass="dnnSecondaryAction" 
                OnClick="cmdSendCredentials_Click"></asp:LinkButton>
            <asp:Label ID="lblSendCredentials" runat="server" Text="" CssClass="NormalBold NormalRed" />
        </div>
        <div class="dnnForm" id="form-edit-donor">
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

<div style="float:right;">

    <asp:Button ID="ButtonReturnToList" runat="server" Text="Return to List" OnClick="ButtonReturnToList_Click" CssClass="dnnPrimaryAction" />

</div>