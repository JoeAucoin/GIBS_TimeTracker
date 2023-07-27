using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Services.Exceptions;
using System;
using GIBS.Modules.GIBS_TimeTracker.Components;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GIBS.Modules.GIBS_TimeTracker
{
    public partial class Camera : PortalModuleBase
    {
        public int ttUserID = 0;


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
            //    JavaScript.RequestRegistration(CommonJs.jQueryUI);
            //    JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CameraScript", (this.TemplateSourceDirectory + "/JavaScript/Camera.js"));


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cid"] != null)
            {
                ttUserID = Int32.Parse(Request.QueryString["cid"]);
            }
            FillClientRecord(ttUserID);
            SetMakeIDLink();
        }

        public void SaveImageToDatabase(string imageData)
        {
            try
            {

                TimeTrackerController controller = new TimeTrackerController();
                TimeTrackerInfo item = new TimeTrackerInfo();

                byte[] imageBytes = Convert.FromBase64String(imageData.Replace("data:image/png;base64,", String.Empty));
                item.TTUserID = ttUserID;
                item.IDPhoto = imageBytes;
                item.CreatedByUserID = this.UserId;

                controller.IDPhoto_Insert(item);

                FillClientRecord(ttUserID);
            }

            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        protected void ButtonSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                //  UploadImage(HiddenFieldImage.Value);
                SaveImageToDatabase(HiddenFieldImage.Value);
                HyperLinkMakeID.Visible = true;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void FillClientRecord(int ttUserID)
        {

            try
            {
                //load the item
                TimeTrackerController controller = new TimeTrackerController();
                TimeTrackerInfo item = controller.GetPhotoByUserID(ttUserID);

                if (item != null)
                {

                    LabelClientInfo.Text = item.FirstName + ' ' + item.LastName + " - ";
                    

                    Session["TTUserID"] = ttUserID.ToString();
                    var queryString = "?TTUserID=" + Session["TTUserID"].ToString() + "&contactname=" + item.FirstName.ToString() + " " + item.LastName.ToString();


                    if (item.IDPhoto != null)
                    {
                        ImageIDClient.Visible = true;
                        byte[] imagem = (byte[])(item.IDPhoto);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);

                        ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        ImageIDClient.AlternateText = item.FirstName + ' ' + item.LastName;
                        HyperLinkMakeID.Visible = true;
                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                        HyperLinkMakeID.Visible = false;
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

        public void SetMakeIDLink()
        {

            //HyperLinkPhotoID
            try
            {

                string myLink = DotNetNuke.Common.Globals.NavigateURL("MakeID", "mid=" + this.ModuleId);
                //myLink += "?cid=" + clientId.ToString() + "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "?cid=" + ttUserID.ToString();

                myLink += "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "&ContainerSrc=";
                myLink += DotNetNuke.Common.Globals.QueryStringEncode("/Portals/_default/Containers/_default/No Container");


                string redirectUrl = UrlUtils.PopUpUrl(myLink, this, PortalSettings, false, true);

                HyperLinkMakeID.NavigateUrl = redirectUrl.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ButtonReturnToClientManager_Click(object sender, EventArgs e)
        {

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&cid=" + ttUserID.ToString()));
            //   object p =   DependencyProvider.GetRequiredService( INavigationManager).NavigateURL(PortalSettings.ActiveTab.TabID, "Edit", "mid=" + ModuleId.ToString());
            //      object p = DependencyProvider.GetRequiredService(INavigationManager).NavigateURL(PortalSettings.ActiveTab.TabID, "Edit", "mid=" + ModuleId.ToString());
        }


    }
}