using System;
using System.Collections.Generic;
using System.Text;

namespace srlWebCom.Astro.AstroObjects
{
    public class SPKhullarCoordinate
    {
        public string PlanetName = "";
        public bool HasPositionalStatus = false;
        public string PositionalStatusHouses = "";
        public string StellarStatusHouses = "";
        public string PlanetResult = "";
        public string StarLordResult = "";
        public string SubLordResult = "";
    }

    public static class KhullarCalculationService
    {
        private static readonly string[] NormalPlanets = new string[]
            { "SU", "MO", "MA", "ME", "VE", "JU", "SA" };

        private static readonly string[] NodePlanets = new string[]
            { "RA", "KE" };

        private static readonly Dictionary<string, string> PlanetFullNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "SU", "SUN" }, { "MO", "MOON" }, { "MA", "MARS" }, { "SA", "SATURN" },
            { "ME", "MERCURY" }, { "KE", "KETU" }, { "VE", "VENUS" }, { "JU", "JUPITER" }, { "RA", "RAHU" }
        };

        public static bool IsNode(string strPlanet)
        {
            if (string.IsNullOrEmpty(strPlanet)) return false;
            string upper = strPlanet.Trim().ToUpperInvariant();
            return upper == "RA" || upper == "KE";
        }

        public static bool IsNormalPlanet(string strPlanet)
        {
            if (string.IsNullOrEmpty(strPlanet)) return false;
            string upper = strPlanet.Trim().ToUpperInvariant();
            for (int i = 0; i < NormalPlanets.Length; i++)
            {
                if (NormalPlanets[i] == upper) return true;
            }
            return false;
        }

        public static string GetNodeSignLord(string strNode, Dictionary<string, HouseSignificationData> significationLookup)
        {
            if (string.IsNullOrEmpty(strNode) || significationLookup == null) return "";
            HouseSignificationData hsd;
            if (significationLookup.TryGetValue(strNode.Trim(), out hsd))
            {
                return hsd.D7;
            }
            return "";
        }

        public static string GetNodeStarLord(string strNode, Dictionary<string, PlanetData> planetLookup)
        {
            if (string.IsNullOrEmpty(strNode) || planetLookup == null) return "";
            PlanetData pd;
            if (planetLookup.TryGetValue(strNode.Trim(), out pd))
            {
                return pd.StarLord;
            }
            return "";
        }

        public static string ResolveStarLordHouses(string starLordName,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            if (string.IsNullOrEmpty(starLordName) || significationLookup == null)
                return "";

            HouseSignificationData hsd;
            if (!significationLookup.TryGetValue(starLordName.Trim(), out hsd))
                return "";

            SortedSet<int> houses = new SortedSet<int>();
            CollectHouses(hsd.D1, houses);
            CollectHouses(hsd.D2, houses);

            StringBuilder result = new StringBuilder();
            foreach (int house in houses)
            {
                if (result.Length > 0) result.Append(',');
                result.Append(house);
            }
            return result.ToString();
        }

        public static string ResolveSubLordHouses(string subLordName,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            if (string.IsNullOrEmpty(subLordName) || significationLookup == null)
                return "";

            HouseSignificationData hsd;
            if (!significationLookup.TryGetValue(subLordName.Trim(), out hsd))
                return "";

            SortedSet<int> houses = new SortedSet<int>();
            CollectHouses(hsd.D5, houses);
            if (IsNode(subLordName))
            {
                CollectHouses(hsd.D7, houses);
                CollectHouses(hsd.D8, houses);
            }
            else
            {
                CollectHouses(hsd.D6, houses);
            }

            StringBuilder result = new StringBuilder();
            foreach (int house in houses)
            {
                if (result.Length > 0) result.Append(',');
                result.Append(house);
            }
            return result.ToString();
        }

        public static string ResolveNodePlanetHouses(string strNode,
            string occupiedHouse, string cuspalSignLord,
            string cuspalStarLord, string cuspalSubLord, string cuspalSSLord)
        {
            SortedSet<int> houses = new SortedSet<int>();
            CollectHouses(occupiedHouse, houses);
            CollectHouses(cuspalSignLord, houses);
            CollectHouses(cuspalStarLord, houses);
            CollectHouses(cuspalSubLord, houses);
            CollectHouses(cuspalSSLord, houses);

            StringBuilder result = new StringBuilder();
            foreach (int house in houses)
            {
                if (result.Length > 0) result.Append(',');
                result.Append(house);
            }
            return result.ToString();
        }

        public static string ResolveNodeSignLordHouses(string signLordName,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            if (string.IsNullOrEmpty(signLordName) || significationLookup == null)
                return "";

            HouseSignificationData hsd;
            if (!significationLookup.TryGetValue(signLordName.Trim(), out hsd))
                return "";

            SortedSet<int> houses = new SortedSet<int>();
            CollectHouses(hsd.D7, houses);
            CollectHouses(hsd.D8, houses);

            StringBuilder result = new StringBuilder();
            foreach (int house in houses)
            {
                if (result.Length > 0) result.Append(',');
                result.Append(house);
            }
            return result.ToString();
        }

        public static string ResolveNodeStarLordHouses(string starLordOfNode,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            if (string.IsNullOrEmpty(starLordOfNode) || significationLookup == null)
                return "";

            return ResolveStarLordHouses(starLordOfNode, significationLookup);
        }

        public static bool HasMutualExchange(string planetA, string starLordOfA,
            string planetB, string starLordOfB)
        {
            if (string.IsNullOrEmpty(planetA) || string.IsNullOrEmpty(starLordOfA) ||
                string.IsNullOrEmpty(planetB) || string.IsNullOrEmpty(starLordOfB))
                return false;

            return string.Equals(planetA.Trim(), starLordOfB.Trim(), StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(planetB.Trim(), starLordOfA.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool DeterminePositionalStatus(string planetName, string starLordOfPlanet,
            List<PlanetData> planetList)
        {
            if (string.IsNullOrEmpty(planetName) || planetList == null) return false;

            string upperPlanet = planetName.Trim().ToUpperInvariant();
            string upperStarLord = string.IsNullOrEmpty(starLordOfPlanet) ? "" : starLordOfPlanet.Trim().ToUpperInvariant();

            // (b) Own star
            if (upperStarLord == upperPlanet) return true;

            // Check if any planet has PS from own star or mutual exchange (global condition)
            bool anyPlanetHasOwnStarOrExchangePS = HasAnyPlanetWithOwnStarOrExchangePS(planetList);

            // (a) Check if star is untenanted (no other planet has same StarLord)
            bool starIsTenanted = false;
            for (int i = 0; i < planetList.Count; i++)
            {
                PlanetData pd = planetList[i];
                if (pd == null) continue;
                string name = (pd.Name ?? "").Trim().ToUpperInvariant();
                if (name.Length == 0 || name == upperPlanet) continue;

                string sl = (pd.StarLord ?? "").Trim().ToUpperInvariant();
                if (sl == upperStarLord && !string.IsNullOrEmpty(upperStarLord))
                {
                    starIsTenanted = true;
                    break;
                }
            }

            // Unoccupied star gives PS only if no planet has PS from own star or mutual exchange
            if (!starIsTenanted && !anyPlanetHasOwnStarOrExchangePS) return true;

            // (c) Mutual exchange
            for (int i = 0; i < planetList.Count; i++)
            {
                PlanetData pdA = planetList[i];
                if (pdA == null) continue;
                string nameA = (pdA.Name ?? "").Trim().ToUpperInvariant();
                if (nameA.Length == 0) continue;

                string slA = (pdA.StarLord ?? "").Trim().ToUpperInvariant();
                if (slA.Length == 0) continue;

                for (int j = i + 1; j < planetList.Count; j++)
                {
                    PlanetData pdB = planetList[j];
                    if (pdB == null) continue;
                    string nameB = (pdB.Name ?? "").Trim().ToUpperInvariant();
                    if (nameB.Length == 0) continue;

                    string slB = (pdB.StarLord ?? "").Trim().ToUpperInvariant();
                    if (slB.Length == 0) continue;

                    if (HasMutualExchange(nameA, slA, nameB, slB))
                    {
                        if (nameA == upperPlanet || nameB == upperPlanet)
                            return true;
                    }
                }
            }

            // (d) Node representation
            if (IsNode(upperPlanet))
            {
                for (int i = 0; i < planetList.Count; i++)
                {
                    PlanetData pd = planetList[i];
                    if (pd == null) continue;
                    string name = (pd.Name ?? "").Trim().ToUpperInvariant();
                    if (name.Length == 0 || name == upperPlanet) continue;

                    string sl = (pd.StarLord ?? "").Trim().ToUpperInvariant();
                    if (sl != upperPlanet) continue;

                    string nodeSignLord = upperPlanet;
                    string nodeStarLord = starLordOfPlanet ?? "";
                    if (string.Equals(name, nodeSignLord, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(name, nodeStarLord, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool HasAnyPlanetWithOwnStarOrExchangePS(List<PlanetData> planetList)
        {
            if (planetList == null) return false;

            // Check own star
            for (int i = 0; i < planetList.Count; i++)
            {
                PlanetData pd = planetList[i];
                if (pd == null) continue;
                string name = (pd.Name ?? "").Trim().ToUpperInvariant();
                string sl = (pd.StarLord ?? "").Trim().ToUpperInvariant();
                if (name.Length > 0 && sl == name) return true;
            }

            // Check mutual exchange
            for (int i = 0; i < planetList.Count; i++)
            {
                PlanetData pdA = planetList[i];
                if (pdA == null) continue;
                string nameA = (pdA.Name ?? "").Trim().ToUpperInvariant();
                string slA = (pdA.StarLord ?? "").Trim().ToUpperInvariant();
                if (nameA.Length == 0 || slA.Length == 0) continue;

                for (int j = i + 1; j < planetList.Count; j++)
                {
                    PlanetData pdB = planetList[j];
                    if (pdB == null) continue;
                    string nameB = (pdB.Name ?? "").Trim().ToUpperInvariant();
                    string slB = (pdB.StarLord ?? "").Trim().ToUpperInvariant();
                    if (nameB.Length == 0 || slB.Length == 0) continue;

                    if (HasMutualExchange(nameA, slA, nameB, slB))
                        return true;
                }
            }

            return false;
        }

        public static SPKhullarCoordinate ResolvePlanetCoordinate(
            PlanetData planetData,
            HouseSignificationData hsd,
            List<PlanetData> planetList,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            SPKhullarCoordinate coord = new SPKhullarCoordinate();
            if (planetData == null || hsd == null) return coord;

            coord.PlanetName = planetData.Name ?? "";

            bool isNodeFlag = IsNode(coord.PlanetName);

            string starLordName = planetData.StarLord ?? "";
            string subLordName = planetData.SubLord ?? "";

            bool hasPS = DeterminePositionalStatus(coord.PlanetName, starLordName, planetList);
            coord.HasPositionalStatus = hasPS;

            if (hasPS)
            {
                SortedSet<int> psHouses = new SortedSet<int>();
                if (isNodeFlag)
                {
                    CollectHouses(hsd.D3, psHouses);
                    CollectHouses(hsd.D8, psHouses);
                }
                else
                {
                    CollectHouses(hsd.D1, psHouses);
                    CollectHouses(hsd.D2, psHouses);
                    CollectHouses(hsd.D3, psHouses);
                    CollectHouses(hsd.D4, psHouses);
                }
                CollectCuspalLordshipHouses(hsd.CuspalSignLord, psHouses);
                CollectCuspalLordshipHouses(hsd.CuspalStarLord, psHouses);
                CollectCuspalLordshipHouses(hsd.CuspalSubLord, psHouses);
                CollectCuspalLordshipHouses(hsd.CuspalSSLord, psHouses);
                coord.PositionalStatusHouses = SetToString(psHouses);
            }
            else
            {
                coord.PositionalStatusHouses = "";
            }

            // Stellar Status: use planet's D1/D2 (StarLord's houses from planet's perspective)
            string stellarHouses = ResolveStarLordHousesFromPlanet(hsd);
            coord.StellarStatusHouses = stellarHouses;

            SortedSet<int> planetResultHouses = new SortedSet<int>();
            CollectHouses(coord.PositionalStatusHouses, planetResultHouses);
            CollectHouses(coord.StellarStatusHouses, planetResultHouses);
            coord.PlanetResult = SetToString(planetResultHouses);

            // StarLord Result: use planet's D1/D2 (same as Stellar for normal planets)
            coord.StarLordResult = ResolveStarLordHousesFromPlanet(hsd);

            // SubLord Result: use planet's D5/D6 (SubLord's houses from planet's perspective)
            coord.SubLordResult = ResolveSubLordHousesFromPlanet(hsd, isNodeFlag);

            return coord;
        }

        public static string ResolveStarLordHousesFromPlanet(HouseSignificationData hsd)
        {
            if (hsd == null) return "";
            SortedSet<int> houses = new SortedSet<int>();
            CollectHouses(hsd.D1, houses);
            CollectHouses(hsd.D2, houses);
            StringBuilder result = new StringBuilder();
            foreach (int house in houses)
            {
                if (result.Length > 0) result.Append(',');
                result.Append(house);
            }
            return result.ToString();
        }

        public static string ResolveSubLordHousesFromPlanet(HouseSignificationData hsd, bool isNode)
        {
            if (hsd == null) return "";
            SortedSet<int> houses = new SortedSet<int>();
            CollectHouses(hsd.D5, houses);
            if (isNode)
            {
                CollectHouses(hsd.D7, houses);
                CollectHouses(hsd.D8, houses);
            }
            else
            {
                CollectHouses(hsd.D6, houses);
            }
            StringBuilder result = new StringBuilder();
            foreach (int house in houses)
            {
                if (result.Length > 0) result.Append(',');
                result.Append(house);
            }
            return result.ToString();
        }

        public static SPKhullarCoordinate ResolveNodeAsPlanetCoordinate(
            string nodeName,
            HouseSignificationData nodeHsd,
            List<PlanetData> planetList,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            SPKhullarCoordinate coord = new SPKhullarCoordinate();
            if (nodeHsd == null) return coord;

            coord.PlanetName = nodeName ?? "";
            string upperNode = coord.PlanetName.Trim().ToUpperInvariant();

            bool hasPS = DeterminePositionalStatus(upperNode, "", planetList);
            coord.HasPositionalStatus = hasPS;

            if (hasPS)
            {
                SortedSet<int> psHouses = new SortedSet<int>();
                CollectHouses(nodeHsd.D3, psHouses);
                CollectHouses(nodeHsd.D7, psHouses);
                CollectHouses(nodeHsd.D8, psHouses);
                CollectCuspalLordshipHouses(nodeHsd.CuspalSignLord, psHouses);
                CollectCuspalLordshipHouses(nodeHsd.CuspalStarLord, psHouses);
                CollectCuspalLordshipHouses(nodeHsd.CuspalSubLord, psHouses);
                CollectCuspalLordshipHouses(nodeHsd.CuspalSSLord, psHouses);
                coord.PositionalStatusHouses = SetToString(psHouses);
            }
            else
            {
                coord.PositionalStatusHouses = "";
            }

            string starLordName = "";
            for (int i = 0; i < planetList.Count; i++)
            {
                if (planetList[i] != null &&
                    string.Equals((planetList[i].Name ?? "").Trim(), upperNode, StringComparison.OrdinalIgnoreCase))
                {
                    starLordName = planetList[i].StarLord ?? "";
                    break;
                }
            }

            coord.StellarStatusHouses = ResolveStarLordHouses(starLordName, significationLookup);

            string signLordName = GetNodeSignLord(upperNode, significationLookup);
            string signLordHouses = ResolveNodeSignLordHouses(signLordName, significationLookup);
            string starLordOfNodeHouses = ResolveNodeStarLordHouses(starLordName, significationLookup);

            SortedSet<int> nodeAsPlanetHouses = new SortedSet<int>();
            CollectHouses(coord.PositionalStatusHouses, nodeAsPlanetHouses);
            CollectHouses(coord.StellarStatusHouses, nodeAsPlanetHouses);
            coord.PlanetResult = SetToString(nodeAsPlanetHouses);

            SortedSet<int> asSignLordHouses = new SortedSet<int>();
            CollectHouses(coord.PositionalStatusHouses, asSignLordHouses);
            CollectHouses(coord.StellarStatusHouses, asSignLordHouses);
            CollectHouses(signLordHouses, asSignLordHouses);
            coord.StarLordResult = SetToString(asSignLordHouses);

            coord.SubLordResult = ResolveSubLordHouses(starLordName, significationLookup);

            return coord;
        }

        public static SPKhullarCoordinate ResolveStarLordAsPlanetCoordinate(
            string starLordName,
            List<PlanetData> planetList,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            SPKhullarCoordinate coord = new SPKhullarCoordinate();
            if (string.IsNullOrEmpty(starLordName) || significationLookup == null) return coord;

            coord.PlanetName = starLordName;

            HouseSignificationData slHsd;
            if (!significationLookup.TryGetValue(starLordName.Trim(), out slHsd))
                return coord;

            PlanetData slPd = null;
            for (int i = 0; i < planetList.Count; i++)
            {
                if (planetList[i] != null &&
                    string.Equals((planetList[i].Name ?? "").Trim(), starLordName.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    slPd = planetList[i];
                    break;
                }
            }

            string slStarLord = slPd != null ? slPd.StarLord ?? "" : "";
            bool isNodeFlag = IsNode(starLordName);
            bool hasPS = DeterminePositionalStatus(starLordName, slStarLord, planetList);
            coord.HasPositionalStatus = hasPS;

            if (hasPS)
            {
                SortedSet<int> psHouses = new SortedSet<int>();
                if (isNodeFlag)
                {
                    CollectHouses(slHsd.D3, psHouses);
                    CollectHouses(slHsd.D8, psHouses);
                }
                else
                {
                    CollectHouses(slHsd.D1, psHouses);
                    CollectHouses(slHsd.D2, psHouses);
                    CollectHouses(slHsd.D3, psHouses);
                    CollectHouses(slHsd.D4, psHouses);
                }
                CollectCuspalLordshipHouses(slHsd.CuspalSignLord, psHouses);
                CollectCuspalLordshipHouses(slHsd.CuspalStarLord, psHouses);
                CollectCuspalLordshipHouses(slHsd.CuspalSubLord, psHouses);
                CollectCuspalLordshipHouses(slHsd.CuspalSSLord, psHouses);
                coord.PositionalStatusHouses = SetToString(psHouses);
            }
            else
            {
                coord.PositionalStatusHouses = "";
            }

            // Stellar Status: use StarLord's D1+D2 (its StarLord's houses)
            coord.StellarStatusHouses = ResolveStarLordHousesFromPlanet(slHsd);

            SortedSet<int> resultHouses = new SortedSet<int>();
            CollectHouses(coord.PositionalStatusHouses, resultHouses);
            CollectHouses(coord.StellarStatusHouses, resultHouses);
            coord.PlanetResult = SetToString(resultHouses);

            // StarLordResult: this entity's StarLord's Stellar Status
            coord.StarLordResult = ResolveStarLordHouses(slStarLord, significationLookup);

            // SubLordResult: this entity's SubLord's Stellar Status
            string slSubLord = slPd != null ? slPd.SubLord ?? "" : "";
            coord.SubLordResult = ResolveSubLordHouses(slSubLord, significationLookup);

            return coord;
        }

        public static SPKhullarCoordinate ResolveSubLordAsPlanetCoordinate(
            string subLordName,
            List<PlanetData> planetList,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            SPKhullarCoordinate coord = new SPKhullarCoordinate();
            if (string.IsNullOrEmpty(subLordName) || significationLookup == null) return coord;

            coord.PlanetName = subLordName;

            HouseSignificationData slHsd;
            if (!significationLookup.TryGetValue(subLordName.Trim(), out slHsd))
                return coord;

            PlanetData slPd = null;
            for (int i = 0; i < planetList.Count; i++)
            {
                if (planetList[i] != null &&
                    string.Equals((planetList[i].Name ?? "").Trim(), subLordName.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    slPd = planetList[i];
                    break;
                }
            }

            string slStarLord = slPd != null ? slPd.StarLord ?? "" : "";
            bool isNodeFlag = IsNode(subLordName);
            bool hasPS = DeterminePositionalStatus(subLordName, slStarLord, planetList);
            coord.HasPositionalStatus = hasPS;

            if (hasPS)
            {
                SortedSet<int> psHouses = new SortedSet<int>();
                if (isNodeFlag)
                {
                    CollectHouses(slHsd.D3, psHouses);
                    CollectHouses(slHsd.D8, psHouses);
                }
                else
                {
                    CollectHouses(slHsd.D1, psHouses);
                    CollectHouses(slHsd.D2, psHouses);
                    CollectHouses(slHsd.D3, psHouses);
                    CollectHouses(slHsd.D4, psHouses);
                }
                CollectCuspalLordshipHouses(slHsd.CuspalSignLord, psHouses);
                CollectCuspalLordshipHouses(slHsd.CuspalStarLord, psHouses);
                CollectCuspalLordshipHouses(slHsd.CuspalSubLord, psHouses);
                CollectCuspalLordshipHouses(slHsd.CuspalSSLord, psHouses);
                coord.PositionalStatusHouses = SetToString(psHouses);
            }
            else
            {
                coord.PositionalStatusHouses = "";
            }

            // Stellar Status: use SubLord's D1+D2 (its StarLord's houses)
            coord.StellarStatusHouses = ResolveStarLordHousesFromPlanet(slHsd);

            SortedSet<int> resultHouses = new SortedSet<int>();
            CollectHouses(coord.PositionalStatusHouses, resultHouses);
            CollectHouses(coord.StellarStatusHouses, resultHouses);
            coord.PlanetResult = SetToString(resultHouses);

            // StarLordResult: this entity's StarLord's Stellar Status
            coord.StarLordResult = ResolveStarLordHouses(slStarLord, significationLookup);

            // SubLordResult: this entity's SubLord's Stellar Status
            string slSubLord = slPd != null ? slPd.SubLord ?? "" : "";
            coord.SubLordResult = ResolveSubLordHouses(slSubLord, significationLookup);

            return coord;
        }

        public static string GetSPKhullarCoordinateString(HouseSignificationData hsd)
        {
            if (hsd == null) return "";

            string sgn = CombineNonEmpty(hsd.D7, hsd.D8);
            string stl = CombineNonEmpty(hsd.D1, hsd.D2);
            string sub = CombineNonEmpty(hsd.D5, hsd.D6);
            string posited = hsd.D3 ?? "";

            return string.Format("Lords of SGN({0}) STL({1}) SUB({2}) Posited({3})",
                sgn, stl, sub, posited);
        }

        public static string GetSPKhullarCoordinateStringWithMarkers(
            string planetName,
            HouseSignificationData hsd,
            List<PlanetData> planetList)
        {
            if (hsd == null)
            {
                return GetFullPlanetName(planetName);
            }

            // Get marker from hsd.Planet field (already set by chart calculation)
            string planetWithMarker = hsd.Planet ?? planetName;
            string planetFullName = GetFullPlanetName(planetWithMarker);
            string coord = GetSPKhullarCoordinateString(hsd);
            return string.Format("{0} {1}", planetFullName, coord);
        }

        private static string GetFullPlanetName(string planetAbbr)
        {
            if (string.IsNullOrEmpty(planetAbbr)) return planetAbbr;
            string clean = planetAbbr.Replace("#", "").Replace("*", "").Trim().ToUpperInvariant();
            string fullName;
            if (PlanetFullNames.TryGetValue(clean, out fullName))
            {
                // Preserve marker
                if (planetAbbr.Contains("#")) return fullName + "#";
                if (planetAbbr.Contains("*")) return fullName + "*";
                return fullName;
            }
            return planetAbbr;
        }

        private static string CombineNonEmpty(string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return b ?? "";
            if (string.IsNullOrEmpty(b)) return a;
            return a + "," + b;
        }

        public static string ResolveKhullarCoordinateString(SPKhullarCoordinate coord)
        {
            if (coord == null) return " |  | ";

            StringBuilder result = new StringBuilder();
            result.Append(coord.PlanetResult ?? "");
            result.Append(" | ");
            result.Append(coord.StarLordResult ?? "");
            result.Append(" | ");
            result.Append(coord.SubLordResult ?? "");
            return result.ToString();
        }

        private static string GetOccupiedHouse(string planetName,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            if (string.IsNullOrEmpty(planetName) || significationLookup == null) return "";
            HouseSignificationData hsd;
            if (significationLookup.TryGetValue(planetName.Trim(), out hsd))
            {
                return hsd.D3;
            }
            return "";
        }

        private static string GetOwnedHouses(string planetName,
            Dictionary<string, HouseSignificationData> significationLookup)
        {
            if (string.IsNullOrEmpty(planetName) || significationLookup == null) return "";
            HouseSignificationData hsd;
            if (significationLookup.TryGetValue(planetName.Trim(), out hsd))
            {
                return hsd.D4;
            }
            return "";
        }

        private static void CollectHouses(string strValue, SortedSet<int> houses)
        {
            if (string.IsNullOrEmpty(strValue)) return;

            string[] entries = strValue.Split(',');
            for (int i = 0; i < entries.Length; i++)
            {
                string entry = entries[i].Trim();
                if (entry.Length == 0) continue;

                int house;
                if (int.TryParse(entry, out house) && house >= 1 && house <= 12)
                {
                    houses.Add(house);
                }
            }
        }

        private static void CollectCuspalLordshipHouses(string cuspalLordshipStr, SortedSet<int> houses)
        {
            if (string.IsNullOrEmpty(cuspalLordshipStr)) return;

            string[] entries = cuspalLordshipStr.Split(',');
            for (int i = 0; i < entries.Length; i++)
            {
                string entry = entries[i].Trim();
                if (entry.Length == 0) continue;

                int cusp;
                if (int.TryParse(entry, out cusp) && cusp >= 1 && cusp <= 12)
                {
                    houses.Add(cusp);
                }
            }
        }

        private static string SetToString(SortedSet<int> houses)
        {
            StringBuilder result = new StringBuilder();
            foreach (int house in houses)
            {
                if (result.Length > 0) result.Append(',');
                result.Append(house);
            }
            return result.ToString();
        }

        public static string FormatCoordinates(SPKhullarCoordinate planetCoord,
            SPKhullarCoordinate starLordCoord, SPKhullarCoordinate subLordCoord)
        {
            StringBuilder result = new StringBuilder();
            result.Append(planetCoord != null ? planetCoord.PlanetResult ?? "" : "");
            result.Append(" | ");
            result.Append(starLordCoord != null ? starLordCoord.PlanetResult ?? "" : "");
            result.Append(" | ");
            result.Append(subLordCoord != null ? subLordCoord.PlanetResult ?? "" : "");
            return result.ToString();
        }
    }
}
