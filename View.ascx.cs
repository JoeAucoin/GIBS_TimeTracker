/*
' Copyright (c) 2023  GIBS.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Security;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using GIBS.Modules.GIBS_TimeTracker.Components;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using System;
using System.Text;
using System.Web.UI.WebControls;
using Topaz;



namespace GIBS.Modules.GIBS_TimeTracker
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from GIBS_TimeTrackerModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    //  public partial class View : GIBS_TimeTrackerModuleBase, IActionable
    public partial class View : PortalModuleBase, IActionable

    {


        static string _ReportsRole = "";
        static string _ManagerRole = "";
        static string _Location;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

         

            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            //    Page.ClientScript.RegisterClientScriptInclude("MyDateJS", "https://storage.googleapis.com/google-code-archive-downloads/v2/code.google.com/datejs/date.js");
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "SigWeb", (this.TemplateSourceDirectory + "/JavaScript/SigWebTablet.js"));
        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtUserId.Focus();
                if (!IsPostBack)
                {
                    LoadSettings();

                    if (Request.QueryString["ttUserId"] != null)
                    {
                        txtUserId.Text = Request.QueryString["ttUserId"];
                        BtnLogin_Click(null,null);
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public void LoadSettings()
        {
            try
            {
                if (Settings.Contains("location"))
                {
                    _Location = (Settings["location"].ToString());
                }
                else
                {
                    _Location = "";
                }

                if (Settings.Contains("reportsRole"))
                {
                    _ReportsRole = (Settings["reportsRole"].ToString());
                    
                    if(UserId > 0)
                    {
                        DotNetNuke.Entities.Users.UserInfo USERINFO = DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, this.UserId);
                        if (!USERINFO.IsInRole(_ReportsRole.ToString()))
                        {
                            ButtonCheckInOutReport.Visible = false;
                            ButtonListUsers.Visible = false;
                        }
                        else
                        {
                            ButtonCheckInOutReport.Visible = true;
                            ButtonListUsers.Visible = true;
                        }
                    }



                }
                else
                {
                    _ReportsRole = "";
                }

                if (Settings.Contains("managerRole"))
                {
                    _ManagerRole = (Settings["managerRole"].ToString());

                    if (UserId > 0)
                    {
                        DotNetNuke.Entities.Users.UserInfo USERINFO = DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, this.UserId);

                        if (!USERINFO.IsInRole(_ManagerRole.ToString()))
                        {
                            ButtonCheckInOutReport.Visible = false;
                            ButtonListUsers.Visible = false;
                        }
                        else
                        {
                            ButtonCheckInOutReport.Visible = true;
                            ButtonListUsers.Visible = true;
                        }
                    }

                }
                else
                {
                    _ManagerRole = "";
                }




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {

            
            if (txtUserId.Text.ToString() != "")
            {

                TimeTrackerController controller = new TimeTrackerController();


 //load the USER first to make sure account exists
               
                TimeTrackerInfo userRecord = controller.GetPhotoByUserID(Int32.Parse(txtUserId.Text.ToString()));

                if (userRecord != null)
                {

                    LabelDebug.Visible = false;

             //       LabelMessage.Text = userRecord.FirstName + ' ' + userRecord.LastName + " - " + WhatDidWeDo.ToString() + " - " + DateTime.Now.ToShortTimeString();



                    if (userRecord.IDPhoto != null)
                    {
                        ImgInitials.Visible = true;
                        byte[] imagem = (byte[])(userRecord.IDPhoto);
                        string PROFILE_PICUser = Convert.ToBase64String(imagem);

                        ImgInitials.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PICUser);
                        ImgInitials.AlternateText = userRecord.FirstName + ' ' + userRecord.LastName;
                        
                    }
                    else
                    {
                        ImgInitials.Visible = false;
                       
                    }



                    // InsertItemPosition TTRECORD
                    TimeTrackerInfo item = new TimeTrackerInfo();

                    //  byte[] imageBytes = Convert.FromBase64String(imageData.Replace("data:image/png;base64,", String.Empty));
                    item.TTUserID = Int32.Parse(txtUserId.Text.ToString());
                    item.WorkDate = DateTime.Now;
                    item.StartTime = DateTime.Now;
                    item.EndTime = DateTime.Now;
                    item.UserID = this.UserId;
                    item.Location = _Location.ToString();

                    //  controller.CheckInOut(item);

                    string MyReturnStatus = "";

                    TimeTrackerInfo itemresult = controller.CheckInOut(item);

                    MyReturnStatus = itemresult.CheckInOut.ToString();
                //    LabelDebug.Text = "MyReturnStatus: " + MyReturnStatus.ToString() + " - " + DateTime.Now.ToShortTimeString();
                    //   TimeTrackerInfo returnStatus = controller.CheckInOut(item);

                    //     FBClientsInfo item = controller.FBClients_GetByID(this.PortalId, clientID);




                    string WhatDidWeDo = "";

                    switch (MyReturnStatus.ToString())
                    {
                        case "1":
                            WhatDidWeDo = "CHECKED IN";
                            break;
                        case "2":
                            WhatDidWeDo = "CHECKED OUT";
                            break;
                        case "3":
                            WhatDidWeDo = "ERROR";
                            break;

                    }

                    LabelMessage.Text = userRecord.FirstName + ' ' + userRecord.LastName + " - " + WhatDidWeDo.ToString() + " - " + DateTime.Now.ToShortTimeString();

                }
                else
                {
                    LabelDebug.Visible = true;
                    LabelDebug.Text = "That user does not exist in our system!";
                    LabelMessage.Text = "That user does not exist in our system!";

                }


               

               
                


            }
            else
            {
                ImgInitials.Visible = false;
            }
            
            txtUserId.Text = "";
            txtUserId.Focus();



        }

        protected void ButtonListUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("ListMembers"));
        }

        protected void ButtonCheckInOutReport_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("CheckInOutReport"));
            
        }
    }
}