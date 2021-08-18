using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jestapp_project.Models
{
    
    public class Customers
    {
        int customerID;
        string fullName;
        string address;
        int phoneNum;
        int packageId;
        double latitude;
        double longitude;

        public Customers()
        {
        }

        public Customers(int customerID, string fullName, string address, int phoneNum, int packageId, double latitude, double longitude)
        {
            CustomerID = customerID;
            FullName = fullName;
            Address = address;
            PhoneNum = phoneNum;
            PackageId = packageId;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int CustomerID { get => customerID; set => customerID = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Address { get => address; set => address = value; }
        public int PhoneNum { get => phoneNum; set => phoneNum = value; }
        public int PackageId { get => packageId; set => packageId = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }

        public int AddCust()
        {
            DBServices dbs = new DBServices();
            return dbs.AddCust(this);
        }

        public Customers GetCustDetails(int PackageId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetCustDetails(PackageId);
        }
        public List<Customers> GetExpressCustomerList(int UserID, double Elat, double Elng, int Station)
        {
            DBServices dbs = new DBServices();
            return dbs.GetExpressCustomerList(UserID, Elat, Elng, Station);
        }
    }
}