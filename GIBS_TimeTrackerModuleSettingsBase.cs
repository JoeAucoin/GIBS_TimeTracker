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

using DotNetNuke.Entities.Modules;

namespace GIBS.Modules.GIBS_TimeTracker
{
    public class GIBS_TimeTrackerModuleSettingsBase : ModuleSettingsBase
    {
        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>

        
        public string IDCardImagePath
        {
            get
            {
                if (Settings.Contains("iDCardImagePath"))
                    return Settings["iDCardImagePath"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "iDCardImagePath", value.ToString());
            }
        }

        public string NumPerPage
        {
            get
            {
                if (Settings.Contains("numPerPage"))
                    return Settings["numPerPage"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "numPerPage", value.ToString());
            }
        }

        public string RoleName
        {
            get
            {
                if (Settings.Contains("roleName"))
                    return Settings["roleName"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "roleName", value.ToString());
            }
        }

        public string ReportsRole
        {
            get
            {
                if (Settings.Contains("reportsRole"))
                    return Settings["reportsRole"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "reportsRole", value.ToString());
            }
        }

        public string Manager
        {
            get
            {
                if (Settings.Contains("manager"))
                    return Settings["manager"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "manager", value.ToString());
            }
        }

        public string EmailFrom
        {
            get
            {
                if (Settings.Contains("emailFrom"))
                    return Settings["emailFrom"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailFrom", value.ToString());
            }
        }

        public string EmailBCC
        {
            get
            {
                if (Settings.Contains("emailBCC"))
                    return Settings["emailBCC"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailBCC", value.ToString());
            }
        }

        public string EmailSubject
        {
            get
            {
                if (Settings.Contains("emailSubject"))
                    return Settings["emailSubject"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailSubject", value.ToString());
            }
        }

        public string EmailMessage
        {
            get
            {
                if (Settings.Contains("emailMessage"))
                    return Settings["emailMessage"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailMessage", value.ToString());
            }
        }

        public string ShowSendPassword
        {
            get
            {
                if (Settings.Contains("showSendPassword"))
                    return Settings["showSendPassword"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showSendPassword", value.ToString());
            }
        }

        public string EmailNewUserCredentials
        {
            get
            {
                if (Settings.Contains("emailNewUserCredentials"))
                    return Settings["emailNewUserCredentials"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailNewUserCredentials", value.ToString());
            }
        }

        public string ShowDonationHistory
        {
            get
            {
                if (Settings.Contains("showDonationHistory"))
                    return Settings["showDonationHistory"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showDonationHistory", value.ToString());
            }
        }

        public string EnableAddNewDonor
        {
            get
            {
                if (Settings.Contains("enableAddNewDonor"))
                    return Settings["enableAddNewDonor"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "enableAddNewDonor", value.ToString());
            }
        }





        #endregion


    }
}