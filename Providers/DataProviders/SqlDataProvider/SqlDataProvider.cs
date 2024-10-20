/*
' Copyright (c) 2023 GIBS.com
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
using DotNetNuke.Framework;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Reflection;

namespace GIBS.Modules.GIBS_TimeTracker.Data
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// 
    /// This concreted data provider class provides the implementation of the abstract methods 
    /// from data dataprovider.cs
    /// 
    /// In most cases you will only modify the Public methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "GIBS_TimeTracker_";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region Constructors

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            Provider objProvider = (Provider)(_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);

            // Read the attributes for this provider

            //Get Connection string from web.config
            _connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Use connection string specified in provider
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(_objectQualifier) && _objectQualifier.EndsWith("_", StringComparison.Ordinal) == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(_databaseOwner) && _databaseOwner.EndsWith(".", StringComparison.Ordinal) == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        // used to prefect your database objects (stored procedures, tables, views, etc)
        private string NamePrefix
        {
            get { return DatabaseOwner + ObjectQualifier + ModuleQualifier; }
        }

        #endregion

        #region Private Methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + _objectQualifier  + ModuleQualifier + name;
        }

        private static object GetNull(object field)
        {
            return Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region Public Methods

        public override void CheckInOut_Update(int timeTrackerID, int userID, DateTime startTime, DateTime endTime)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CheckInOut_Update"), timeTrackerID, userID, startTime, endTime);
        }

        public override IDataReader UserFullListSearch(int PortalID, int PageIndex, int PageSize, string searchField, string searchCriteria, string orderByField, string OrderByDirection, string roleName)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("UserFullListSearch"), PortalID, PageIndex, PageSize, searchField, searchCriteria, orderByField, OrderByDirection, roleName);
        }

        public override IDataReader GetPhotoByUserID(int ttUserID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetPhotoByUserID"), ttUserID);
        }

        public override IDataReader GetCheckInOutRecord(int ttID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetCheckInOutRecord"), ttID);
        }


        public override void IDPhoto_Insert(int ttUserID, byte[] iDPhoto, int createdByUserID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("IDPhoto_Insert"), ttUserID, iDPhoto, createdByUserID);
        }

        public override IDataReader CheckInOut(DateTime workDate, int userID, int ttUserID, DateTime startTime, DateTime endTime, string location, string iPAddress)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CheckInOut"), workDate, userID, ttUserID, startTime, endTime, location, iPAddress);
        }

        public override IDataReader CheckInOutInsert(DateTime workDate, int userID, int ttUserID, DateTime startTime, DateTime endTime, string location, string iPAddress)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CheckInOutInsert"), workDate, userID, ttUserID, startTime, endTime, location, iPAddress);
        }

        public override IDataReader GetCheckInReport(DateTime startDate, DateTime endDate, string location)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetCheckInReport"), startDate, endDate, location);
        }


        public override IDataReader GetCheckInReport_ForUser(DateTime startDate, DateTime endDate, int ttUserID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetCheckInReport_ForUser"), startDate, endDate, ttUserID);
        }



        //public abstract void CheckInOut(DateTime workDate, int userID, int ttUserID, DateTime startDate, DateTime endDate);



        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}


        #endregion

    }

}