<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MakeID.ascx.cs" Inherits="GIBS.Modules.GIBS_TimeTracker.MakeID" %>



<style>
    

.button-group, .play-area {
  border: 1px solid grey;
  padding: 1em 1%;
  margin-bottom: 1em;
  text-align: center;
}

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

.btn{

font-weight: 0;
font-size: 16px;
color: #fff;
background-color: #0066CC;
padding: 10px 30px;
border: 2px solid #0066cc;
box-shadow: none;
border-radius: 5px;
transition : 1000ms;
transform: translateY(0);

align-items: center;
cursor: pointer;
}

.btn:hover{

transition : 1000ms;
padding: 10px 35px;
transform : translateY(-0px);
background-color: #fff;
color: #0066cc;
border: solid 2px #8b8d8f;
}

</style>
<div style="padding: 20px;"> &nbsp;</div>

 <h3 style="text-align: center;"><asp:Label ID="LabelClientInfo" runat="server" Text="LabelClientInfo"></asp:Label></h3>

<asp:HiddenField ID="HiddenFieldFirstName" runat="server" />
<div class="button-group">
<asp:Button ID="ButtonPhotoID" runat="server" Text="Generate Photo ID Card" CssClass="btn"
    onclick="ButtonPhotoID_Click" />
    <br />
    <asp:Button ID="ButtonNoPhotoID" runat="server" Text="Generate Name Only ID Card" CssClass="btn" OnClick="ButtonNoPhotoID_Click" />
</div>

<div style="text-align: center; padding-top:20px; padding-bottom:20px;">
    <asp:HyperLink ID="HyperLinkPDF" Visible="false" runat="server" CssClass="btn" Target="_blank"><span class="glyphicon glyphicon-ok"></span> Print PDF ID Card</asp:HyperLink>
</div>



<div style="padding-top:20px; width: 100%; text-align: center;"> 
<asp:Image ID="ImageIDClient" runat="server" Height="100" CssClass="hover-zoom" />
    <asp:HiddenField ID="HiddenFieldClientPicture" runat="server" />
</div>


<div class="button-group">
  
 
    <asp:Button ID="ButtonReturnToClientManager" runat="server" Text="Return To User Record" OnClick="ButtonReturnToClientManager_Click" />

    
    
</div>
