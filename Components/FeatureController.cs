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

//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System.Collections.Generic;

namespace GIBS.Modules.GIBS_TimeTracker.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for GIBS_TimeTracker
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<GIBS_TimeTrackerInfo> colGIBS_TimeTrackers = GetGIBS_TimeTrackers(ModuleID);
        //if (colGIBS_TimeTrackers.Count != 0)
        //{
        //    strXML += "<GIBS_TimeTrackers>";

        //    foreach (GIBS_TimeTrackerInfo objGIBS_TimeTracker in colGIBS_TimeTrackers)
        //    {
        //        strXML += "<GIBS_TimeTracker>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objGIBS_TimeTracker.Content) + "</content>";
        //        strXML += "</GIBS_TimeTracker>";
        //    }
        //    strXML += "</GIBS_TimeTrackers>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlGIBS_TimeTrackers = DotNetNuke.Common.Globals.GetContent(Content, "GIBS_TimeTrackers");
        //foreach (XmlNode xmlGIBS_TimeTracker in xmlGIBS_TimeTrackers.SelectNodes("GIBS_TimeTracker"))
        //{
        //    GIBS_TimeTrackerInfo objGIBS_TimeTracker = new GIBS_TimeTrackerInfo();
        //    objGIBS_TimeTracker.ModuleId = ModuleID;
        //    objGIBS_TimeTracker.Content = xmlGIBS_TimeTracker.SelectSingleNode("content").InnerText;
        //    objGIBS_TimeTracker.CreatedByUser = UserID;
        //    AddGIBS_TimeTracker(objGIBS_TimeTracker);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<GIBS_TimeTrackerInfo> colGIBS_TimeTrackers = GetGIBS_TimeTrackers(ModInfo.ModuleID);

        //foreach (GIBS_TimeTrackerInfo objGIBS_TimeTracker in colGIBS_TimeTrackers)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objGIBS_TimeTracker.Content, objGIBS_TimeTracker.CreatedByUser, objGIBS_TimeTracker.CreatedDate, ModInfo.ModuleID, objGIBS_TimeTracker.ItemId.ToString(), objGIBS_TimeTracker.Content, "ItemId=" + objGIBS_TimeTracker.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

    }

}
