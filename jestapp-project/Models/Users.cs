using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jestapp_project.Models.DAL;


namespace jestapp_project.Models
{
    public class Users
    {
        int userId;
        string fullName;
        string phoneNum;
        string emailAddress;
        string password;
        string profilePic;
        DateTime birthDate;
        int rating;
        string token;
        DateTime dTtoken;

        public int UserId { get => userId; set => userId = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string Password { get => password; set => password = value; }
        public string ProfilePic { get => profilePic; set => profilePic = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Token { get => token; set => token = value; }
        public DateTime DTtoken { get => dTtoken; set => dTtoken = value; }

        public Users() { }

        public Users(int userId, string fullName, string phoneNum, string emailAddress, string password, string profilePic, DateTime birthDate, int rating, string token, DateTime dTtoken)
        {
            UserId = userId;
            FullName = fullName;
            PhoneNum = phoneNum;
            EmailAddress = emailAddress;
            Password = password;
            ProfilePic = profilePic;
            BirthDate = birthDate;
            Rating = rating;
            Token = token;
            DTtoken = dTtoken;
        }

        public int InsertUser()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertUser(this);
        }

        public List<Users> LoginUser(string email, string pass)
        {
            DBServices dbs = new DBServices();
            List<Users> userlist = dbs.LoginUser(email, pass);
            return userlist;

        }

        public List<Users> CheckExistUser(int UserId)
        {
            DBServices dbs = new DBServices();
            List<Users> userlist = dbs.CheckExistUser(UserId);
            return userlist;

        }
        public Users GetSenderToken(int UserId, int PackageID)
        {
            DBServices dbs = new DBServices();
            return dbs.GetSenderToken(UserId, PackageID);
        }
        public void UpdateUserToken(int userId, string token)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateUserToken(userId, token);

        }

        public void UpdateDT()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateDT(this);
        }

        public List<Users> GetUsers()
        {
            DBServices dbs = new DBServices();
            return dbs.GetUsers();
        }
    }
}