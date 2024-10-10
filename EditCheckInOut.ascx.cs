using DotNetNuke.Abstractions;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Services.Exceptions;
using GIBS.Modules.GIBS_TimeTracker.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DotNetNuke.Abstractions.Logging;


namespace GIBS.Modules.GIBS_TimeTracker
{
    public partial class EditCheckInOut : PortalModuleBase
    {

        static int _ttid;
        static int _ttUserID;


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "TimePicker", (this.TemplateSourceDirectory + "/JavaScript/jquery.timepicker.min.js"));
           
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {


                    if (Request.QueryString["ttid"] != null)
                    {
                        _ttid = Int32.Parse(Request.QueryString["ttid"]);
                     //   hidUserId.Value = VolunteerUserId.ToString();
                        LoadRecord(_ttid);

                       // SetPhotoIDLink();


                    }
                    else
                    {
                        this.ModuleConfiguration.ModuleControl.ControlTitle = "Add New Record";

                    }

                    if (Request.QueryString["ttUserId"] != null)
                    {
                        _ttUserID = Int32.Parse(Request.QueryString["ttUserId"]);
                        HiddenFieldTimeTrackerID.Value = "0";
                        HiddenFieldTTUserID.Value = _ttUserID.ToString();

                        var thisuser = DotNetNuke.Entities.Users.UserController.GetUserById(this.PortalId, _ttUserID);
                        LabelClientInfo.Text = thisuser.FirstName.ToString() + " " + thisuser.LastName.ToString();

                    }

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void LoadRecord(int RecordID)
        {
            try
            {

                //  DotNetNuke.Entities.Users.UserInfo VolunteerUser = DotNetNuke.Entities.Users.UserController.GetUserById(DonorPortal, RecordID);

                //load the item
                TimeTrackerController controller = new TimeTrackerController();
                TimeTrackerInfo item = controller.GetCheckInOutRecord(RecordID);

                if (item != null)
                {

                    LabelClientInfo.Text = item.FirstName + ' ' + item.LastName;
                    HiddenFieldTimeTrackerID.Value = item.TimeTrackerID.ToString();
                    HiddenFieldTTUserID.Value = item.TTUserID.ToString();

                    _ttUserID = Int32.Parse(item.UserID.ToString());


            //        string newURL = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "Edit", "mid=" + ModuleId.ToString(), "UserId=" + _ttUserID.ToString());
                   

                    LiteralCreatedOnDate.Text = item.CreatedOnDate.ToShortDateString();
                    
                   
                    
                    txtStartTime.Text = item.StartTime.ToString("yyyy-MM-ddTHH:mm");

                    if(item.WorkEndDate.ToShortDateString() != "1/1/0001")
                    {
                        txtWorkEndDate.Text = item.EndTime.ToString("yyyy-MM-ddTHH:mm");
                    }
                    
                   // txtWorkEndDate.Text= item.WorkEndDate.ToShortDateString();

                

                    if (item.IDPhoto != null)
                    {
                        ImageIDClient.Visible = true;
                        byte[] imagem = (byte[])(item.IDPhoto);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);

                        ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        ImageIDClient.AlternateText = item.FirstName + ' ' + item.LastName;
                     //   HyperLinkMakeID.Visible = true;
                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                       // HyperLinkMakeID.Visible = false;
                    }

                }
                else
                {
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(), true);
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            //CheckInOut_Update
            try
            {

                TimeTrackerController controller = new TimeTrackerController();
                TimeTrackerInfo item = new TimeTrackerInfo();

                
                item.StartTime = DateTime.Parse(txtStartTime.Text.ToString());
                item.EndTime = DateTime.Parse(txtWorkEndDate.Text.ToString());
                item.UserID = this.UserId;
                string hiddenLocation = "";
                if (Settings.Contains("location"))
                {

                    hiddenLocation = (Settings["location"].ToString());
                }
                else
                {
                    hiddenLocation = "-";
                }

                item.Location = hiddenLocation.ToString();
                item.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();


                if (Int32.Parse(HiddenFieldTimeTrackerID.Value.ToString()) > 0)
                {
                    item.TimeTrackerID = Int32.Parse(HiddenFieldTimeTrackerID.Value.ToString());
                    controller.CheckInOut_Update(item);
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "Record Updated!";
                }
                else 
                {
                    // info.WorkDate, info.UserID, info.TTUserID, info.StartTime, info.EndTime, info.Location, info.IPAddress)
                   
                    
                    item.TTUserID = Int32.Parse(HiddenFieldTTUserID.Value.ToString());
                    item.WorkDate = DateTime.Parse(txtStartTime.Text.ToString());

                    //LabelMessage.Visible = true;
                    //LabelMessage.Text = "WorkDate: " + item.WorkDate.ToShortDateString() + "<br>" +
                    //     "UserID: " + item.UserID + "<br>" +
                    //      "TTUserID: " + item.TTUserID + "<br>" +
                    //       "StartTime: " + item.StartTime + "<br>" +
                    //        "EndTime: !" + item.EndTime + "<br>" +
                    //         "Location: " + item.Location + "<br>" +
                    //          "IPAddress: " + item.IPAddress + "<br>";


                    controller.CheckInOutInsert(item);
                    LiteralCreatedOnDate.Text = DateTime.Now.Date.ToShortDateString();
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "Record Inserted!";
                }
                
               
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {

        }
        


        protected void ButtonReturnToList_Click(object sender, EventArgs e)
        {
            string newURL = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "ListMembers", "mid=" + ModuleId.ToString());
            Response.Redirect(newURL);

        }

        protected void ButtonReturnToUserRecord_Click(object sender, EventArgs e)
        {
            //ButtonReturnToUserRecord_Click
            string newURL1 = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "Edit", "mid=" + ModuleId.ToString(), "ttUserId=" + _ttUserID.ToString());
            Response.Redirect(newURL1);


        }
    }
}