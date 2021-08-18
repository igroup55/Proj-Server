using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jestapp_project.Models.DAL;

namespace jestapp_project.Models
{
    public class Stations
    {
        int stationID;
        string stationName;
        double latitude;
        double longitude;

        public Stations()
        {
        }

        public Stations(int stationID, string stationName, double latitude, double longitude)
        {
            StationID = stationID;
            StationName = stationName;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int StationID { get => stationID; set => stationID = value; }
        public string StationName { get => stationName; set => stationName = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }

        public List<Stations> GetStations()
        {
            DBServices dbs = new DBServices();
            List<Stations> StationList = dbs.GetAllStations();
            return StationList;
        }

        public List<Stations> GetStationCoords(int stationID)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStationCoords(stationID);
        }

    }
}