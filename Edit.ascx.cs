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


using System;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using GIBS.Modules.GIBS_TimeTracker.Components;
using DotNetNuke.Common.Lists;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace GIBS.Modules.GIBS_TimeTracker
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditGIBS_TimeTracker class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from GIBS_TimeTrackerModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit :  GIBS_TimeTrackerModuleBase
    {

        private GridViewHelper helper;
        // To show custom operations...
        private List<int> mQuantities = new List<int>();

        protected string _RoleName = "Registered Users";
        
        int VolunteerUserId = Null.NullInteger;
        public int UserPortal;
        public bool _EmailNewUserCredentials = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            Page.ClientScript.RegisterClientScriptInclude("MyDateJS", "https://storage.googleapis.com/google-code-archive-downloads/v2/code.google.com/datejs/date.js");
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput.js"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Watermark", (this.TemplateSourceDirectory + "/JavaScript/jquery.watermarkinput.js"));

            //     Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Style", ("https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css"));

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
              

                UserPortal = this.PortalId;
                LoadSettings();


   

                if (!IsPostBack)
                {
                    txtStartDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                    txtEndDate.Text = DateTime.Now.ToShortDateString();


                    if (Request.QueryString["TabFocus"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("dnnTabs-tabs-client");
                        myCookie.HttpOnly = false;
                        switch (Request.QueryString["TabFocus"])
                        {
                            case "UserRecord":
                                myCookie.Value = "";
                                break;

                            case "History":
                                myCookie.Value = "3";
                                break;

                            default:
                                myCookie.Value = "";
                                break;
                        }
                        Response.Cookies.Add(myCookie);
                    }


                    GetStates();
                   // FillDropDown();



                    if (Request.QueryString["UserId"] != null)
                    {
                        VolunteerUserId = Int32.Parse(Request.QueryString["UserId"]);
                        hidUserId.Value = VolunteerUserId.ToString();
                        LoadRecord(VolunteerUserId);

                        SetPhotoIDLink();

                        // GET USERS CHECK-IN HISTORY
                        GroupIt();
                        Fill_Report();

                        cmdSendCredentials.Visible = true;
                    }
                    else
                    {
                        this.ModuleConfiguration.ModuleControl.ControlTitle = "Add New Record";
                        
                    }


                    
                    if (Request.QueryString["Status"] != null)
                    {
                        if (Request.QueryString["Status"] == "Success")
                        {
                            //AddNewDonationRecord();
                            // SET FOCUS TO ADD DONATION TAB
                            lblStatus.Text = "Record Successfully Inserted";
                        }
                    }

               //     cmdDeleteDonation.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");




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

                DotNetNuke.Entities.Users.UserInfo VolunteerUser = DotNetNuke.Entities.Users.UserController.GetUserById(UserPortal, RecordID);

                LabelName.Text = VolunteerUser.FirstName + " " + VolunteerUser.LastName;

                //      this.ModuleConfiguration.ModuleControl.ControlTitle = DonationUser.Profile.GetPropertyValue("Company") + " - " + DonationUser.DisplayName;

            
               

                txtFirstName.Text = VolunteerUser.FirstName;
                txtLastName.Text = VolunteerUser.LastName;
                txtMiddleName.Text = VolunteerUser.Profile.GetPropertyValue("MiddleName");
              //  txtCompany.Text = VolunteerUser.Profile.GetPropertyValue("Company");



                txtEmail.Text = VolunteerUser.Email;

                //ListItem litown = ddlPrefix.Items.FindByValue(VolunteerUser.Profile.GetPropertyValue("Prefix"));
                //if (litown != null)
                //{
                //    // value found
                //    ddlPrefix.SelectedValue = VolunteerUser.Profile.GetPropertyValue("Prefix");
                //}
                //else
                //{
                //    //Value not found
                //    //   ddlTown.SelectedValue = item.ClientTown;
                //}


                txtStreet.Text = VolunteerUser.Profile.Street;
                txtStreet2.Text = VolunteerUser.Profile.GetPropertyValue("Street2");
                txtCity.Text = VolunteerUser.Profile.City;
                txtWorkPhone.Text = VolunteerUser.Profile.GetPropertyValue("WorkPhone");
                txtTelephone.Text = VolunteerUser.Profile.Telephone;
                txtCellPhone.Text = VolunteerUser.Profile.Cell;
                
                txtZip.Text = VolunteerUser.Profile.PostalCode;

                //   ddlState.SelectedValue = DonationUser.Profile.Region;
                //     lblDebug.Text = DonationUser.Profile.Region;

                ListItem liregion = ddlState.Items.FindByText(VolunteerUser.Profile.Region);      //.FindByValue(DonationUser.Profile.Region);
                if (liregion != null)
                {
                    // value found
                    ddlState.SelectedValue = ddlState.Items.FindByText(VolunteerUser.Profile.Region).Value;    //DonationUser.Profile.Region;
                }
                else
                {
                    //Value not found
                    //   ddlTown.SelectedValue = item.ClientTown;
                    lblStatus.Text += "<br />STATE NOT FOUND";
                }





                string _PrimaryAddress = VolunteerUser.Profile.GetPropertyValue("Prefix") + " "
                    + VolunteerUser.FirstName + " "
                    + VolunteerUser.Profile.GetPropertyValue("MiddleName") + " "
                    + VolunteerUser.LastName + " "
                    + VolunteerUser.Profile.GetPropertyValue("Suffix") + Environment.NewLine
                    + VolunteerUser.Profile.GetPropertyValue("AdditionalName") + Environment.NewLine
                  //  + VolunteerUser.Profile.GetPropertyValue("Company") + Environment.NewLine
                    + VolunteerUser.Profile.Street + Environment.NewLine
                    + VolunteerUser.Profile.GetPropertyValue("Street2") + Environment.NewLine
                    + VolunteerUser.Profile.City + ", "
                    + VolunteerUser.Profile.Region + " "
                    + VolunteerUser.Profile.PostalCode;
                //   REMOVE EMPTY LINES
                _PrimaryAddress = Regex.Replace(_PrimaryAddress.ToString(), @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

                //  txtPrimaryAddress.Text = _PrimaryAddress.ToString().TrimStart().TrimEnd().Replace("  ", " ");
                FillClientPhoto(RecordID);



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void FillClientPhoto(int ttUserID)
        {

            try
            {
                //load the item
                TimeTrackerController controller = new TimeTrackerController();
                TimeTrackerInfo item = controller.GetPhotoByUserID(ttUserID);

                if (item != null)
                {

                   

                    if (item.IDPhoto != null)
                    {
                        ImageIDClient.Visible = true;
                        byte[] imagem = (byte[])(item.IDPhoto);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);

                        ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        ImageIDClient.AlternateText = item.FirstName + ' ' + item.LastName;
                        
                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                      
                    }

                }
                

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void btnGetSchedule_Click(object sender, EventArgs e)
        {
            GroupIt();
            Fill_Report();
        }

        public void Fill_Report()
        {

            try
            {

                List<TimeTrackerInfo> items;
                TimeTrackerController controller = new TimeTrackerController();

                items = controller.GetCheckInReport_ForUser(Convert.ToDateTime(txtStartDate.Text.ToString()), Convert.ToDateTime(txtEndDate.Text.ToString()), Int32.Parse(hidUserId.Value.ToString()));

                gv_Report.DataSource = items;
                gv_Report.DataBind();





            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int itemID = (int)gv_Report.DataKeys[e.NewEditIndex].Value;
            Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditCheckInOut", "mid=" + ModuleId.ToString() + "&ItemId=" + itemID));


        }

        protected void gv_Report_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hyperLink = e.Row.FindControl("HyperLink1") as HyperLink;
                if (hyperLink != null)
                {
                    string newURL = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditCheckInOut", "mid=" + ModuleId.ToString(), "ttid=" + DataBinder.Eval(e.Row.DataItem, "TimeTrackerID"));
                    hyperLink.NavigateUrl = newURL.ToString();
                }


                if (e.Row.Cells[6].Text.Contains("12:00 AM"))
                {
                    e.Row.Cells[6].Text = "Not Checked Out!<br / >Report to Manager";
                    e.Row.Cells[6].BackColor = System.Drawing.Color.LightPink;

                }
            }
        }



        protected void gv_Report_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        public void GroupIt()
        {

            try
            {

                helper = new GridViewHelper(this.gv_Report);
                helper.RegisterGroup("WorkDate", true, true);

                helper.RegisterSummary("TotalTime", SummaryOperation.Sum, "WorkDate");
                helper.RegisterSummary("DisplayName", SummaryOperation.Count, "WorkDate");



                helper.RegisterSummary("TotalTime", SummaryOperation.Sum);
                helper.RegisterSummary("DisplayName", SummaryOperation.Count);


                // helper.RegisterGroup("ClientZipCode", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                helper.GroupSummary += new GroupEvent(helper_Bug);
                helper.GeneralSummary += new FooterEvent(helper_GeneralSummary);
                helper.ApplyGroupSort();



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        void helper_GeneralSummary(GridViewRow row)
        {

            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            row.Cells[0].Text = "Grand Totals: ";
            row.BackColor = Color.BlanchedAlmond;
            row.ForeColor = Color.Black;

        }

        private void helper_Bug(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == null) return;

            DateTime dt;
            dt = Convert.ToDateTime(values[0]);

            row.BackColor = Color.Lavender;
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = dt.DayOfWeek.ToString() + " - " + Convert.ToDateTime(values[0]).ToShortDateString() + " Totals&nbsp;";
        }

        private void helper_ManualSummary(GridViewRow row)
        {
            GridViewRow newRow = helper.InsertGridRow(row);
            newRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            newRow.Cells[0].Text = String.Format("Total: {0} items, ", helper.GeneralSummaries["TotalTime"].Value, helper.GeneralSummaries["FirstName"].Value);
        }



        private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "WorkDate")
            {
                DateTime dt;
                dt = Convert.ToDateTime(row.Cells[0].Text);
                row.BackColor = Color.LightGray;
                row.Cells[0].Text = "&nbsp;&nbsp;<b>" + dt.DayOfWeek.ToString() + " - " + Convert.ToDateTime(row.Cells[0].Text).ToShortDateString() + "</b>";


            }
            else if (groupName == "ShipName")
            {
                row.BackColor = Color.FromArgb(236, 236, 236);
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Cells[0].Text;
            }
        }


        public void GetStates()
        {

            try
            {
                //UserRecord
                var _states = new ListController().GetListEntryInfoItems("Region", "Country.US", this.PortalId);

                ddlState.DataTextField = "Text";
                ddlState.DataValueField = "EntryID";
                ddlState.DataSource = _states;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("--", ""));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //INSERT NEW USER BUTTON CLICK
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["UserId"] != null)
                {

                    VolunteerUserId = Int32.Parse(Request.QueryString["UserId"]);
                    UpdateRecord(VolunteerUserId);
                }
                else
                {
                    CreateNewUser(_RoleName);
                }



                //  Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void UpdateRecord(int RecordID)
        {
            try
            {
                //  DotNetNuke.Entities.Users.UserInfo uUser = DotNetNuke.Entities.Users.UserController.GetUserById(PortalSettings.PortalId, RecordID);

                UserController objUserController = new UserController();
                UserInfo uUser = objUserController.GetUser(UserPortal, RecordID);

                uUser.FirstName = txtFirstName.Text.Trim().ToString();
                uUser.LastName = txtLastName.Text.Trim().ToString();
                uUser.Email = txtEmail.Text.ToString();

                uUser.Profile.Street = txtStreet.Text.Trim().ToString();
                uUser.Profile.City = txtCity.Text.Trim().ToString();

          //      uUser.Profile.SetProfileProperty("Prefix", ddlPrefix.SelectedValue.ToString());
                uUser.Profile.SetProfileProperty("MiddleName", txtMiddleName.Text.Trim().ToString());

            

               

                uUser.Profile.SetProfileProperty("Street2", txtStreet2.Text.Trim().ToString());
                uUser.Profile.Telephone = txtTelephone.Text.ToString();
                uUser.Profile.SetProfileProperty("WorkPhone", txtWorkPhone.Text.ToString());
                uUser.Profile.Cell = txtCellPhone.Text.ToString();
                
                uUser.Profile.PostalCode = txtZip.Text.ToString();
                uUser.Profile.Region = ddlState.SelectedValue.ToString();

               

                UserController.UpdateUser(UserPortal, uUser);

                // RELOAD RECORD
                LoadRecord(RecordID);

                lblStatus.Text = "Record Successully Updated";


             




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        protected void cmdSendCredentials_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtEmail.Text.ToString().Length > 7)
                {

                    UserInfo u = new UserInfo();

                    u = UserController.GetUserById(UserPortal, Int32.Parse(Request.QueryString["UserId"]));
                    //     u.Membership.v
                    DotNetNuke.Entities.Users.UserController.ResetPasswordToken(u); 

                  //  u.Membership.Password = UserController.GetPassword(ref u, "");
                    //   System.Web.Security.Membership.ValidateUser(u.Username, u.Profile.p);
                    DotNetNuke.Services.Mail.Mail.SendMail(u, DotNetNuke.Services.Mail.MessageType.PasswordReminder, PortalSettings);
                    cmdSendCredentials.Visible = false;
                    lblSendCredentials.Text = "Password reset link successfully sent!";

                }
                else
                {
                    lblSendCredentials.Text = "A valid e-mail address is required to send a password!";
                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void CreateNewUser(string AddUserRole)
        {

            try
            {

                string vPassword = GenerateRandomString(7);

                UserInfo oUser = new UserInfo();

                oUser.PortalID = UserPortal;
                oUser.IsSuperUser = false;
                oUser.FirstName = txtFirstName.Text.Trim().ToString();
                oUser.LastName = txtLastName.Text.Trim().ToString();
                oUser.Email = txtEmail.Text.Trim().ToString();
                //
                string NewUserName = "";
                if (txtEmail.Text.ToString().Length >= 7)
                {
                    NewUserName = txtEmail.Text.ToString();
                }
 
                else
                {
                    NewUserName = txtLastName.Text.ToString().Trim().Replace(" ", "").Replace("'", "").Replace(",", "") + txtFirstName.Text.ToString().Trim().Replace(" ", "").Replace("'", "").Replace(",", "");
                }
                if (NewUserName.ToString().Length < 7)
                {
                    NewUserName += "!!!";
                }

                oUser.Username = NewUserName.ToString();

                oUser.DisplayName = txtFirstName.Text.Trim().ToString() + " " + txtLastName.Text.Trim().ToString();

                //Fill MINIMUM Profile Items (KEY PIECE)
                oUser.Profile.PreferredLocale = PortalSettings.DefaultLanguage;
                oUser.Profile.PreferredTimeZone = PortalSettings.TimeZone;
                oUser.Profile.FirstName = oUser.FirstName;
                oUser.Profile.LastName = oUser.LastName;


                oUser.Profile.Street = txtStreet.Text.ToString();
                oUser.Profile.City = txtCity.Text.ToString();
            //    oUser.Profile.SetProfileProperty("Suffix", ddlSuffix.SelectedValue.ToString());
           //     oUser.Profile.SetProfileProperty("Prefix", ddlPrefix.SelectedValue.ToString());
                oUser.Profile.SetProfileProperty("Street2", txtStreet2.Text.ToString());
                oUser.Profile.SetProfileProperty("MiddleName", txtMiddleName.Text.Trim().ToString());
              

                
                //     oUser.Profile.SetProfileProperty("Biography", txtNotes.Text.ToString());
                oUser.Profile.Country = "221";
                oUser.Profile.Telephone = txtTelephone.Text.ToString();
                oUser.Profile.Cell = txtCellPhone.Text.ToString();
               
                oUser.Profile.PostalCode = txtZip.Text.ToString();
                oUser.Profile.Region = ddlState.SelectedValue.ToString();

                //Set Membership
                UserMembership oNewMembership = new UserMembership(oUser);
                oNewMembership.Approved = true;
                oNewMembership.CreatedDate = System.DateTime.Now;

                //       oNewMembership.Email = oUser.Email;
                oNewMembership.IsOnLine = false;
                //       oNewMembership.Username = oUser.Username;
                oNewMembership.Password = vPassword.ToString();

                //Bind membership to user
                oUser.Membership = oNewMembership;

                //Add the user, ensure it was successful 
                if (DotNetNuke.Security.Membership.UserCreateStatus.Success == UserController.CreateUser(ref oUser))
                {
                    //Add Role if passed something from module settings

                    if (AddUserRole.Length > 0)
                    {
                        DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();
                        //retrieve role
                        string groupName = AddUserRole;
                        DotNetNuke.Security.Roles.RoleInfo ri = rc.GetRoleByName(this.PortalId, groupName);
                        rc.AddUserRole(UserPortal, oUser.UserID, ri.RoleID, DotNetNuke.Security.Roles.RoleStatus.Approved, false, DateTime.Today, Null.NullDate);
                    }

                    //string EmailContent = settingsData.EmailMessage + content;
                    //    DonationTrackerSettings settingsData = new DonationTrackerSettings(this.TabModuleId);

                    if (Settings.Contains("emailNewUserCredentials"))
                    {
                        if (Convert.ToBoolean(Settings["emailNewUserCredentials"].ToString()) == true)
                        {
                            string EmailContent = Settings["emailMessage"].ToString() + "<p>UserName: " + txtEmail.Text.ToString() + "<br />";
                            EmailContent += "Password: " + vPassword.ToString() + "<br />";
                            EmailContent += "Site: http://" + Request.Url.Host + "</p>";

                            EmailContent = EmailContent.ToString().Replace("[FULLNAME]", oUser.DisplayName);

                            // SEND EMAIL
                            EmailNotification(EmailContent.ToString(), txtEmail.Text.ToString());
                        }
                        else
                        {
                            // DO NOT SEND EMAIL
                        }

                    }




                    // THIS URL WILL GIVE YOU THE ADD NEW DONATION PANEL
                    string newURL = Globals.NavigateURL(this.TabId, "", "UserId=" + oUser.UserID, "ctl=Edit", "mid=" + this.ModuleId, "Status=Success");

                    // THIS URL WILL GIVE YOU A BLANK FORM TO ADD A NEW USER RECORD
                    //string newURL = Globals.NavigateURL(this.TabId, "", "ctl=Edit", "mid=" + this.ModuleId, "Status=Success");

                    cmdSendCredentials.Visible = true;

                    Response.Redirect(newURL, false);

                }
                else
                {
                    DotNetNuke.Entities.Users.UserInfo DonationUser = DotNetNuke.Entities.Users.UserController.GetUserByName(oUser.Username);
                    LoadRecord(DonationUser.UserID);
                    lblStatus.Text = "Error on Insert. Thay user already exists . . . I've loaded the record for you!";
                }






            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void EmailNotification(string content, string toEmail)
        {

            try
            {
                //  DonationTrackerSettings settingsData = new DonationTrackerSettings(this.TabModuleId);
                // BUILD E-MAIL BODY

                string EmailContent = content;
                string subject = Settings["emailSubject"].ToString();

                // LOOK FOR THE FROM EMAIL ADDRESS
                string EmailFrom = "";
                if (Settings["emailFrom"].ToString().Length > 1)
                {
                    EmailFrom = Settings["emailFrom"].ToString();
                }
                else
                {
                    EmailFrom = PortalSettings.Email.ToString();
                }
                // LOOK FOR BCC ADDRESS
                string EmailBCC = "";
                if (Settings["emailBCC"].ToString().Length > 1)
                {
                    EmailBCC = Settings["emailBCC"].ToString();
                }
                else
                {
                    EmailBCC = "";
                }
                //  SEND THE EMAIL
                DotNetNuke.Services.Mail.Mail.SendMail(EmailFrom.ToString(), toEmail, EmailBCC.ToString(), subject, EmailContent.ToString(), "", "HTML", "", "", "", "");

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }


        }


        public static string GenerateRandomString(int length)
        {
            //Removed O, o, 0, l, 1 
            string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            string allowedNumberChars = "23456789";
            char[] chars = new char[length];
            Random rd = new Random();

            bool useLetter = true;
            for (int i = 0; i < length; i++)
            {
                if (useLetter)
                {
                    chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
                    useLetter = false;
                }
                else
                {
                    chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
                    useLetter = true;
                }

            }

            return new string(chars);
        }


        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }





        public void LoadSettings()
        {
            try
            {

                if (Settings.Contains("roleName"))
                {
                    _RoleName = Settings["roleName"].ToString();
                }
                if (Settings.Contains("showSendPassword"))
                {
                    if (Convert.ToBoolean(Settings["showSendPassword"].ToString()) == true)
                    {
                        cmdSendCredentials.Visible = true;
                    }
                    else
                    {
                        cmdSendCredentials.Visible = false;
                    }

                }




                if (Settings.Contains("reportsRole"))
                {
                    string _ReportsRole = Settings["reportsRole"].ToString();
                    var roleGroup = UserInfo.IsInRole(_ReportsRole);

                    if (roleGroup == true)
                    {
                    //    GridViewDonations.Visible = true;
                    }
                    else
                    {
                    //    GridViewDonations.Visible = false;
                    }

                }






            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
        public void SetPhotoIDLink()
        {

            //HyperLinkPhotoID
            try
            {

                string myLink = DotNetNuke.Common.Globals.NavigateURL("Camera", "mid=" + this.ModuleId);
                //myLink += "?cid=" + clientId.ToString() + "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "?cid=" + hidUserId.Value.ToString();

                myLink += "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "&ContainerSrc=";
                myLink += DotNetNuke.Common.Globals.QueryStringEncode("/Portals/_default/Containers/_default/No Container");
               
                string redirectUrl = UrlUtils.PopUpUrl(myLink, this, PortalSettings, false, true);
                HyperLinkPhotoID.Visible = true;
                HyperLinkPhotoID.NavigateUrl = redirectUrl.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void ButtonReturnToList_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("ListMembers"));
        }
    }
}