using jestapp_project.Models;
using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveLocker
{
    public class Helper
    {
        DBServices dbs = null;
        Packages packages = null;
        ExpressUser ExPackages = null;
        public Helper()
        {
            dbs = new DBServices();
            packages = new Packages();
            ExPackages = new ExpressUser();
        }


        public void HandlePackages()
        {
            //receive array of [ { packageid , slocker , endlocker } ]
            List<Packages> PackagesList = packages.GetCanceledPackages();
            List<ExpressUser> ExPackagesList = ExPackages.GetCanceledEx();
            //Get Packages where PackTime < Now and status = 1
            foreach (var item in PackagesList)
            {
                // update package status to -1
                Packages packagesdetails = new Packages()
                {
                    PackageId = item.PackageId,
                    Status = -1,
                   

                };

                packagesdetails.UpdatePackage();

                //update lockers (start & end) with ( busy = 0 , packageid = null )

                Lockers Sdetails = new Lockers()
                {
                    PackageID = -1,
                    LockerID = item.SLockerID,
                    Busy = false

                };
                Sdetails.UpdateLocker();

                Lockers Edetails = new Lockers()
                {
                    PackageID = -1,
                    LockerID = item.ELockerID,
                    Busy = false

                };
                Edetails.UpdateLocker();
                Console.WriteLine("Package Id : "+item.PackageId+" | Start Locker : "+item.SLockerID+" | End Locker : "+item.ELockerID);
              
            }

            foreach (var item in ExPackagesList)
            {
                // update package status to -1
                ExpressUser expackagesdetails = new ExpressUser()
                {
                    PackageID = item.PackageID,
                    Status = -1,
                    PackTime = item.PackTime

                };

                expackagesdetails.UpdateExPackage(expackagesdetails);

                //update package status to be available for other express users
                Packages ExPackages = new Packages()
                {
                    PackageId = item.PackageID,
                    Status = 4

                };

                ExPackages.UpdatePackage();

                Console.WriteLine("ExPackage Id : " + item.PackageID + " | Ex Status : " + ExPackages.Status);

            }


        }

    }
}
