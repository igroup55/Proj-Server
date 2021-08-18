using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using jestapp_project.Models;
using System.Net.Http;


namespace jestapp_project.Models.DAL
{
    public class DBServices
    {
        public DataTable dt;
        public SqlDataAdapter da;
        //--------------------------------------------------------------------
        //משתמש נרשם למערכת 
        //--------------------------------------------------------------------
        public int InsertUser(Users users)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand(users);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String ----- Sign Up
        //--------------------------------------------------------------------
        private string BuildInsertCommand(Users users)
        {
            string command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", users.UserId, users.FullName, users.PhoneNum, users.EmailAddress, users.Password, users.ProfilePic, users.BirthDate);
            string prefix = "INSERT INTO tbl_User ([UserId],[Fullname],[PhoneNum],[EmailAddress],[Password],[ProfilePic],[BirthDate]) ";
            command = prefix + sb.ToString();

            return command;
        }


        //--------------------------------------------------------------------
        //בדיקת מייל וסיסמה ---- Log In 
        //--------------------------------------------------------------------
        public List<Users> LoginUser(string email, string pass)
        {
            SqlConnection con = null;
            List<Users> UserList = new List<Users>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                Users user = new Users();
                String selectSTR = "SELECT * FROM tbl_User where EmailAddress='" + email + "'and Password='" + pass + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.FullName = (string)dr["Fullname"];
                    user.PhoneNum = (string)dr["PhoneNum"];
                    user.EmailAddress = (string)dr["EmailAddress"];
                    user.Password = (string)dr["Password"];
                    user.ProfilePic = (string)dr["ProfilePic"];
                    user.BirthDate = (DateTime)dr["BirthDate"];
                    //  user.Rating = Convert.ToInt32(dr["rating"]);

                }
                UserList.Add(user);
                if (UserList.Count < 1)
                {
                    throw new InvalidOperationException("problem with Email or Password");
                }
                return UserList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //--------------------------------------------------------------------שבאן
        //בדיקה האם המשתמש קיים במערכת 
        //--------------------------------------------------------------------
        public List<Users> CheckExistUser(int UserId)
        {
            SqlConnection con = null;
            List<Users> UserList = new List<Users>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT * FROM tbl_User where UserId = " + UserId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Users user = new Users
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FullName = (string)dr["Fullname"],
                        PhoneNum = (string)dr["PhoneNum"],
                        EmailAddress = (string)dr["EmailAddress"],
                        Password = (string)dr["Password"],
                        ProfilePic = (string)dr["ProfilePic"],
                        BirthDate = (DateTime)dr["BirthDate"],
                        //  user.Rating = Convert.ToInt32(dr["rating"]);
                    };
                    UserList.Add(user);
                }
                return UserList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }



        //--------------------------------------------------------------------
        //החזרת כל התחנות והמיקום שלהם
        //--------------------------------------------------------------------
        public List<Stations> GetAllStations()
        {
            SqlConnection con = null;
            List<Stations> StationList = new List<Stations>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Stations ORDER BY StationName";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Stations R = new Stations
                    {
                        StationID = Convert.ToInt32(dr["StationID"]),
                        StationName = (string)dr["StationName"],
                        Latitude = Convert.ToDouble(dr["Latitude"]),
                        Longitude = Convert.ToDouble(dr["Longitude"])


                    };
                    StationList.Add(R);
                }

                return StationList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //--------------------------------------------------------------------
        //הוספת חבילה חדשה
        //--------------------------------------------------------------------
        public int AddPack(Packages packages)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            string cStr = BuildInsertCommand(packages);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }

        }

        public SqlConnection Connect(string conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private string BuildInsertCommand(Packages packages)
        {
            string command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values ({0},{1},{2},'{3}',{4},{5},'{6}')", packages.StartStation, packages.EndStation, packages.Pweight, packages.ExpressP, packages.UserId, packages.Status,packages.PackTime);
            string prefix = "INSERT INTO Packages ([StartStation],[EndStation],[Pweight],[ExpressP],[UserId],[Status],[PackTime])";

            command = prefix + " " + sb.ToString() + " SELECT SCOPE_IDENTITY()";
            return command;
        }

        private SqlCommand CreateCommand(string CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand
            {
                Connection = con,              // assign the connection to the command object

                CommandText = CommandSTR,      // can be Select, Insert, Update, Delete 

                CommandTimeout = 10,           // Time to wait for the execution' The default is 30 seconds

                CommandType = System.Data.CommandType.Text // the type of the command, can also be stored procedure
            }; // create the command object

            return cmd;
        }

        //------------------------------------------------------------------------------------------------------------------
        //Insert A New Customer
        //------------------------------------------------------------------------------------------------------------------
        public int AddCust(Customers customers)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand1(customers);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }


        }

        private string BuildInsertCommand1(Customers customers)
        {
            string command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("VALUES ('{0}','{1}',{2},{3},{4},{5})", customers.FullName, customers.Address, customers.PhoneNum, customers.PackageId,customers.Latitude, customers.Longitude);
            string prefix = "INSERT INTO CustomersPackages ([FullName],[Address],[PhoneNum],[PackageId],[Latitude],[Longitude]) ";

            command = prefix + sb.ToString()/* + "SELECT @@IDENTITY"*/;


            return command;

        }

        //--------------------------------------------------------------------------------------------------------------------------
        //Find an empty locker for a new delivery
        //--------------------------------------------------------------------------------------------------------------------------
        public List<Lockers> GetEmptyLocker(int StationID)
        {
            SqlConnection con = null;
            List<Lockers> StationList = new List<Lockers>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT TOP 1 * FROM Lockers WHERE Busy = 0 and StationID =" + StationID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Lockers L = new Lockers
                    {
                        LockerID = Convert.ToInt32(dr["LockerID"]),
                        Busy = (bool)dr["Busy"],

                        StationID = Convert.ToInt32(dr["StationID"]),


                    };
                    StationList.Add(L);
                }

                return StationList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }

        //--------------------------------------------------------------------------------------------------------------------------
        //Get The Identity Package ID
        //---------------------------------------------------------------------------------------------------------------------------
        public List<Packages> GetPackageID(int UserID)
        {

            SqlConnection con = null;
            List<Packages> PackageID = new List<Packages>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT MAX(PackageID) As PackageID FROM Packages Where UserId= " + UserID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Packages R = new Packages
                    {
                        PackageId = Convert.ToInt32(dr["PackageID"])

                    };
                    PackageID.Add(R);
                }

                return PackageID;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        //-----------------------------------------------------------------------------------------------------------------------------------------
        //Update Lockers ( Busy , PackageId)
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public int UpdateLocker(Lockers lockers)
        {


            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand2(lockers);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }


        }

        private string BuildInsertCommand2(Lockers lockers)
        {
            string command;

            if (lockers.PackageID == 0)
            {
                command = "UPDATE Lockers  SET [Busy] = '" + lockers.Busy + "'  where LockerID=" + lockers.LockerID;
            }
            else
            {
                if (lockers.PackageID == -1)
                {
                    command = "UPDATE Lockers  SET [Busy] = '" + lockers.Busy + "' , [PackageID] = null  where LockerID=" + lockers.LockerID;
                }
                else
                {

                    command = "UPDATE Lockers  SET [Busy] = '" + lockers.Busy + "' , [PackageID] =" + lockers.PackageID + "  where LockerID=" + lockers.LockerID;
                }
            }



            return command;
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------
        //Update Package Status   (Deposit,PickUp,Cancel)
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public int UpdatePackage(Packages packages)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand3(packages);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }



        }

        private string BuildInsertCommand3(Packages packages)
        {
            string command;


            command = "UPDATE Packages  SET [Status] = '" + packages.Status + "'  where PackageID=" + packages.PackageId;


            return command;
        }






        //------------------------------------------------------------------------------------------------------------------
        //החזרת חבילות לצורך לקיחת חבילה אחת  והצגתה למתעניין הראשון + החזרת חבילות במסלול כדי לבדוק האם בכלל יש חבילות במסלול
        //------------------------------------------------------------------------------------------------------------------
        public List<Packages> GetPackageList(int startStation, int endStation, float Pweight , bool express)
        {
            SqlConnection con = null;
            List<Packages> PackagesList = new List<Packages>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

              
                if (Pweight == -1)
                {
                    String selectSTR = "select * FROM Packages where StartStation=" + startStation + " and EndStation=" + endStation + "  And status =2 And ExpressP = " + express;
                    SqlCommand cmd = new SqlCommand(selectSTR, con);
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                    while (dr.Read())
                    {   // Read till the end of the data into a row
                        Packages P = new Packages
                        {


                            PackageId = Convert.ToInt32(dr["PackageId"]),
                            StartStation = Convert.ToInt32(dr["StartStation"]),
                            EndStation = Convert.ToInt32(dr["EndStation"]),
                            Pweight = (float)dr["Pweight"],
                            ExpressP = Convert.ToBoolean(dr["ExpressP"]),
                            Status = Convert.ToInt32(dr["Status"]),
                            UserId = Convert.ToInt32(dr["UserId"]),
                            Price = Convert.ToDouble(dr["Price"]),
                            SLockerID = Convert.ToInt32(dr["SLockerID"]),
                            ELockerID = Convert.ToInt32(dr["ELockerID"])
                        };
                        PackagesList.Add(P);
                    }
                }
                else
                {

                    if (endStation == 0)
                    {
                        String selectSTR = "select * FROM Packages where EndStation=" + startStation + " And status =4 And ExpressP = '" + express + "' Order By PackageID  ";

                        SqlCommand cmd = new SqlCommand(selectSTR, con);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                        while (dr.Read())
                        {   // Read till the end of the data into a row
                            Packages P = new Packages
                            {


                                PackageId = Convert.ToInt32(dr["PackageId"]),
                                StartStation = Convert.ToInt32(dr["StartStation"]),
                                EndStation = Convert.ToInt32(dr["EndStation"]),
                                Pweight = (float)dr["Pweight"],
                                ExpressP = Convert.ToBoolean(dr["ExpressP"]),
                                Status = Convert.ToInt32(dr["Status"]),
                                UserId = Convert.ToInt32(dr["UserId"]),
                                Price = Convert.ToDouble(dr["Price"]),
                                SLockerID = Convert.ToInt32(dr["SLockerID"]),
                                ELockerID = Convert.ToInt32(dr["ELockerID"])
                            };
                            PackagesList.Add(P);
                        }
                    }

                    else
                    {
                        String selectSTR = "select * FROM Packages where StartStation=" + startStation + " and EndStation=" + endStation + " and Pweight=" + Pweight + " And status =2  Order By PackageID  ";
                        SqlCommand cmd = new SqlCommand(selectSTR, con);
                       SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                        while (dr.Read())
                        {   // Read till the end of the data into a row
                            Packages P = new Packages
                            {


                                PackageId = Convert.ToInt32(dr["PackageId"]),
                                StartStation = Convert.ToInt32(dr["StartStation"]),
                                EndStation = Convert.ToInt32(dr["EndStation"]),
                                Pweight = (float)dr["Pweight"],
                                ExpressP = Convert.ToBoolean(dr["ExpressP"]),
                                Status = Convert.ToInt32(dr["Status"]),
                                UserId = Convert.ToInt32(dr["UserId"])
                            };
                            PackagesList.Add(P);
                        }
                    }

                   
                }

                return PackagesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //--------------------------------------------------------------------
        //הוספת שליח רכבת מתעניין AddTDUser
        //--------------------------------------------------------------------
        public int AddTDUser(TDUser tDUser)
        {


            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand4(tDUser);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }



        }

        private string BuildInsertCommand4(TDUser tDUser)
        {
            string command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("VALUES ('{0}','{1}','{2}','{3}',{4},{5})", tDUser.UserID, tDUser.Pweight, tDUser.Status, tDUser.StartStation, tDUser.EndStation, tDUser.Rating);
            string prefix = "INSERT INTO TDUser ([UserId],[Pweight],[Status],[StartStation],[EndStation],[Rating]) ";
            command = prefix + " " + sb.ToString() + " SELECT SCOPE_IDENTITY()";

            return command;
        }



        public TDUser GetInterestedTD(int UserId)
        {

            SqlConnection con = null;
            TDUser TDInterested = new TDUser();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM TDUser where UserID=" + UserId + " and Status = 0";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    TDUser L = new TDUser
                    {

                        DeliveryID = Convert.ToInt32(dr["DeliveryID"]),
                        UserID = Convert.ToInt32(dr["UserID"]),
                        Pweight = (float)dr["Pweight"],
                        //        PackageID = Convert.ToInt32(dr["PackageID"]),
                        Status = Convert.ToInt16(dr["Status"]),
                        Rating = (float)dr["Rating"]


                    };
                    TDInterested = (L);
                }

                return TDInterested;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }



        //    ------------------------------------------------------------------------------------------------------------------------------
        //Get The Interested Users by StationIDs ( return Users that interested in packages in the same category - weight)
        //    -------------------------------------------------------------------------------------------------------------------------------


        public List<TDUser> GetTDUserList(int startStation, int endStation, float Pweight)
        {
            SqlConnection con = null;
            List<TDUser> TDUserList = new List<TDUser>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * FROM TDUser where StartStation=" + startStation + " and EndStation=" + endStation + " and Pweight=" + Pweight + " And status = 0";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    TDUser interests = new TDUser
                    {
                        DeliveryID = Convert.ToInt32(dr["DeliveryID"]),
                        StartStation = Convert.ToInt32(dr["StartStation"]),
                        EndStation = Convert.ToInt32(dr["EndStation"]),
                        Pweight = (float)dr["Pweight"],
                        Status = Convert.ToInt16(dr["Status"]),
                        UserID = Convert.ToInt32(dr["UserID"]),
                        Rating = (float)dr["Rating"]

                    };
                    TDUserList.Add(interests);

                }


                return TDUserList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }





        public List<UserCredits> GetTDUsers(int TDUserID)
        {

            SqlConnection con = null;
            List<UserCredits> userCredits = new List<UserCredits>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select U.UserId,U.Fullname,TD.PackageID from Packages P inner join TDUser TD on P.PackageID=TD.PackageID inner join tbl_User U on TD.UserID=U.UserId  where P.UserId= " + TDUserID + "and P.Status>=1";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserCredits user = new UserCredits()
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FullName = (string)dr["Fullname"],


                    };

                    userCredits.Add(user);


                }

                return userCredits;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        //כמה יש למשתמש קרידיטים
        public List<UserCredits> GetUserCredit(int UserID)
        {

            SqlConnection con = null;
            List<UserCredits> userCredits = new List<UserCredits>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from UserCredits where UserId = " + UserID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserCredits user = new UserCredits()
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FullName = (string)dr["Fullname"],
                        Credit = Convert.ToDouble(dr["Credit"])

                    };
                    if (UserID == user.UserId)
                    {
                        userCredits.Add(user);
                    }

                }

                return userCredits;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        public int InsertUserCredits(UserCredits userCredits)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand6(userCredits);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }

        }

        private string BuildInsertCommand6(UserCredits userCredits)
        {
            string command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("VALUES ('{0}','{1}','{2}')", userCredits.UserId, userCredits.FullName, userCredits.Credit);
            string prefix = "INSERT INTO UserCredits ([UserId],[Fullname],[Credit]) ";
            command = prefix + " " + sb.ToString();

            return command;
        }


        public int UpdateUserCredits(UserCredits userCredits)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand5(userCredits);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }



        }

        private string BuildInsertCommand5(UserCredits userCredits)
        {
            string command;


            command = "UPDATE UserCredits  SET [Credit] = " + userCredits.Credit + "  where UserId=" + userCredits.UserId;


            return command;
        }

        public List<Transaction> GetTransactions(int UserID)
        {
            SqlConnection con = null;
            List<Transaction> TransactionsList = new List<Transaction>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Transactions WHERE UserID1 =" + UserID + " or UserID2=" + UserID+ " order by TransactionID desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Transaction transaction = new Transaction()
                    {
                        TranstactionID = Convert.ToInt32(dr["TransactionID"]),
                        UserID1 = Convert.ToInt32(dr["UserID1"]),
                        UserID2 = Convert.ToInt32(dr["UserID2"]),
                        CreditAmount = Convert.ToDouble(dr["CreditAmount"]),
                        TransactionDate = (DateTime)dr["TransactionDate"]

                    };
                    TransactionsList.Add(transaction);
                }

                return TransactionsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }


        //--------------------------------------------------------------------
        // Build the Insert command String ----- Sign Up
        //--------------------------------------------------------------------
        public int InsertTransaction(Transaction transaction)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand(transaction);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }

        }

        private string BuildInsertCommand(Transaction transaction)
        {
            string command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("VALUES ('{0}','{1}','{2}','{3}')", transaction.UserID1, transaction.UserID2, transaction.CreditAmount, transaction.TransactionDate);
            string prefix = "INSERT INTO Transactions ([UserID1],[UserID2],[CreditAmount],[TransactionDate]) ";
            command = prefix + sb.ToString();
            if (transaction.UserID1 == 1)
            {
                Users DeliveryToken = this.GetDeliveryToken(transaction.UserID2);
                this.SendDeliveryNotification(DeliveryToken.Token);
            }

            return command;
        }

        public List<ModuleActivity> GetModuleActivity(int UserID)
        {

            SqlConnection con = null;
            List<ModuleActivity> moduleActivity = new List<ModuleActivity>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select P.PackageID, P.Status,s1.StationName as 'StartStation',s2.StationName as 'EndStation',P.UserId from Packages P inner join Stations s1 on p.StartStation=s1.StationID inner join Stations s2 on  P.EndStation =s2.StationID  where UserId = " + UserID + "and P.StartStation= s1.StationID and P.EndStation= s2.StationID and P.Status >=1 order by P.PackageID desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    ModuleActivity M = new ModuleActivity()
                    {

                        
                        UserID1 = Convert.ToInt32(dr["UserID"]),
                        PackageID = Convert.ToInt32(dr["PackageID"]),
                        Status = Convert.ToInt32(dr["Status"]),
                        StartStation = (string)dr["StartStation"],
                        EndStation = (string)dr["EndStation"],


                    };
                    moduleActivity.Add(M);
                }

                return moduleActivity;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        public List<ModuleActivity> GetModuleActivityTD(int UserID)
        {

            SqlConnection con = null;
            List<ModuleActivity> moduleActivity = new List<ModuleActivity>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select TD.DeliveryID, TD.PackageID, TD.Status,s1.StationName as 'StartStation',s2.StationName as 'EndStation',TD.UserID from TDUser TD inner join Stations s1 on TD.StartStation=s1.StationID inner join Stations s2 on  TD.EndStation =s2.StationID  where UserId = " + UserID + " order by TD.DeliveryID desc " ;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    
                    if(!string.IsNullOrEmpty(dr["PackageID"].ToString()) ){
                        ModuleActivity M = new ModuleActivity()
                        {

                            UserID1 = Convert.ToInt32(dr["UserID"]),
                            PackageID = Convert.ToInt32(dr["PackageID"]),
                            Status = Convert.ToInt32(dr["Status"]),
                            StartStation = (string)dr["StartStation"],
                            EndStation = (string)dr["EndStation"],

                        };
                        moduleActivity.Add(M);
                    }
                    else
                    {

                        ModuleActivity M = new ModuleActivity()
                        {

                            UserID1 = Convert.ToInt32(dr["UserID"]),
                            PackageID = null,
                            Status = Convert.ToInt32(dr["Status"]),
                            StartStation = (string)dr["StartStation"],
                            EndStation = (string)dr["EndStation"],

                        };
                        moduleActivity.Add(M);
                    }


                }

                return moduleActivity;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        //--------------------------------------------------------------------
        //החזרת לוקר שמשוריין לחבילה מסויימת 
        //--------------------------------------------------------------------
        public List<Lockers> GetLocker(int PackageID)
        {
            SqlConnection con = null;
            List<Lockers> StationList = new List<Lockers>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Lockers WHERE Busy = 1 and PackageID =" + PackageID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Lockers L = new Lockers
                    {
                        LockerID = Convert.ToInt32(dr["LockerID"]),
                        Busy = (bool)dr["Busy"],
                        StationID = Convert.ToInt32(dr["StationID"]),

                    };
                    StationList.Add(L);
                }

                return StationList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }


        // עדכון סטטוס מתעניין אחרי הפקדה של אחד המתעניינים בחבילה או ל (-1) או ל 1
    
        public int UpdateTDUserPack(TDUser tDUser)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand5(tDUser);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }



        }

        private string BuildInsertCommand5(TDUser tDUser)
        {
            string command;

            if (tDUser.Status != -1)
                if(tDUser.Status == 1)
                command = "UPDATE TDUser  SET [PackageID] = " + tDUser.PackageID + " , [Status] = " + tDUser.Status + ", [PickUpDT] = '" +tDUser.PickUpDT.ToString()+ "' where DeliveryID=" + tDUser.DeliveryID;
                else
                command = "UPDATE TDUser  SET [PackageID] = " + tDUser.PackageID + " , [Status] = " + tDUser.Status + " where DeliveryID=" + tDUser.DeliveryID;
            else
                command = "UPDATE TDUser  SET [Status] = " + tDUser.Status + "  where StartStation=" + tDUser.StartStation + " and EndStation=" + tDUser.EndStation + " and Pweight=" + tDUser.Pweight + " and Status= 0";

            return command;
        }



        public int GetPossiblePickup(int StartStation, int EndStation, int UserId)
        {

            SqlConnection con = null;
            List<TDUser> PossiblePickup = new List<TDUser>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * FROM TDUser where StartStation=" + StartStation + " and EndStation=" + EndStation + " and UserID=" + UserId + " And status =0 ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    TDUser P = new TDUser
                    {
                        DeliveryID = Convert.ToInt32(dr["DeliveryID"])


                    };
                    PossiblePickup.Add(P);
                }

                if (PossiblePickup.Count < 1)
                {
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }

        public DBServices RatingAlgorithm(TDUser Category)
        {

            SqlConnection con = null;

            try
            {
                // connect
                con = Connect("DBConnectionString");

                // create a dataadaptor




                da = new SqlDataAdapter("select *  from TDUser td where Status != 0", con);

                // automatic build the commands
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                // create a DataSet
                DataSet ds = new DataSet();


                // Fill the Dataset
                da.Fill(ds);

                // keep the table in a field
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                // write to log
            }

            finally
            {
                if (con != null)
                    con.Close();
            }

            return this;


        }

        public void UpdateRating()
        {
            da.Update(dt);
        }

        //  החזרת דירוג מעודכן לצורך
        public List<TDUser> GetUpdatedRating(int UserId)
        {
            SqlConnection con = null;
            List<TDUser> RatingObject = new List<TDUser>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select Top 1 * from TDUser where UserId =" + UserId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    TDUser L = new TDUser
                    {
                        Rating = (float)dr["Rating"],
                        UserID = Convert.ToInt32(dr["UserID"])


                    };
                    RatingObject.Add(L);
                }

                return RatingObject;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Stations> GetStationCoords(int stationID)
        {

            SqlConnection con = null;
            List<Stations> StationList = new List<Stations>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Stations where StationID=" + stationID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Stations R = new Stations
                    {
                        StationID = Convert.ToInt32(dr["StationID"]),
                        StationName = (string)dr["StationName"],
                        Latitude = Convert.ToDouble(dr["Latitude"]),
                        Longitude = Convert.ToDouble(dr["Longitude"])
                    };
                    StationList.Add(R);
                }
                return StationList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int UpdatePrice(Packages packages)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand4(packages);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }
        }

        private string BuildInsertCommand4(Packages packages)
        {
            string command;

            command = "UPDATE Packages  SET [Price] = '" + packages.Price + "'  where PackageID=" + packages.PackageId;

            return command;
        }

        public List<Packages> GetPrice(int PackageId)
        {

            SqlConnection con = null;
            List<Packages> PackPrice = new List<Packages>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Packages Where PackageID= " + PackageId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Packages P = new Packages
                    {
                        PackageId = Convert.ToInt32(dr["PackageId"]),
                        StartStation = Convert.ToInt32(dr["StartStation"]),
                        EndStation = Convert.ToInt32(dr["EndStation"]),
                        Pweight = (float)dr["Pweight"],
                        ExpressP = Convert.ToBoolean(dr["ExpressP"]),
                        Status = Convert.ToInt32(dr["Status"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        Price = Convert.ToDouble(dr["Price"]),
                        SLockerID = Convert.ToInt32(dr["SLockerID"]),
                        ELockerID = Convert.ToInt32(dr["ELockerID"])
                    };
                    PackPrice.Add(P);
                }

                return PackPrice;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int UpdateLocker(Packages packages)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand5(packages);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }
        }
        private string BuildInsertCommand5(Packages packages)
        {
            string command;

            command = "UPDATE Packages  SET [SLockerID] = '" + packages.SLockerID + "' , [ELockerID] = '" + packages.ELockerID + "' where PackageID=" + packages.PackageId;

            return command;
        }

        public List<TDUser> GetDeliveryId(int UserId)
        {
            SqlConnection con = null;
            List<TDUser> TDDelivery = new List<TDUser>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from TDUser where UserId =" + UserId + " and DeliveryID>=All(select DeliveryID from TDUser)";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    TDUser TD = new TDUser
                    {
                        Rating = (float)dr["Rating"],
                        UserID = Convert.ToInt32(dr["UserID"]),
                        DeliveryID = Convert.ToInt32(dr["DeliveryID"])
                    };
                    TDDelivery.Add(TD);
                }

                return TDDelivery;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }




        }
        //קבלת איידי של השולח--------------------------
        public Packages GetSenderId(int packageId)
        {
            SqlConnection con = null;
            Packages UserId = new Packages();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT UserId  FROM Packages Where PackageID= " + packageId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Packages R = new Packages
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),


                    };
                    UserId = R;
                }
                return UserId;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        //קבלת טוקן של השולח--------------------------
        public Users GetSenderToken(int UserId, int PackageID)
        {
            SqlConnection con = null;
            Users UserToken = new Users();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "Select u.token From Packages p inner join tbl_User u on p.UserId = u.UserId where p.PackageID=" + PackageID + " and p.UserId=" + UserId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Users T = new Users()
                    {
                        Token = (string)dr["token"],


                    };
                    UserToken = T;
                }
                return UserToken;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        // משתמש מעדכן את הטוקן שלו push notifications-------------------------------------
        public int UpdateUserToken(int userId, string token)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand5(userId, token);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }
        }

        private string BuildInsertCommand5(int userId, string token)
        {
            string command;

            command = "UPDATE tbl_User  SET [token] = '" + token + "'  where UserId=" + userId;

            return command;
        }

        
  public Customers GetCustDetails(int PackageId)
        {
            SqlConnection con = null;
            Customers CustDetails = new Customers();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT *  FROM CustomersPackages Where PackageId= " + PackageId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Customers R = new Customers
                    {
                        FullName=(string)dr["FullName"],
                        Address=(string)dr["Address"],
                        PhoneNum= Convert.ToInt32(dr["PhoneNum"]),
                        PackageId= Convert.ToInt32(dr["PackageId"]),
                        CustomerID= Convert.ToInt32(dr["CustomerID"]),
                        Latitude = (dr["Longitude"] != DBNull.Value) ? Convert.ToDouble(dr["Latitude"]) : 0,
                        Longitude = (dr["Longitude"] != DBNull.Value) ? Convert.ToDouble(dr["Longitude"]) : 0

                    };
                    CustDetails = R;
                }
                return CustDetails;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        

            public int PostUser(ExpressUser UserDetails)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            string cStr = BuildInsertCommandEx(UserDetails);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }

        }

     

        private string BuildInsertCommandEx(ExpressUser UserDetails)
        {

           
                string command;

                StringBuilder sb = new StringBuilder();
                // use a string builder to create the dynamic string
                sb.AppendFormat("Values ('{0}','{1}','{2}','{3}','{4}')", UserDetails.UserID, UserDetails.FullName, UserDetails.PackageID , UserDetails.Status,UserDetails.PackTime);
                string prefix = "INSERT INTO ExpressUser ([UserID],[FullName],[PackageID],[Status],[PackTime])";

                command = prefix + " " + sb.ToString() + " SELECT SCOPE_IDENTITY()";
                return command;
            
           
        }

        public List<ModuleActivity> GetModuleActivityEx(int UserID)
        {

            SqlConnection con = null;
            List<ModuleActivity> moduleActivity = new List<ModuleActivity>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select Ex.UserID , Ex.FullName , Ex.PackageID ,S.StationName , P.ELockerID , Ex.Status , CP.Address from ExpressUser Ex  inner join Packages P on Ex.PackageID =P.PackageID inner join Stations S on P.EndStation = S.StationID inner join CustomersPackages CP on Ex.PackageID = CP.PackageId where Ex.UserID =" + UserID +" order by PackageID desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    if (!string.IsNullOrEmpty(dr["PackageID"].ToString()))
                    {
                        ModuleActivity M = new ModuleActivity()
                        {

                            UserID1 = Convert.ToInt32(dr["UserID"]),
                            PackageID = Convert.ToInt32(dr["PackageID"]),
                            Status = Convert.ToInt32(dr["Status"]),
                            StartStation = (string)dr["StationName"],
                            EndStation = (string)dr["Address"],
                            UserID2 = Convert.ToInt32(dr["ELockerID"])


                        };
                        moduleActivity.Add(M);
                    }
                    else
                    {

                        ModuleActivity M = new ModuleActivity()
                        {

                            UserID1 = Convert.ToInt32(dr["UserID"]),
                            PackageID = null,
                            Status = Convert.ToInt32(dr["Status"]),
                            StartStation = (string)dr["StationName"],
                            EndStation = (string)dr["Address"],
                            UserID2 = Convert.ToInt32(dr["ELockerID"])
                        };
                        moduleActivity.Add(M);
                    }


                }

                return moduleActivity;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }
        //--------------------------------------








        public DBServices GetDate(int StartStation, int EndStation, int UserId, DateTime PickUpDT)
        {
            SqlConnection con = null;
            try
            {
                // connect
                con = Connect("DBConnectionString");

                // create a dataadaptor
                da = new SqlDataAdapter("select top 4 * from TDUser td where StartStation=" + StartStation + "and EndStation=" + EndStation + "and UserID=" + UserId + " order by DeliveryID desc", con);

                // automatic build the commands
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                // create a DataSet
                DataSet ds = new DataSet();

                // Fill the Dataset
                da.Fill(ds);

                // keep the table in a field
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                // write to log
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return this;
        }

        //public void UpdateDate()
        //{
        //    da.Update(dt);
        //}

        public int UpdateExPackage(ExpressUser ExPackage)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand6(ExPackage);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }



        }

        private string BuildInsertCommand6(ExpressUser ExPackage)
        {
            string command;

            if(ExPackage.Status == -1)
                command = "Delete From ExpressUser where PackTime ='"+ ExPackage.PackTime +"' and PackageID=" + ExPackage.PackageID;
            else
            {
                command = "UPDATE ExpressUser  SET [Status] = " + ExPackage.Status + "  where PackageID=" + ExPackage.PackageID;
                if (ExPackage.Status == 2)
                    command += "UPDATE Packages  SET [Status] = 6 where PackageID=" + ExPackage.PackageID;


            }

            return command;
        }



        public List<Packages> GetPackagesByStations(int startStation, int endStation)
        {
            SqlConnection con = null;
            List<Packages> PackagesList = new List<Packages>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file


                String selectSTR = "select * FROM Packages where StartStation=" + startStation + " and EndStation=" + endStation + "  And status =2 ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Packages P = new Packages
                    {
                        PackageId = Convert.ToInt32(dr["PackageId"]),
                        StartStation = Convert.ToInt32(dr["StartStation"]),
                        EndStation = Convert.ToInt32(dr["EndStation"]),
                        Pweight = (float)dr["Pweight"],
                        ExpressP = Convert.ToBoolean(dr["ExpressP"]),
                        Status = Convert.ToInt32(dr["Status"]),
                        UserId = Convert.ToInt32(dr["UserId"])
                    };
                    PackagesList.Add(P);
                }
                return PackagesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //---------------------------
        //קבלת רשימת הלקוחות עבור שליח אקספרס לפי תחנה

        public List<Customers> GetExpressCustomerList(int UserID, double Elat, double Elng, int Station)
        {

            SqlConnection con = null;
            List<Customers> CustomersList = new List<Customers>();
            List<Customers> Path = new List<Customers>();
            //double Elat = 32.58651;
            //double Elng = 34.95332;
            Customers express = new Customers(0, "station", "userLocation", 111, 1, Elat, Elng);
            Path.Add(express);

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select C.* From CustomersPackages C inner join ExpressUser E on C.PackageId = E.PackageID inner join Packages P on E.PackageID=P.PackageID where E.Status=2  and E.UserID=" + UserID + " and P.EndStation=" + Station;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Customers C = new Customers
                    {
                        CustomerID = Convert.ToInt32(dr["CustomerID"]),
                        FullName = (string)dr["FullName"],
                        Address = (string)dr["Address"],
                        PhoneNum = Convert.ToInt32(dr["PhoneNum"]),
                        PackageId = Convert.ToInt32(dr["PackageId"]),
                        Latitude = (dr["Longitude"] != DBNull.Value) ? Convert.ToDouble(dr["Latitude"]) : Elat,
                        Longitude = (dr["Longitude"] != DBNull.Value) ? Convert.ToDouble(dr["Longitude"]) : Elng


                    };
                    CustomersList.Add(C);
                }

                //CustomersList[0].Latitude= HaversineDistanceInKM(CustomersList[0].Latitude, CustomersList[0].Longitude, CustomersList[0 + 1].Latitude, CustomersList[0 + 1].Longitude);
                for (int i = 0; i < CustomersList.Count; i++)
                {
                    int minPoint = i;
                    double minDist = 300;
                    for (int j = 0; j < CustomersList.Count; j++)
                    {
                        if (checkVisited(j, CustomersList, Path)) { continue; }
                        double dist = HaversineDistanceInKM(Path[Path.Count - 1].Latitude, Path[Path.Count - 1].Longitude, CustomersList[j].Latitude, CustomersList[j].Longitude);
                        if (dist < minDist)
                        {
                            minPoint = j;
                            minDist = dist;
                        }
                    }

                    Path.Add(CustomersList[minPoint]);
                }
                Path.Remove(Path[0]);
                return Path;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //Compute distance
        public double HaversineDistanceInKM(double prevLat, double prevLng, double CurrentLat, double CurrentLng)
        {
            double prevLatInRad = this.ConvertToRadians(prevLat);
            double prevLngInRad = this.ConvertToRadians(prevLng);
            double CurrentLatInRad = this.ConvertToRadians(CurrentLat);
            double CurrentLngInRad = this.ConvertToRadians(CurrentLng);
            double R = 6377.830272;
            var lat = ConvertToRadians(CurrentLat - prevLat);
            var lng = ConvertToRadians(CurrentLng - prevLng);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(prevLatInRad) * Math.Cos(CurrentLatInRad) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }
        //ToRadians
        public double ConvertToRadians(double angle)
        {
            return (angle * Math.PI / 180);
        }
        //checkifVisited
        public bool checkVisited(int i, List<Customers> customersList, List<Customers> path)
        {
            foreach (Customers j in path)
            {
                if (customersList[i] == j)
                {
                    return true;
                }

            }
            return false;
        }


        public int UpdateDT(Users users)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand3(users);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }
        }


        private string BuildInsertCommand3(Users users)
        {
            string command;

            command = "UPDATE tbl_User  SET [DTtoken] = '" + users.DTtoken + "'  where UserId=" + users.UserId;

            return command;
        }


        public List<Users> GetUsers()
        {
            SqlConnection con = null;
            List<Users> UserList = new List<Users>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT * FROM tbl_User where dttoken is not null";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Users user = new Users
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FullName = (string)dr["Fullname"],
                        PhoneNum = (string)dr["PhoneNum"],
                        EmailAddress = (string)dr["EmailAddress"],
                        Password = (string)dr["Password"],
                        ProfilePic = (string)dr["ProfilePic"],
                        BirthDate = (DateTime)dr["BirthDate"],
                        Token = (string)dr["token"],
                        DTtoken = (DateTime)dr["DTtoken"]
                        //DTtoken = dr["DTtoken"] == null? (DateTime?)null: DateTime.Parse(dr["DTtoken"].ToString())
                    };
                    UserList.Add(user);
                }
                return UserList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private static readonly HttpClient client = new HttpClient();
        public async void GetToken(string Token)
        {

            var values = new Dictionary<string, string>
            {
                { "to", Token },
                 { "sound", "default" },
                 { "title", "? נוסעים היום" },
                 { "body","מהרו וחסכו כרטיס נסיעה, החבילה מחכה לך בתחנה"}
             };

            var content = new FormUrlEncodedContent(values);


            var response = await client.PostAsync("https://exp.host/--/api/v2/push/send", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }

        public List<Packages> GetCanceledPackages()
        {
            SqlConnection con = null;
            List<Packages> PackagesList = new List<Packages>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT PackageID , SLockerID , ELockerID FROM Packages where PackTime <= '"+ DateTime.Now +"' and status = 1"  ;
               // string s = "SELECT PackageID , SLockerID , ELockerID FROM Packages where PackTime <= @packtime and status = @status";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
              //  SqlCommand cmd = new SqlCommand();
                //cmd.Connection = con;
                //cmd.CommandText = s;
                //cmd.Parameters.AddWithValue("@packtime", DateTime.Now);
                //cmd.Parameters.AddWithValue("@status", 1);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                  

                        Packages packages = new Packages
                        {
                            PackageId = Convert.ToInt32(dr["PackageID"]),
                            SLockerID = Convert.ToInt32(dr["SLockerID"]),
                            ELockerID = Convert.ToInt32(dr["ELockerID"]),
                        };
                        PackagesList.Add(packages);
                   
                }
                return PackagesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


        }

        public List<ExpressUser> GetCanceledEx()
        {
            SqlConnection con = null;
            List<ExpressUser> PackagesList = new List<ExpressUser>();

            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT Ex.PackageID , Ex.PackTime , P.SLockerID , P.ELockerID  FROM ExpressUser Ex inner join Packages P on Ex.PackageID = P.PackageID where Ex.PackTime <= '"+ DateTime.Now+"' and Ex.Status = 1";
                // string s = "SELECT PackageID , SLockerID , ELockerID FROM Packages where PackTime <= @packtime and status = @status";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                //  SqlCommand cmd = new SqlCommand();
                //cmd.Connection = con;
                //cmd.CommandText = s;
                //cmd.Parameters.AddWithValue("@packtime", DateTime.Now);
                //cmd.Parameters.AddWithValue("@status", 1);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row


                    ExpressUser packages = new ExpressUser
                    {
                        PackageID = Convert.ToInt32(dr["PackageID"]),
                        UserID = Convert.ToInt32(dr["SLockerID"]),
                        Status = Convert.ToInt32(dr["ELockerID"]),
                        PackTime = (DateTime)dr["PackTime"]
                };
                    PackagesList.Add(packages);

                }
                return PackagesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


        }
        //-----------------------------------------------------------------------------------------------------------------------------------------
        //Update ExpressPackage Status   (Delivered)
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public int UpdateStatusDelivered(int PackageId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cStr = BuildInsertCommand4(PackageId);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                Packages senderId = this.GetSenderId(PackageId);
                Users token = this.GetSenderToken(senderId.UserId, PackageId);
                this.SendExpressNotification(token.Token, PackageId);

                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    //close the db connection
                    con.Close();
                }
            }



        }

        private string BuildInsertCommand4(int PackageId)
        {
            string command, command2;


            command = "UPDATE Packages  SET [Status] =7 where PackageID=" + PackageId;
            command2 = "UPDATE ExpressUser  SET [Status] =3 where PackageID=" + PackageId;


            return command + command2;
        }
        //קבלת טוקן של השליח--------------------------
        public Users GetDeliveryToken(int UserId)
        {
            SqlConnection con = null;
            Users UserToken = new Users();
            var token = "";
            try
            {
                con = Connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "Select u.token From tbl_User u where u.UserId=" + UserId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Users T = new Users()
                    {
                        Token = (string)dr["token"],


                    };
                    UserToken = T;
                    token = UserToken.Token;
                }
                //  Token(token);
                return UserToken;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }
        //פוש נוטיפיקיישן על מסירת משלוח אקספרס
        private static readonly HttpClient client2 = new HttpClient();
        async void SendExpressNotification(string Token, int PackageId)
        {

            var values = new Dictionary<string, string>
            {
                { "to", Token },
                 { "sound", "default" },
                 { "title", "משלוח אקספרס מספר "+PackageId+ " נמסר "},
                 { "body","שליח אקספרס מסר את החבילה ללקוח" }
             };

            var content = new FormUrlEncodedContent(values);


            var response = await client2.PostAsync("https://exp.host/--/api/v2/push/send", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }


        //---------------------------
        //פוש נוטיפיקיישן לשליח על השלמת משלוח
        private static readonly HttpClient client1 = new HttpClient();
        async void SendDeliveryNotification(string Token)
        {

            var values = new Dictionary<string, string>
            {
                { "to", Token },
                 { "sound", "default" },
                 { "title", "!עוד ג'סטה הסתיימה "},
                 { "body","קיבלת תשלום עבור משלוח שביצעת" }
             };

            var content = new FormUrlEncodedContent(values);


            var response = await client1.PostAsync("https://exp.host/--/api/v2/push/send", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }


        //---------------------------

    }
}