using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIBS.Modules.GIBS_TimeTracker.Components
{
    public class TimeTrackerInfo
    {
        private int checkInOut;
        private int timeTrackerID;
        private int moduleId;
        private int donationID;
        private string notes;

        private int createdByUserID;
        private DateTime createdDate;
        private string createdByUserName = null;
        private string updatedByUserName;

        private int driveID;
        private string driveName;
        private DateTime driveDate;

        private int ttUserID;
        private DateTime workDate;
        private DateTime startTime;
        private DateTime endTime;
        private decimal totalTime;
        private DateTime workEndDate;


        private string donationNotes;
        private string donationType;
        private bool followup;
        private string description;
        private bool isActive;

        private string location;

        private bool doNotMail;

        private string prefix;
        private string suffix;
        private string firstName;
        private string lastName;
        private string middleName;

        private string street;
        private string city;
        private string state;
        private string postalCode;
        private string company;
        private int userID;

        // LETTER
        private int letterID;
        private string letter;
        private string pledgeLetter;
        private string pDFFile;
        private string letterName;
        private bool letterGenerated;
        private string letterType;


        private string userName;
        private string displayName;
        private string email;
        private string country;
        private string cell;
        private string telephone;


        //COMMON

        private DateTime createdOnDate;
        private int lastModifiedByUserID;
        private DateTime lastModifiedOnDate;
    
        private string lastModifiedByUserName = null;

        // IDPhoto
        private byte[] iDPhoto;
  //      private int ttImageID;


        // PAGING
        private int totalRecords;
        private int recordsperPage;
        private int currentPage;
        private int pageSize;

        #region properties

        public byte[] IDPhoto
        {

            get { return iDPhoto; }
            set { iDPhoto = value; }
        }


        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        public string Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        //checkInOut CheckInOut
        public int CheckInOut
        {
            get { return checkInOut; }
            set { checkInOut = value; }
        }


        public int TimeTrackerID
        {
            get { return timeTrackerID; }
            set { timeTrackerID = value; }
        }

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public int DonationID
        {
            get { return donationID; }
            set { donationID = value; }
        }


        public int DriveID
        {
            get { return driveID; }
            set { driveID = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }

        }

        public int TTUserID
        {
            get { return ttUserID; }
            set { ttUserID = value; }
        }

        public DateTime WorkDate
        {
            get { return workDate; }
            set { workDate = value; }
        }
        //WorkEndDate
        public DateTime WorkEndDate
        {
            get { return workEndDate; }
            set { workEndDate = value; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

 
        public decimal TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        public string DriveName
        {
            get { return driveName; }
            set { driveName = value; }
        }


        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        public DateTime DriveDate
        {
            get { return driveDate; }
            set { driveDate = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }



        public string DonationType
        {
            get { return donationType; }
            set { donationType = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        //doNotMail
        public bool DoNotMail
        {
            get { return doNotMail; }
            set { doNotMail = value; }
        }

        public bool Followup
        {
            get { return followup; }
            set { followup = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string DonationNotes
        {
            get { return donationNotes; }
            set { donationNotes = value; }
        }

        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }

        public string Suffix
        {
            get { return suffix; }
            set { suffix = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }



        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }


        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        public string Company
        {
            get { return company; }
            set { company = value; }
        }


        public int CreatedByUserID
        {
            get { return createdByUserID; }
            set { createdByUserID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public DateTime CreatedOnDate
        {
            get { return createdOnDate; }
            set { createdOnDate = value; }
        }

        public int LastModifiedByUserID
        {
            get { return lastModifiedByUserID; }
            set { lastModifiedByUserID = value; }
        }

        public DateTime LastModifiedOnDate
        {
            get { return lastModifiedOnDate; }
            set { lastModifiedOnDate = value; }
        }

        public string LastModifiedByUserName
        {

            get { return lastModifiedByUserName; }
            set { lastModifiedByUserName = value; }

        }


        // PAGING

        public int TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; }
        }

        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int RecordsperPage
        {
            get { return recordsperPage; }
            set { recordsperPage = value; }
        }

        public string CreatedByUserName
        {

            get { return createdByUserName; }
            set { createdByUserName = value; }

        }

        public string UpdatedByUserName
        {
            get { return updatedByUserName; }
            set { updatedByUserName = value; }
        }


        public int LetterID
        {
            get { return letterID; }
            set { letterID = value; }
        }


        public string Letter
        {
            get { return letter; }
            set { letter = value; }
        }
        //pledgeLetter
        public string PledgeLetter
        {
            get { return pledgeLetter; }
            set { pledgeLetter = value; }
        }


        //letterName
        public string LetterName
        {
            get { return letterName; }
            set { letterName = value; }
        }

        public bool LetterGenerated
        {
            get { return letterGenerated; }
            set { letterGenerated = value; }

        }

        //
        public string LetterType
        {
            get { return letterType; }
            set { letterType = value; }
        }

        public string PDFFile
        {
            get { return pDFFile; }
            set { pDFFile = value; }
        }


        #endregion

    }
}