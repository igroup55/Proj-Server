using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jestapp_project.Models.DAL;

namespace jestapp_project.Models
{
    public class Lockers
    {
        int lockerID;
        bool busy;
        int packageID;
        int stationID;

        public Lockers()
        {
        }

        public Lockers(int lockerID, bool busy, int packageID, int stationID)
        {
            LockerID = lockerID;
            Busy = busy;
            PackageID = packageID;
            StationID = stationID;
        }

        public int LockerID { get => lockerID; set => lockerID = value; }
        public bool Busy { get => busy; set => busy = value; }
        public int PackageID { get => packageID; set => packageID = value; }
        public int StationID { get => stationID; set => stationID = value; }

        public List<Lockers> GetEmptyLocker(int StationID)
        {
            DBServices dbs = new DBServices();
            return dbs.GetEmptyLocker(StationID);
        }

        public void UpdateLocker()
        {

            DBServices dbs = new DBServices();
            dbs.UpdateLocker(this);

        }

        public List<Lockers> GetLocker(int PackageID)
        {
            DBServices dbs = new DBServices();
            return dbs.GetLocker(PackageID);
        }
    }
}