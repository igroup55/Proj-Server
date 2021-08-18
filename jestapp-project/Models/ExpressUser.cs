using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jestapp_project.Models
{
    public class ExpressUser
    {
        int userID;
        string fullName;
        int packageID;
        int status;
        double rating;
        int customerID;
        string address;
        int custPhoneNum;
        DateTime packTime;

        public int UserID { get => userID; set => userID = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public int PackageID { get => packageID; set => packageID = value; }
        public int Status { get => status; set => status = value; }
        public double Rating { get => rating; set => rating = value; }
        public int CustomerID { get => customerID; set => customerID = value; }
        public string Address { get => address; set => address = value; }
        public int CustPhoneNum { get => custPhoneNum; set => custPhoneNum = value; }
        public DateTime PackTime { get => packTime; set => packTime = value; }

        public ExpressUser() { }

        public ExpressUser(int userID, string fullName, int packageID, int status, double rating, int customerID, string address, int custPhoneNum, DateTime packTime)
        {
            UserID = userID;
            FullName = fullName;
            PackageID = packageID;
            Status = status;
            Rating = rating;
            CustomerID = customerID;
            Address = address;
            CustPhoneNum = custPhoneNum;
            PackTime = packTime;
        }

        public int PostUser(ExpressUser UserDetails)
        {
            DBServices dbs = new DBServices();
            return dbs.PostUser(UserDetails);
        }

        public void UpdateExPackage(ExpressUser ExPackage)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateExPackage(this);
        }
        public int UpdateStatusDelivered(int PackageId)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateStatusDelivered(PackageId);
        }
        public List<ExpressUser> GetCanceledEx()
        {
            DBServices dbs = new DBServices();
            List<ExpressUser> ExPackageLockers = dbs.GetCanceledEx();
            return ExPackageLockers;


        }

    }
}