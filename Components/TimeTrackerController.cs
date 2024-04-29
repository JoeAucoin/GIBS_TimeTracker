using DotNetNuke.Entities.Modules;
using DotNetNuke.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GIBS.Modules.GIBS_TimeTracker.Data;
using Microsoft.ApplicationBlocks.Data;

namespace GIBS.Modules.GIBS_TimeTracker.Components
{
    public class TimeTrackerController 
    {

        public void CheckInOut_Update(TimeTrackerInfo info)
        {
            //check we have some content to update
            if (info.EndTime != null)
            {
                DataProvider.Instance().CheckInOut_Update(info.TimeTrackerID, info.UserID, info.StartTime, info.EndTime, info.Location);
            }
        }

        public List<TimeTrackerInfo> UserFullListSearch(int PortalID, int PageIndex, int PageSize, string searchField, string searchCriteria, string orderByField, string OrderByDirection)
        {
            return CBO.FillCollection<TimeTrackerInfo>(DataProvider.Instance().UserFullListSearch(PortalID, PageIndex, PageSize, searchField, searchCriteria, orderByField, OrderByDirection));
        }
        //GetCheckInReport

        public List<TimeTrackerInfo> GetCheckInReport(DateTime startDate, DateTime endDate, string location)
        {
            return CBO.FillCollection<TimeTrackerInfo>(DataProvider.Instance().GetCheckInReport(startDate, endDate, location));
        }


        public List<TimeTrackerInfo> GetCheckInReport_ForUser(DateTime startDate, DateTime endDate, int ttUserID)
        {
            return CBO.FillCollection<TimeTrackerInfo>(DataProvider.Instance().GetCheckInReport_ForUser(startDate, endDate, ttUserID));
        }

        public TimeTrackerInfo GetPhotoByUserID(int ttUserID)
        {

            return CBO.FillObject<TimeTrackerInfo>(DataProvider.Instance().GetPhotoByUserID(ttUserID));
        }


        public TimeTrackerInfo GetCheckInOutRecord(int ttID)
        {

            return CBO.FillObject<TimeTrackerInfo>(DataProvider.Instance().GetCheckInOutRecord(ttID));
        }


        public void IDPhoto_Insert(TimeTrackerInfo info)
        {
            //check we have some content to update
            if (info.IDPhoto != null)
            {
                DataProvider.Instance().IDPhoto_Insert(info.TTUserID, info.IDPhoto, info.CreatedByUserID);
            }
        }

        public TimeTrackerInfo CheckInOut(TimeTrackerInfo info)
        {
            //check we have some content to update
            if (info.TTUserID > 0)
            {
              //  DataProvider.Instance().CheckInOut(info.WorkDate, info.UserID, info.TTUserID, info.StartTime, info.EndTime);
                return CBO.FillObject<TimeTrackerInfo>(DataProvider.Instance().CheckInOut(info.WorkDate, info.UserID, info.TTUserID, info.StartTime, info.EndTime, info.Location));
            }
            else
                return null;
            
        }

        public TimeTrackerInfo CheckInOutInsert(TimeTrackerInfo info)
        {
            //check we have some content to update
            if (info.TTUserID > 0)
            {
                //  DataProvider.Instance().CheckInOutInsert(info.WorkDate, info.UserID, info.TTUserID, info.StartTime, info.EndTime);
                return CBO.FillObject<TimeTrackerInfo>(DataProvider.Instance().CheckInOutInsert(info.WorkDate, info.UserID, info.TTUserID, info.StartTime, info.EndTime, info.Location));
            }
            else
                return null;

        }




    }
}