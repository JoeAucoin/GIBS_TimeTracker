﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.View" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

  <script type="text/javascript">

      $(function () {
          var $id = $('#txtUserId');
          $id.keyup(function (e) {
              if ($id.val().length >= 11) {
                  $(this.form).submit();
              }
          });
      });

      $(function () {
          $("#hide-it").hide(10000);
      });        


  </script>

<style>
    
 .hover-zoom {
    -moz-transition:all 0.3s;
    -webkit-transition:all 0.3s;
     transition:all 0.3s
 }
.hover-zoom:hover {
    -moz-transform: scale(1.1);
    -webkit-transform: scale(1.1);
     transform: scale(4.6)
 }

</style>
      
<div>
<asp:Label ID="LabelDebug" runat="server" Text="LabelDebug" Visible="false" />
</div>

  <fieldset>
              <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="txtUserId" ID="lblUserId" Suffix=":" ResourceKey="lblUserId" />
            <asp:TextBox runat="server" ID="txtUserId" TextMode="Password" Width="20%" /><asp:RangeValidator runat="server" Type="Integer"  ID="UserIDRangeValidator" 
            MinimumValue="0" MaximumValue="999999" ControlToValidate="txtUserId" ErrorMessage="<span style='color:red'>Value must numeric</span>" />
        </div>    
</fieldset>




<div style="float: right;">
    <asp:Button ID="BtnLogin" runat="server" Text="Check In-Out" OnClick="BtnLogin_Click" CssClass="dnnPrimaryAction" />

</div>
<br />&nbsp;<br />
<div style="float: right; padding-top:15px;overflow: auto;">
    <asp:Button ID="ButtonCheckInOutReport" runat="server" Visible="false" Text="Login Report" CssClass="dnnSecondaryAction" OnClick="ButtonCheckInOutReport_Click" /> &nbsp; <asp:Button ID="ButtonListUsers" runat="server" Text="List Users" Visible="false" CssClass="dnnSecondaryAction" OnClick="ButtonListUsers_Click" />

</div>
  
<div id="hide-it">
<p style="text-align:center;"><b>
        <asp:Label ID="LabelMessage" runat="server" Text="" /></b><br /> <asp:Image ID="ImgInitials" runat="server" Visible="false" Height="140" CssClass="hover-zoom" /></p>

</div>



