using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jestapp_project.Models
{
    public class TDUser
    {
        int deliveryID;
        int userID;
        float pweight;
        int packageID;
        int status;
        int startStation;
        int endStation;
        float rating;
        DateTime pickUpDT;

        public int DeliveryID { get => deliveryID; set => deliveryID = value; }
        public int UserID { get => userID; set => userID = value; }
        public float Pweight { get => pweight; set => pweight = value; }
        public int PackageID { get => packageID; set => packageID = value; }
        public int Status { get => status; set => status = value; }
        public int StartStation { get => startStation; set => startStation = value; }
        public int EndStation { get => endStation; set => endStation = value; }
        public float Rating { get => rating; set => rating = value; }
        public DateTime PickUpDT { get => pickUpDT; set => pickUpDT = value; }

        public TDUser()
        {
        }

        public TDUser(int deliveryID, int userID, float pweight, int packageID, int status, int startStation, int endStation, float rating, DateTime pickUpDT)
        {
            DeliveryID = deliveryID;
            UserID = userID;
            Pweight = pweight;
            PackageID = packageID;
            Status = status;
            StartStation = startStation;
            EndStation = endStation;
            Rating = rating;
            PickUpDT = pickUpDT;
        }

        public int AddTDUser()
        {

            DBServices dbs = new DBServices();
            return dbs.AddTDUser(this);
        }

        public TDUser GetInterestedTD(int UserId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetInterestedTD(UserId);


        }

        public int GetPossiblePickup(int StartStation, int EndStation, int UserId)
        {

            DBServices dbs = new DBServices();
            return dbs.GetPossiblePickup(StartStation, EndStation, UserId);


        }

        public List<TDUser> GetTDUserList(int startStation, int endStation, float Pweight)
        {

            DBServices dbs = new DBServices();
            List<TDUser> Interests = dbs.GetTDUserList(startStation, endStation, Pweight);
            return Interests;
        }


        public List<TDUser> GetDeliveryId(int UserId)
        {

            DBServices dbs = new DBServices();
            return dbs.GetDeliveryId(UserId);
        }

        public void UpdateTDUserPack()
        {

            DBServices dbs = new DBServices();
            dbs.UpdateTDUserPack(this);
        }

        public List<float> RatingAlgorithm(TDUser Category)
        {
            List<float> RatingList = new List<float>();
            DBServices dbs = new DBServices();
            dbs = dbs.RatingAlgorithm(Category);

            dbs.dt = EditRating(dbs.dt, Category);

            dbs.UpdateRating();

            foreach (DataRow dr in dbs.dt.Rows)
            {
                float c = new float();
                //c.DeliveryID = Convert.ToInt32(dr["DeliveryID"]);
                //c.UserID = Convert.ToInt32(dr["UserID"]);
                ////c.PackageID = Convert.ToInt32(dr["PackageID"]);
                //c.Pweight = (float)dr["Pweight"];
                //c.StartStation = Convert.ToInt32(dr["StartStation"]);
                //c.EndStation = Convert.ToInt32(dr["EndStation"]);
                //c.Status = Convert.ToInt16(dr["Status"]);
                c = (float)dr["Rating"];
                RatingList.Add(c);
            }
            return RatingList;
        }

        private DataTable EditRating(DataTable dt, TDUser Category)
        {


        
            
            foreach (DataRow dr in dt.Rows)
            {
               int DeliveryID = Convert.ToInt32(dr["DeliveryID"]);
                int UserID = Convert.ToInt32(dr["UserID"]);
              //  int PackageID = Convert.ToInt32(dr["PackageID"]);
                float Pweight = (float)dr["Pweight"];
                int StartStation = Convert.ToInt32(dr["StartStation"]);
                int EndStation = Convert.ToInt32(dr["EndStation"]);
                 int Status = Convert.ToInt16(dr["Status"]);
                float Rating = (float)dr["Rating"];

                if(Rating <= 10 && Rating >= 0)
                {

                    if (UserID == Category.UserID)
                    {

                        dr["Rating"] = Rating + 0.2;


                    }
                    else
                    {
                        if (StartStation == Category.StartStation && EndStation == Category.EndStation && Pweight == Category.Pweight && Status == -1)
                        {

                            dr["Rating"] = Rating - 0.1;
                        }

                    }

                }
         
                  
                


            }


          


            return dt;
        }
       
        public List<TDUser> GetUpdatedRating( int UserId)
        {

            DBServices dbs = new DBServices();

            return dbs.GetUpdatedRating(UserId);
        }


      
        public List<TDUser> GetDate(int StartStation, int EndStation, int UserId, DateTime PickUpDT)
        {
            List<TDUser> UserDT = new List<TDUser>();
            DBServices dbs = new DBServices();
            dbs = dbs.GetDate(StartStation, EndStation, UserId, PickUpDT);

            dbs.dt = CheckDate(dbs.dt, StartStation, EndStation, UserId, PickUpDT);

            //   dbs.UpdateDate();
            if (dbs.dt == null) { return null; }
            foreach (DataRow dr in dbs.dt.Rows)
            {
                TDUser c = new TDUser();
                c.UserID = Convert.ToInt32(dr["UserID"]);
                c.PickUpDT = Convert.ToDateTime(dr["PickUpDT"]);
                c.DeliveryID = Convert.ToInt32(dr["DeliveryID"]);
                c.PackageID = Convert.ToInt32(dr["PackageID"]);

                UserDT.Add(c);
            }
            return UserDT;
        }

        private DataTable CheckDate(DataTable dt, int StartStation, int EndStation, int UserId, DateTime PickUpDTime)
        {
            DateTime PrevPickUpDT = PickUpDTime;
            DateTime LastDT = PickUpDTime;

            int diff1 = 0;
            int diff2 = 0;
            int diff3 = 0;
            int equal = 1;

            foreach (DataRow dr in dt.Rows)
            {
                DateTime PickUpDT = Convert.ToDateTime(dr["PickUpDT"]);
                TimeSpan difference = PrevPickUpDT.Subtract(PickUpDT);
                if (dr == dt.Rows[0])
                {
                    LastDT = PickUpDT;
                }
                if (dr == dt.Rows[1])
                {
                    diff1 = difference.Days;
                }
                if (dr == dt.Rows[2])
                {
                    diff2 = difference.Days;
                }
                if (dr == dt.Rows[3])
                {
                    diff3 = difference.Days;
                }

                if (dr != dt.Rows[0])
                {
                    if (diff1 == diff2 && diff1 == diff3 && diff2 == diff3)
                    {
                        dr["PickUpDT"] = LastDT.AddDays(diff1);
                        equal = 1;
                    }
                    else
                    {
                        equal = 0;
                    }
                }
                PrevPickUpDT = PickUpDT;
            }
            if (equal == 1)
                return dt;
            else
            {
                return null;
            }
        }
    }


}