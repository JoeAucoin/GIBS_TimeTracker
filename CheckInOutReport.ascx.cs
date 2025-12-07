using DotNetNuke.Abstractions;
using DotNetNuke.Entities.Modules;
using Microsoft.Extensions.DependencyInjection;
using DotNetNuke.Services.Exceptions;
using GIBS.Modules.GIBS_TimeTracker.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace GIBS.Modules.GIBS_TimeTracker
{
    public partial class CheckInOutReport : PortalModuleBase
    {
        private INavigationManager _navigationManager;
        //  static int _eventMID = 0;
        private GridViewHelper helper;
        // To show custom operations...
        private List<int> mQuantities = new List<int>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _navigationManager = DependencyProvider.GetRequiredService<INavigationManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
                if (Settings.Contains("location"))
                {
                    ddlLocations.SelectedValue = Settings["location"].ToString();
                }

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

                items = controller.GetCheckInReport(Convert.ToDateTime(txtStartDate.Text.ToString()), Convert.ToDateTime(txtEndDate.Text.ToString()), ddlLocations.SelectedValue.ToString());

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
            // Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditCheckInOut", "mid=" + ModuleId.ToString() + "&ItemId=" + itemID));
            var url = _navigationManager.NavigateURL(PortalSettings.ActiveTab.TabID, "EditCheckInOut", $"mid={ModuleId}", $"ItemId={itemID}");
            Response.Redirect(url);

        }

        protected void gv_Report_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_navigationManager == null)
                {
                    Exceptions.ProcessModuleLoadException(this, new Exception("_navigationManager is null."));
                    return;
                }

                var hyperLink = e.Row.FindControl("HyperLink1") as HyperLink;
                if (hyperLink != null && e.Row.DataItem != null)
                {
                    var timeTrackerIdObj = DataBinder.Eval(e.Row.DataItem, "TimeTrackerID");
                    string timeTrackerId = timeTrackerIdObj != null ? timeTrackerIdObj.ToString() : string.Empty;
                    string newURL = _navigationManager.NavigateURL(
                        PortalSettings.ActiveTab.TabID,
                        "EditCheckInOut",
                        $"mid={ModuleId}",
                        $"ttid={timeTrackerId}"
                    );
                    hyperLink.NavigateUrl = newURL;
                }
                else if (hyperLink == null)
                {
                    Exceptions.ProcessModuleLoadException(this, new Exception("HyperLink1 not found in row."));
                }
                else if (e.Row.DataItem == null)
                {
                    Exceptions.ProcessModuleLoadException(this, new Exception("DataItem is null in row."));
                }

                if (e.Row.Cells.Count > 7 && e.Row.Cells[7].Text != null && e.Row.Cells[7].Text.Contains("12:00 AM"))
                {
                    e.Row.Cells[7].Text = "Not Checked Out";
                    e.Row.Cells[7].BackColor = System.Drawing.Color.LightPink;
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

        private void SaveQuantity(string column, string group, object value)
        {
            mQuantities.Add(Convert.ToInt32(value));
        }

        private object GetMinQuantity(string column, string group)
        {
            int[] qArray = new int[mQuantities.Count];
            mQuantities.CopyTo(qArray);
            Array.Sort(qArray);
            return qArray[0];
        }



    }
}