using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using DotNetNuke.UI.WebControls;
using GIBS.Modules.GIBS_TimeTracker.Components;
using DotNetNuke.Common.Lists;
using System.Security.Policy;

namespace GIBS.Modules.GIBS_TimeTracker
{
    public partial class ListMembers : PortalModuleBase, IActionable
    {
        private string _Filter = "";

        private int _CurrentPage = 1;
        private ArrayList _Users = new ArrayList();
        protected int TotalPages = -1;
        protected int TotalRecords = 0;
        static int PageSize;
        static string RoleName;
        public string _ReportsRole;
        protected bool SuppressPager = false;
        //       protected string RoleName = "Registered Users";

        public DataTable dt;

        protected int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }
        protected string Filter
        {
            get { return _Filter; }
            set { _Filter = value; }
        }


        //protected void Page_Init(Object sender, EventArgs e)
        //{
        //    foreach (DataGridColumn column in grdUsers.Columns)
        //    {

        //        if (column.GetType() == typeof(ImageCommandColumn))
        //        {

        //            ImageCommandColumn imageColumn = (ImageCommandColumn)column;

        //            //Manage Edit Column NavigateURLFormatString                      
        //            if (imageColumn.CommandName == "Edit")
        //            {
        //                //The Friendly URL parser does not like non-alphanumeric characters                          
        //                //so first create the format string with a dummy value and then                          
        //                //replace the dummy value with the FormatString place holder                          
        //                string formatString = EditUrl("UserId", "KEYFIELD", "Edit");
        //               // string formatString = EditUrl("UserId", "KEYFIELD", "Edit", "TabFocus=DonorRecord");
        //                formatString = formatString.Replace("KEYFIELD", "{0}");
        //                imageColumn.NavigateURLFormatString = formatString;
        //            }

        //            //CheckInOut
        //            if (imageColumn.CommandName == "CheckInOut")
        //            {
        //                //The Friendly URL parser does not like non-alphanumeric characters                          
        //                //so first create the format string with a dummy value and then                          
        //                //replace the dummy value with the FormatString place holder                          
        //                string formatString1 = Globals.NavigateURL(TabId, "", "UserId=IDFIELD");

        //                formatString1 = formatString1.Replace("IDFIELD", "{0}");
        //                imageColumn.NavigateURLFormatString = formatString1;
        //            }


        //            if (imageColumn.CommandName == "MakeID")
        //            {
        //                //The Friendly URL parser does not like non-alphanumeric characters                          
        //                //so first create the format string with a dummy value and then                          
        //                //replace the dummy value with the FormatString place holder
        //                //
        //                //   /ctl/MakeID/mid/386?cid=2&SkinSrc=[G]Skins%252f_default%252fpopUpSkin&ContainerSrc=%252fPortals%252f_default%252fContainers%252f_default%252fNo+Container&popUp=true

        //                string formatString1 = Globals.NavigateURL(TabId, "MakeID", "AutoIDCard", "true", "mid", this.ModuleId.ToString(), "cid=IDFIELD&SkinSrc=[G]Skins%252f_default%252fpopUpSkin&ContainerSrc=%252fPortals%252f_default%252fContainers%252f_default%252fNo+Container&popUp=true");

        //                formatString1 = formatString1.Replace("IDFIELD", "{0}");
                        
        //                imageColumn.NavigateURLFormatString = formatString1;
        //            }

        //            //Localize Image Column Text                     
        //            if (!String.IsNullOrEmpty(imageColumn.CommandName))
        //            {
        //                imageColumn.Text = Localization.GetString(imageColumn.CommandName, this.LocalResourceFile);
        //            }
        //        }
        //        //column.Visible = isVisible;              
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {



                if (!IsPostBack)
                {
                    txtSearch.Focus();

                    

                    if (Request.QueryString["CurrentPage"] != null)
                    {
                        CurrentPage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
                    }
                    else
                    {
                        CurrentPage = 1;
                    }

          //          BindData("A", "LastName", "LastName", "asc");


                    if (Request.QueryString["SearchField"] != null)
                    {
                        ddlSearchType.SelectedValue = Request.QueryString["SearchField"];
                    }

                    if (Request.QueryString["OrderBy"] != null)
                    {
                        ddlOrderBy.SelectedValue = Request.QueryString["OrderBy"];
                    }



                    CreateLetterSearch();
                    //  LoadDropdowns();
                    LoadSettings();
                    SetReportsButtonView();


                    if (Request.QueryString["filter"] != null)
                    {
                        //   Filter = Request.QueryString["filter"];
                        txtSearch.Text = Request.QueryString["filter"].ToString();

                        BindData(txtSearch.Text.ToString(), ddlSearchType.SelectedValue.ToString(), ddlOrderBy.SelectedValue.ToString(), "asc");

                    }

                    else
                    {
                        BindData("%", "LastName", "LastName", "asc");
                    }
                    

                }

                else
                {

                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        private void BindData()
        {
            BindData(null, null, null, null);
            ctlPagingControl.Visible = false;
        }

        public void BindData(string SearchText, string SearchField, string orderByField, string orderByDirection)
        {

            ctlPagingControl.Visible = true;

            orderByDirection = "asc";
            string strQuerystring = Null.NullString;


            //if (Filter == Localization.GetString("All"))
            //{
            //    SearchText = "";
            //}

            if (txtSearch.Text.ToString().Trim().Length > 0)
            {
                strQuerystring += "filter=" + txtSearch.Text.ToString();
            }

            strQuerystring += "&SearchField=" + SearchField.ToString();
            strQuerystring += "&OrderBy=" + orderByField.ToString();
            strQuerystring += "&ctl=ListMembers";
            strQuerystring += "&mid=" + this.ModuleId.ToString();

            TimeTrackerController controller = new TimeTrackerController(); //ref TotalRecords,
            List<TimeTrackerInfo> Users11 = new List<TimeTrackerInfo>();

            //Joe this
            Users11 = controller.UserFullListSearch(this.PortalId, CurrentPage, PageSize, SearchField, SearchText, orderByField, orderByDirection);


            DataTable dt = new DataTable();
            dt = Components.GridViewTools.ToDataTable(Users11);

            TotalRecords = (from DataRow dr in dt.Rows
                            select (int)dr["TotalRecords"]).FirstOrDefault();

            CurrentPage = (from DataRow dr in dt.Rows
                           select (int)dr["CurrentPage"]).FirstOrDefault();

            //        PageSize = (from DataRow dr in dt.Rows
            //                   select (int)dr["RecordsperPage"]).FirstOrDefault();

            lblDebug.Text = "Criteria: " + SearchText.ToString()
                + "  <br />Searching: " + ddlSearchType.SelectedItem.ToString()
                + "  <br />Order By: " + ddlOrderBy.SelectedItem.ToString()
                + "<br />Total Records: " + TotalRecords.ToString();

            //           	 Number Of Pages	
        //   grdUsers.DataSource = dt;
        //    grdUsers.DataBind();

            gvUsers.DataSource = dt;
            gvUsers.DataBind();

            ctlPagingControl.TotalRecords = TotalRecords;
            ctlPagingControl.PageSize = PageSize;
            ctlPagingControl.CurrentPage = CurrentPage;
            ctlPagingControl.QuerystringParams = strQuerystring;
            ctlPagingControl.TabID = TabId;

        }


        protected void dnnGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

            grdUsers.CurrentPageIndex = e.NewPageIndex;
            BindData();

        }



        protected void btnSearch_Click(Object sender, ImageClickEventArgs e)
        {
            TotalRecords = 0;
            CurrentPage = 1;


            string _criteria = "";
            if (txtSearch.Text.ToString().Trim().Length == 0)
            {
                _criteria = "Return=AllRecords";
            }
            else
            {
                _criteria = "filter=" + txtSearch.Text.ToString();
            }


            string _URL = "";
            _URL = Globals.NavigateURL(TabId, "ListMembers", _criteria.ToString(),"mid=" + ModuleId.ToString(), "currentpage=1", "SearchField=" + ddlSearchType.SelectedValue, "OrderBy=" + ddlOrderBy.SelectedValue);
            Response.Redirect(_URL);


        }


        public void SetReportsButtonView()
        {
            try
            {

                var roleGroup = UserInfo.IsInRole(_ReportsRole);

                if (roleGroup == true)
                {
                    // TURN OFF LEGACY REPORTS
                    //btnReports.Visible = true;
                    //btnReportSearch.Visible = true;
                }
                else
                {
               //     btnReports.Visible = false;
               //     btnReportSearch.Visible = false;

                }



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

                if (Settings.Contains("numPerPage"))
                {
                    PageSize = Int32.Parse(Settings["numPerPage"].ToString());
                }

                if (Settings.Contains("roleName"))
                {
                    RoleName = Settings["roleName"].ToString();
                }

                if (Settings.Contains("reportsRole"))
                {
                    _ReportsRole = Settings["reportsRole"].ToString();
                }

                if (Settings.Contains("enableAddNewDonor"))
                {
                    if (Convert.ToBoolean(Settings["enableAddNewDonor"].ToString()) == true)
                    {
                        btnAddNewUser.Enabled = true;
                    }
                    else
                    {
                        btnAddNewUser.Enabled = false;
                    }
                }
                else
                {
                    btnAddNewUser.Enabled = false;
                }
               

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        private void CreateLetterSearch()
        {
            string filters = Localization.GetString("Filter.Text", this.LocalResourceFile);
            //filters += "," + Localization.GetString("All");
            //filters += "," + Localization.GetString("Donors Only"); 
            //    filters += "," + Localization.GetString("OnLine"); 
            //   filters += "," + Localization.GetString("Unauthorized"); 
            string[] strAlphabet = filters.Split(',');
            rptLetterSearch.DataSource = strAlphabet;
            rptLetterSearch.DataBind();
        }

        protected string FilterURL(string Filter, string CurrentPage)
        {
            string _URL = Null.NullString;

            _URL = Globals.NavigateURL(TabId, "ListMembers", "mid=" + ModuleId.ToString(), "filter=" + Filter, "currentpage=1", "SearchField=" + ddlSearchType.SelectedValue.ToString(), "OrderBy=" + ddlOrderBy.SelectedValue.ToString());

            return _URL;
        }

        protected string GetStateLookup(object regionID)
        {
            try
            {
                string _state = "";
                int n;
                bool isNumeric = int.TryParse(regionID.ToString(), out n);

                if (isNumeric)
                {
                    // Get State from DNN Lists
                    ListController ctlList = new ListController();
                    ListEntryInfo vStateLookup = ctlList.GetListEntryInfo(Convert.ToInt32(regionID));

                    //_state = vStateLookup.Text.ToString();
                    _state = vStateLookup.Value.ToString();
                    return _state.ToString();
                }
                else
                {
                    return regionID.ToString();
                }


            }
            catch (Exception exc) //Module failed to load             
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                return "ERROR ON LOOKUP";
            }
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            //  Response.Redirect(Globals.NavigateURL(this.TabId, EditUrl(), ""), true);
            Response.Redirect(EditUrl("TabFocus", "UserRecord"));
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.View, true, false
                        }
                    };
                return actions;
            }
        }

        protected void grdUsers_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        protected void gvUsers_PageIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Edit
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink myHL = (HyperLink)e.Row.FindControl("HyperLinkEditUser");
                string formatString = EditUrl("ttUserId", "KEYFIELD", "Edit");
                formatString = formatString.Replace("KEYFIELD", e.Row.Cells[0].Text.ToString());
                myHL.NavigateUrl = formatString.ToString();

                HyperLink myHLMakeIDCard = (HyperLink)e.Row.FindControl("HyperLinkMakeIDCard");
                string formatString1 = Globals.NavigateURL(TabId, "MakeID", "AutoIDCard", "true", "mid", this.ModuleId.ToString(), "cid=IDFIELD&SkinSrc=[G]Skins%252f_default%252fpopUpSkin&ContainerSrc=%252fPortals%252f_default%252fContainers%252f_default%252fNo+Container&popUp=true");
                formatString1 = formatString1.Replace("IDFIELD", e.Row.Cells[0].Text.ToString());
                myHLMakeIDCard.NavigateUrl = formatString1.ToString();

                //HyperLinkCheckInOut
                HyperLink myHLCheckInOut = (HyperLink)e.Row.FindControl("HyperLinkCheckInOut");
                string formatString2 = Globals.NavigateURL(TabId, "", "ttUserId=IDFIELD");
                formatString2 = formatString2.Replace("IDFIELD", e.Row.Cells[0].Text.ToString());
                myHLCheckInOut.NavigateUrl = formatString2.ToString();

                //HyperLinkAddShift
                HyperLink myHLAddShift = (HyperLink)e.Row.FindControl("HyperLinkAddShift");
                string fromatString3 = Globals.NavigateURL(TabId, "EditCheckInOut", "mid", this.ModuleId.ToString(), "ttUserId=IDFIELD");
                fromatString3 = fromatString3.Replace("IDFIELD", e.Row.Cells[0].Text.ToString());
                myHLAddShift.NavigateUrl = fromatString3.ToString();

            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}