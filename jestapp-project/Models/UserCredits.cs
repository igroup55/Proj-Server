using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jestapp_project.Models
{
    public class UserCredits
    {
        int userId;
        string fullName;
        double credit;

        public UserCredits()
        {

        }
        public UserCredits(int userId, string fullName, double credit)
        {
            this.userId = userId;
            this.fullName = fullName;
            this.credit = credit;
        }

        public int UserId { get => userId; set => userId = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public double Credit { get => credit; set => credit = value; }

        public List<UserCredits> GetUserCredit(int UserID)
        {

            DBServices dbs = new DBServices();
            List<UserCredits> userCredits = dbs.GetUserCredit(UserID);
            return userCredits;
        }

        public List<UserCredits> GetTDUsers(int TDUserID)
        {

            DBServices dbs = new DBServices();
            List<UserCredits> userCredits = dbs.GetTDUsers(TDUserID);
            return userCredits;
        }
        public int InsertUserCredits(UserCredits userCredits)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertUserCredits(userCredits);
        }

        public void UpdateUserCredits(UserCredits user)
        {

            DBServices dbs = new DBServices();
            dbs.UpdateUserCredits(user);

        }

    }
}