using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jestapp_project.Models
{
    public class Packages
    {
        int? packageId;
        int startStation;
        int endStation;
        float pweight;
        bool expressP;
        int userId;
        int status;
        double price;
        int sLockerID;
        int eLockerID;
        DateTime packTime;

        public int? PackageId { get => packageId; set => packageId = value; }
        public int StartStation { get => startStation; set => startStation = value; }
        public int EndStation { get => endStation; set => endStation = value; }
        public float Pweight { get => pweight; set => pweight = value; }
        public bool ExpressP { get => expressP; set => expressP = value; }
        public int UserId { get => userId; set => userId = value; }
        public int Status { get => status; set => status = value; }
        public double Price { get => price; set => price = value; }
        public int SLockerID { get => sLockerID; set => sLockerID = value; }
        public int ELockerID { get => eLockerID; set => eLockerID = value; }
        public DateTime PackTime { get => packTime; set => packTime = value; }

        public Packages()
        {
        }

    

        public int AddPack()
        {
            DBServices dbs = new DBServices();
            return dbs.AddPack(this);

        }

        public List<Packages> GetPackageID(int UserID)
        {

            DBServices dbs = new DBServices();
            List<Packages> PackageID = dbs.GetPackageID(UserID);
            return PackageID;
        }

        public List<Packages> GetPackageList(int startStation, int endStation , float Pweight, bool express)
        {

            DBServices dbs = new DBServices();
            List<Packages> Packages = dbs.GetPackageList(startStation, endStation , Pweight , express);
            return Packages;
        }

        public void UpdatePackage()
        {

            DBServices dbs = new DBServices();
            dbs.UpdatePackage(this);
        }

        public void UpdatePrice()
        {
            DBServices dbs = new DBServices();
            dbs.UpdatePrice(this);
        }

        public void UpdateLocker()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateLocker(this);
        }


        public List<Packages> GetPrice(int PackageId)
        {

            DBServices dbs = new DBServices();
            List<Packages> PackageID = dbs.GetPrice(PackageId);
            return PackageID;
        }
        public Packages GetSenderId(int packageId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetSenderId(packageId);

        }


        public List<Packages> GetPackagesByStations(int startStation, int endStation)
        {
            DBServices dbs = new DBServices();
            List<Packages> Packages = dbs.GetPackagesByStations(startStation, endStation);
            return Packages;
        }

        public List<Packages> GetCanceledPackages()
        {
            DBServices dbs = new DBServices();
            List<Packages> PackageLockers = dbs.GetCanceledPackages();
            return PackageLockers;


        }
    }
}