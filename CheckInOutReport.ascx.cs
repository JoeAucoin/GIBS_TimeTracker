using System;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using GIBS.Modules.GIBS_TimeTracker.Components;
using DotNetNuke.UI.UserControls;
using MigraDoc.DocumentObjectModel.Tables;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.WebControls;

namespace GIBS.Modules.GIBS_TimeTracker
{
    public partial class CheckInOutReport : PortalModuleBase
    {

        static int _eventMID = 0;
        private GridViewHelper helper;
        // To show custom operations...
        private List<int> mQuantities = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtStartDate.Text = DateTime.Now.AddDays(-7).ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
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

                items = controller.GetCheckInReport(Convert.ToDateTime(txtStartDate.Text.ToString()), Convert.ToDateTime(txtEndDate.Text.ToString()));

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
                    e.Row.Cells[6].Text = "Not Checked Out";
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