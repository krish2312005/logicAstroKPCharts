using System;
using System.Collections;
using System.Collections.Generic;

namespace srlWebCom.Astro.AstroObjects
{
    public class PlanetData
    {
        public string Name = "";
        public string SignLord = "";
        public string StarLord = "";
        public string SubLord = "";
        public string SSLord = "";
        public string Strength = "";
        public int PlanetsInStars = 0;
    }

    public class CuspData
    {
        public int HouseNo = 0;
        public string SignLord = "";
        public string StarLord = "";
        public string SubLord = "";
        public string SSLord = "";
        public string SubStrength = "";
    }

    public class HouseSignificationData
    {
        public string StarWise = "";
        public string StarLord = "";
        public string Planet = "";
        public string SubLord = "";
        public string SubWise = "";
    }

    public class DasaData
    {
        public string DasaLord = "";
        public string BukthiLord = "";
        public string StartDate = "";
        public string EndDate = "";
    }

    public class AstronomyData
    {
        public string Planet = "";
        public string Sign = "";
        public string SLongitude = "";
        public string ZLongitude = "";
        public string ZLatitude = "";
        public string Declination = "";
    }

    public class AspectData
    {
        public string Planet = "";
        public int[] Degrees = new int[9];
    }

    public class AstroChartData
    {
        public string Name = "";
        public string Sex = "";
        public string DateTimeOfBirth = "";
        public string PlaceOfBirth = "";
        public string Longitude = "";
        public string Latitude = "";
        public string Ayanamsa = "";
        public string SiderealTime = "";
        public string MoonStarInfo = "";
        public string DasaBalance = "";

        public string[] RasiDataArray = new string[12];

        public List<PlanetData> PlanetList = new List<PlanetData>();
        public List<CuspData> CuspList = new List<CuspData>();
        public List<HouseSignificationData> HouseSignificationList = new List<HouseSignificationData>();
        public List<DasaData> DasaList = new List<DasaData>();
        public List<AstronomyData> AstronomyList = new List<AstronomyData>();
        public List<AspectData> AspectList = new List<AspectData>();

        public string[] PlanetTable = { "SU", "MO", "MA", "RA", "JU", "SA", "ME", "KE", "VE" };
    }
}
