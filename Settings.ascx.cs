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
using GIBS.Modules.GIBS_TimeTracker.Components;


using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Profile;

namespace GIBS.Modules.GIBS_TimeTracker
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from GIBS_TimeTrackerSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : GIBS_TimeTrackerModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {
                    GetRoles();

                    //  DonationTrackerSettings settingsData = new DonationTrackerSettings(this.TabModuleId);
                    if (Settings.Contains("iDCardImagePath"))
                    {
                        txtIDCardImagePath.Text = IDCardImagePath;
                    }
                    //   if (RoleName != null)
                    if (Settings.Contains("roleName"))
                    {
                        ddlRoles.SelectedValue = RoleName;
                    }
                    if (NumPerPage != null)
                    {
                        ddlNumPerPage.SelectedValue = NumPerPage;
                    }
                    if (EmailMessage != null)
                    {
                        txtEmailMessage.Text = EmailMessage;
                    }
                    if (EmailFrom != null)
                    {
                        txtEmailFrom.Text = EmailFrom;
                    }

                    if (EmailBCC != null)
                    {
                        txtEmailBCC.Text = EmailBCC;
                    }

                    if (EmailSubject != null)
                    {
                        txtEmailSubject.Text = EmailSubject;
                    }

                    if (ReportsRole != null || ReportsRole.Length > 0)
                    {
                        ddlReportsRoles.SelectedValue = ReportsRole;
                    }

                    if (Manager != null)
                    {
                        ddlMergeRoles.SelectedValue = Manager;
                    }


                    if (Settings.Contains("showSendPassword"))
                    {
                        cbxShowSendPassword.Checked = Convert.ToBoolean(ShowSendPassword);
                    }

                    if (Settings.Contains("emailNewUserCredentials"))
                    {
                        cbxEmailNewUserCredentials.Checked = Convert.ToBoolean(EmailNewUserCredentials);
                    }

                    if (Settings.Contains("showDonationHistory"))
                    {
                        cbxShowDonationHistory.Checked = Convert.ToBoolean(ShowDonationHistory);
                    }

                    if (Settings.Contains("enableAddNewDonor"))
                    {
                        cbxEnableAddNewDonor.Checked = Convert.ToBoolean(EnableAddNewDonor);
                    }




                   
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void GetRoles()
        {
            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();

            var myRoles = rc.GetRoles(this.PortalId);
            //  myRoles
            ddlRoles.DataSource = myRoles;
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleName";
            ddlRoles.DataBind();

            // ADD FIRST (NULL) ITEM
            ListItem item = new ListItem();
            item.Text = "-- Select Role to Assign --";
            item.Value = "";
            ddlRoles.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlRoles.Items.Remove("Administrators");
            ddlRoles.Items.Remove("Registered Users");
            ddlRoles.Items.Remove("Subscribers");

            // REPORTS ROLE
            ddlReportsRoles.DataSource = myRoles;
            ddlReportsRoles.DataBind();

            // ADD FIRST (NULL) ITEM
            ListItem item1 = new ListItem();
            item1.Text = "-- Select Role to View Reports --";
            item1.Value = "";
            ddlReportsRoles.Items.Insert(0, item1);
            // REMOVE DEFAULT ROLES
            ddlReportsRoles.Items.Remove("Administrators");
            ddlReportsRoles.Items.Remove("Registered Users");
            ddlReportsRoles.Items.Remove("Subscribers");

            // MERGE ROLE

            ddlMergeRoles.DataSource = myRoles;
            ddlMergeRoles.DataBind();

            // ADD FIRST (NULL) ITEM
            item1.Value = "Select Role to Allow Merge";
            ddlMergeRoles.Items.Insert(0, item1);
            // REMOVE DEFAULT ROLES
            ddlMergeRoles.Items.Remove("Administrators");
            ddlMergeRoles.Items.Remove("Registered Users");
            ddlMergeRoles.Items.Remove("Subscribers");

        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                //the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
                //module settings
                //modules.UpdateModuleSetting(ModuleId, "Setting1", txtSetting1.Text);
                //modules.UpdateModuleSetting(ModuleId, "Setting2", txtSetting2.Text);

                //tab module settings
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting1",  txtSetting1.Text);
              //  modules.UpdateTabModuleSetting(TabModuleId, "Setting2",  txtSetting2.Text);

                NumPerPage = ddlNumPerPage.SelectedValue;
                RoleName = ddlRoles.SelectedValue;
                EmailMessage = txtEmailMessage.Text;
                EmailFrom = txtEmailFrom.Text;
                EmailSubject = txtEmailSubject.Text;
                EmailBCC = txtEmailBCC.Text;
                ReportsRole = ddlReportsRoles.SelectedValue;
                Manager = ddlMergeRoles.SelectedValue;
                ShowSendPassword = cbxShowSendPassword.Checked.ToString();
                EmailNewUserCredentials = cbxEmailNewUserCredentials.Checked.ToString();
                ShowDonationHistory = cbxShowDonationHistory.Checked.ToString();
                EnableAddNewDonor = cbxEnableAddNewDonor.Checked.ToString();
                IDCardImagePath = txtIDCardImagePath.Text.ToString();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}