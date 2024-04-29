<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCheckInOut.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.EditCheckInOut" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css" />	

<script type="text/javascript">

    //$(document).ready(function () {
    //    $("#txtWorkEndDate").on("input", function () {
    //        // Print entered value in a div box
    //        CalculateHours();
    //    });
    //});

    $(document).ready(function () {
        CalculateHours();

        $("#txtWorkEndDate").on("input", function () {
            // Print entered value in a div box
            CalculateHours();
        });

    });


   

    $(function () {
        $("#txtWorkEndDate1").datepicker({
            numberOfMonths: 1,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });

        $(".timepicker1").timepicker({
            timeFormat: "h:mm p",
            interval: 30,
            minTime: "06",
            maxTime: "23:55pm",
            defaultTime: "06",
            startTime: "01:00",
            dynamic: true,
            dropdown: true,
            scrollbar: false
        });
       
    });

    function CalculateHours() {

        var startTime = document.getElementById('<%= txtStartTime.ClientID %>').value;
        var endTime = document.getElementById('<%= txtWorkEndDate.ClientID %>').value;

        const d = new Date(endTime);
        if (!d.valueOf()) {
           
            document.getElementById('<%= txtHoursWorked.ClientID %>').value = "";
        }
        else {
            // act on your validated date object
            var diff = Math.abs(new Date(endTime) - new Date(startTime));
            var seconds = Math.floor(diff / 1000); //ignore any left over units smaller than a second
            var minutes = Math.floor(seconds / 60);
            seconds = seconds % 60;
            var hours = Math.floor(minutes / 60);
            minutes = minutes % 60;

            document.getElementById('<%= txtHoursWorked.ClientID %>').value = hours.toString().padStart(2, '0') + ":" + minutes.toString().padStart(2, '0');
        }

    }

</script>

<asp:Label ID="LabelMessage" runat="server" CssClass="dnnFormMessage dnnFormSuccess" Text="" Visible="false"></asp:Label>

<p style="text-align:center;">
 <b><asp:Label ID="LabelClientInfo" runat="server" Text="LabelClientInfo" CssClass="VolunteerName" /></b>
<br />

<asp:Image ID="ImageIDClient" runat="server" Height="100" CssClass="hover-zoom" />
</p>
<asp:HiddenField ID="HiddenFieldTimeTrackerID" runat="server" />
<asp:HiddenField ID="HiddenFieldTTUserID" runat="server" />
<div style="float:right;">
<asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" CssClass="dnnPrimaryAction" />
    </div>
<div class="dnnForm" id="form-edit-donor">
            <fieldset>
               
                <div class="dnnFormItem">
                    <dnn:Label ID="lblStartTime" runat="server" ControlName="txtStartTime" Suffix=":"></dnn:Label>
                    <asp:TextBox ID="txtStartTime" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                </div>

                <div class="dnnFormItem">
                    <dnn:Label ID="lblWorkEndDate" runat="server" ControlName="txtWorkEndDate" Suffix=":">
                    </dnn:Label>
                    <asp:TextBox ID="txtWorkEndDate" runat="server" ClientIDMode="Static" TextMode="DateTimeLocal" ></asp:TextBox>
                </div>
               
                <div class="dnnFormItem">
                    <dnn:Label ID="lblHoursWorked" runat="server" ControlName="txtHoursWorked" Suffix=":">
                    </dnn:Label>
                    <asp:TextBox ID="txtHoursWorked" runat="server" ClientIDMode="Static" Width="190" ></asp:TextBox>
                </div>


                <div class="dnnFormItem">
                    <dnn:Label ID="lblCreatedOnDate" runat="server" ControlName="txtCreatedOnDate" Suffix=":">
                    </dnn:Label><asp:Literal ID="LiteralCreatedOnDate" runat="server"></asp:Literal>
                    
                </div>
               
            </fieldset>
        </div>

<div style="float:right;">

<asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" Visible="false" />

<asp:Button ID="ButtonReturnToList" runat="server" Text="Return To User List" OnClick="ButtonReturnToList_Click"/>
    <asp:Button ID="ButtonReturnToUserRecord" runat="server" Text="Return To User Record" OnClick="ButtonReturnToUserRecord_Click" />





</div>