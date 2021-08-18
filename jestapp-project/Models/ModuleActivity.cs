using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jestapp_project.Models.DAL;
namespace jestapp_project.Models
{
    public class ModuleActivity
    {
        int? packageID;
        int status;
        string startStation;
        string endStation;
        int userID1;
        int userID2;

        public ModuleActivity()
        {

        }
        public ModuleActivity(int packageID, int status, string startStation, string endStation, int userID1, int userID2)
        {
            this.packageID = packageID;
            this.status = status;
            this.startStation = startStation;
            this.endStation = endStation;
            this.userID1 = userID1;
            this.userID2 = userID2;
        }

        public int? PackageID { get => packageID; set => packageID = value; }
        public int Status { get => status; set => status = value; }
        public string StartStation { get => startStation; set => startStation = value; }
        public string EndStation { get => endStation; set => endStation = value; }
        public int UserID1 { get => userID1; set => userID1 = value; }
        public int UserID2 { get => userID2; set => userID2 = value; }

        public List<ModuleActivity> GetModuleActivity(int UserID)
        {

            DBServices dbs = new DBServices();
            List<ModuleActivity> ModuleActivityList = dbs.GetModuleActivity(UserID);
            return ModuleActivityList;
        }

        public List<ModuleActivity> GetModuleActivityTD(int UserID)
        {

            DBServices dbs = new DBServices();
            List<ModuleActivity> ModuleActivityList = dbs.GetModuleActivityTD(UserID);
            return ModuleActivityList;
        }

        
            public List<ModuleActivity> GetModuleActivityEx(int UserID)
        {

            DBServices dbs = new DBServices();
            List<ModuleActivity> ModuleActivityList = dbs.GetModuleActivityEx(UserID);
            return ModuleActivityList;
        }
    }

}