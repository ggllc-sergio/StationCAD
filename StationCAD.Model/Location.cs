using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public class GeoLocation
    {
        public bool PartialMatch { get; set; }
        public string Type { get; set; }
        public string PlaceID { get; set; }
        public string FormattedAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public ICollection<GoogleAddressComponent> AddressComponents { get; set; }
    }

    public class GoogleGeoCodeResponse
    {

        public string status { get; set; }
        public GoogleResult[] results { get; set; }

    }

    public class GoogleResult
    {
        public string formatted_address { get; set; }
        public GoogleGeometry geometry { get; set; }
        public string[] types { get; set; }
        public GoogleAddressComponent[] address_components { get; set; }
        public string place_id { get; set; }
        public bool partial_match { get; set; }
    }

    public class GoogleGeometry
    {
        public string location_type { get; set; }
        public GoogleLocation location { get; set; }
    }

    public class GoogleLocation
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class GoogleAddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
    public enum GoogleAddressComponentType
    {
        street_number,
        route,
        locality,
        administrative_area_level_3,
        administrative_area_level_2,
        administrative_area_level_1,
        country,
        postal_code,
        postal_code_suffix
    }


    public class ChesterCountyData
    {
        private List<Municipality> _munic;
        public ICollection<Municipality> Municipalities
        {
            get
            {
                if (_munic == null)
                {
                    _munic = new List<Municipality>();
                    _munic.Add(new Municipality { Name = "West Chester Borough", Abbreviation = "WCHEST", Code = "01" });
                    _munic.Add(new Municipality { Name = "Malvern Borough", Abbreviation = "MALVRN", Code = "02" });
                    _munic.Add(new Municipality { Name = "Kennett Square Borough", Abbreviation = "KNTSQR", Code = "03" });
                    _munic.Add(new Municipality { Name = "Avondale Borough", Abbreviation = "AVNDAL", Code = "04" });
                    _munic.Add(new Municipality { Name = "West Grove Borough", Abbreviation = "WGROVE", Code = "05" });
                    _munic.Add(new Municipality { Name = "Oxford Borough", Abbreviation = "OXFRD", Code = "06" });
                    _munic.Add(new Municipality { Name = "Atglen Borough", Abbreviation = "ATGLN", Code = "07" });
                    _munic.Add(new Municipality { Name = "Parkesburg Borough", Abbreviation = "PRKSBG", Code = "08" });
                    _munic.Add(new Municipality { Name = "South Coatesville Borough", Abbreviation = "SCOATV", Code = "09" });
                    _munic.Add(new Municipality { Name = "Modena Borough", Abbreviation = "MODNA", Code = "10" });
                    _munic.Add(new Municipality { Name = "Downingtown Borough", Abbreviation = "DNGTWN", Code = "11" });
                    _munic.Add(new Municipality { Name = "Honey Brook Borough", Abbreviation = "HBBORO", Code = "12" });
                    _munic.Add(new Municipality { Name = "Elverson Borough", Abbreviation = "ELVRSN", Code = "13" });
                    _munic.Add(new Municipality { Name = "Spring City Borough", Abbreviation = "SPRCTY", Code = "14" });
                    _munic.Add(new Municipality { Name = "Phoenixville Borough", Abbreviation = "PHNXVL", Code = "15" });
                    _munic.Add(new Municipality { Name = "Coatesville City", Abbreviation = "COATVL", Code = "16" });
                    _munic.Add(new Municipality { Name = "North Coventry Township", Abbreviation = "NCVNTY", Code = "17" });
                    _munic.Add(new Municipality { Name = "East Coventry Township", Abbreviation = "ECVNTY", Code = "18" });
                    _munic.Add(new Municipality { Name = "Warwick Township", Abbreviation = "WARWCK", Code = "19" });
                    _munic.Add(new Municipality { Name = "South Coventry Township", Abbreviation = "SCVNTY", Code = "20" });
                    _munic.Add(new Municipality { Name = "East Vincent Township", Abbreviation = "EVINCT", Code = "21" });
                    _munic.Add(new Municipality { Name = "Honey Brook Township", Abbreviation = "HBTWP", Code = "22" });
                    _munic.Add(new Municipality { Name = "West Nantmeal Township", Abbreviation = "WNANT", Code = "23" });
                    _munic.Add(new Municipality { Name = "East Nantmeal Township", Abbreviation = "ENANT", Code = "24" });
                    _munic.Add(new Municipality { Name = "West Vincent Township", Abbreviation = "WVINCT", Code = "25" });
                    _munic.Add(new Municipality { Name = "East Pikeland Township", Abbreviation = "EPIKEL", Code = "26" });
                    _munic.Add(new Municipality { Name = "Schuylkill Township", Abbreviation = "SCHYKL", Code = "27" });
                    _munic.Add(new Municipality { Name = "West Caln Township", Abbreviation = "WCALN", Code = "28" });
                    _munic.Add(new Municipality { Name = "West Brandywine Township", Abbreviation = "WBRAND", Code = "29" });
                    _munic.Add(new Municipality { Name = "East Brandywine Township", Abbreviation = "EBRAND", Code = "30" });
                    _munic.Add(new Municipality { Name = "Wallace Township", Abbreviation = "WALLAC", Code = "31" });
                    _munic.Add(new Municipality { Name = "Upper Uwchlan Township", Abbreviation = "UPUWCH", Code = "32" });
                    _munic.Add(new Municipality { Name = "Uwchlan Township", Abbreviation = "UWCHLN", Code = "33" });
                    _munic.Add(new Municipality { Name = "West Pikeland Township", Abbreviation = "WPIKEL", Code = "34" });
                    _munic.Add(new Municipality { Name = "Charlestown Township", Abbreviation = "CHARLS", Code = "35" });
                    _munic.Add(new Municipality { Name = "West Sadsbury Township", Abbreviation = "WSADS", Code = "36" });
                    _munic.Add(new Municipality { Name = "Sadsbury Township", Abbreviation = "SADS", Code = "37" });
                    _munic.Add(new Municipality { Name = "Valley Township", Abbreviation = "VALLY", Code = "38" });
                    _munic.Add(new Municipality { Name = "Caln Township", Abbreviation = "CLN", Code = "39" });
                    _munic.Add(new Municipality { Name = "East Caln Township", Abbreviation = "ECALN", Code = "40" });
                    _munic.Add(new Municipality { Name = "West Whiteland Township", Abbreviation = "WWHITE", Code = "41" });
                    _munic.Add(new Municipality { Name = "East Whiteland Township", Abbreviation = "EWHITE", Code = "42" });
                    _munic.Add(new Municipality { Name = "Tredyffrin Township", Abbreviation = "TREDYF", Code = "43" });
                    _munic.Add(new Municipality { Name = "West Fallowfield Township", Abbreviation = "WFALLO", Code = "44" });
                    _munic.Add(new Municipality { Name = "Highland Township", Abbreviation = "HGHLND", Code = "45" });
                    _munic.Add(new Municipality { Name = "Londonderry Township", Abbreviation = "LONDER", Code = "46" });
                    _munic.Add(new Municipality { Name = "East Fallowfield Township", Abbreviation = "EFALLO", Code = "47" });
                    _munic.Add(new Municipality { Name = "West Marlborough Township", Abbreviation = "WMARLB", Code = "48" });
                    _munic.Add(new Municipality { Name = "Newlin Township", Abbreviation = "NEWLN", Code = "49" });
                    _munic.Add(new Municipality { Name = "West Bradford Township", Abbreviation = "WBRAD", Code = "50" });
                    _munic.Add(new Municipality { Name = "East Bradford Township", Abbreviation = "EBRAD", Code = "51" });
                    _munic.Add(new Municipality { Name = "West Goshen Township", Abbreviation = "WGOSHN", Code = "52" });
                    _munic.Add(new Municipality { Name = "East Goshen Township", Abbreviation = "EGOSHN", Code = "53" });
                    _munic.Add(new Municipality { Name = "Willistown Township", Abbreviation = "WILSTN", Code = "54" });
                    _munic.Add(new Municipality { Name = "Easttown Township", Abbreviation = "EASTWN", Code = "55" });
                    _munic.Add(new Municipality { Name = "Lower Oxford Township", Abbreviation = "LWROXF", Code = "56" });
                    _munic.Add(new Municipality { Name = "Upper Oxford Township", Abbreviation = "UPROXF", Code = "57" });
                    _munic.Add(new Municipality { Name = "Penn Township", Abbreviation = "PENNTP", Code = "58" });
                    _munic.Add(new Municipality { Name = "London Grove Township", Abbreviation = "LGROVE", Code = "59" });
                    _munic.Add(new Municipality { Name = "New Garden Township", Abbreviation = "NGARDN", Code = "60" });
                    _munic.Add(new Municipality { Name = "East Marlborough Township", Abbreviation = "EMARLB", Code = "61" });
                    _munic.Add(new Municipality { Name = "Kennett Township", Abbreviation = "KNTTWP", Code = "62" });
                    _munic.Add(new Municipality { Name = "Pocopson Township", Abbreviation = "POCOPS", Code = "63" });
                    _munic.Add(new Municipality { Name = "Pennsbury Township", Abbreviation = "PNSBRY", Code = "64" });
                    _munic.Add(new Municipality { Name = "Birmingham Township", Abbreviation = "BIRMHM", Code = "65" });
                    _munic.Add(new Municipality { Name = "Thornbury Township", Abbreviation = "THORNB", Code = "66" });
                    _munic.Add(new Municipality { Name = "Westtown Township", Abbreviation = "WESTWN", Code = "67" });
                    _munic.Add(new Municipality { Name = "West Nottingham Township", Abbreviation = "WNOTT", Code = "68" });
                    _munic.Add(new Municipality { Name = "East Nottingham Township", Abbreviation = "ENOTT", Code = "69" });
                    _munic.Add(new Municipality { Name = "Elk Township", Abbreviation = "ELKTP", Code = "70" });
                    _munic.Add(new Municipality { Name = "New London Township", Abbreviation = "NEWLND", Code = "71" });
                    _munic.Add(new Municipality { Name = "Franklin Township", Abbreviation = "FRNKLN", Code = "72" });
                    _munic.Add(new Municipality { Name = "London Britain Township", Abbreviation = "LDNBRT", Code = "73" });
                }
                return _munic;
            }

        }

        private List<EventCode> _evtCode;
        public ICollection<EventCode> EventCodes
        {
            get
            {
                if (_evtCode == null)
                {
                    _evtCode = new List<EventCode>();
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ACCIDENT", SubTypeCode = "BLS", Description = "BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ACCIDENT", SubTypeCode = "HAZ", Description = "HAZ-MAT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ACCIDENT", SubTypeCode = "MASS", Description = "MASS TRANSIT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ACCIDENT", SubTypeCode = "SER", Description = "SERIOUS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ALARM", SubTypeCode = "CO", Description = "CARBON MONOXIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ALARM", SubTypeCode = "FIRE", Description = "FIRE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ALARM", SubTypeCode = "GAS", Description = "GAS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ALARM", SubTypeCode = "HAZ", Description = "HAZMAT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "ALARM", SubTypeCode = "MED", Description = "MEDICAL - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "AGRES", Description = "AGRICULTURAL RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "APLNC", Description = "APPLIANCE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "APT", Description = "APARTMENT BUILDING" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "ASSTAMB", Description = "ASSIST THE AMBULANCE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "ASSTPD", Description = "ASSIST THE POLICE DEPARTMENT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "BARN", Description = "BARN" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "BOMBFND", Description = "BOMB/EXPLOSIVES FOUND" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "BRUSH", Description = "BRUSH" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "BUILDNG", Description = "BUILDING" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "BURNING", Description = "BURNING COMPLAINT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "CHIMNEY", Description = "CHIMNEY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "COVER", Description = "COVER UNIT FROM STA (FIRE/EMS)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "DEBRIS", Description = "DEBRIS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "DRILL", Description = "DRILL TYPE CODE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "ELECIN", Description = "ELECTRICAL INSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "ELEVRES", Description = "STUCK/MALFUNCTIONING ELEVATOR" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "FARMEQ", Description = "FARM EQUIPMENT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "FIRE", Description = "FIRE CALL - TERT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "FORCIBL", Description = "FORCIBLE ENTRY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "FP", Description = "FIRE POLICE REQUEST" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "FUEL", Description = "FUEL SPILL" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "GARAGE", Description = "GARAGE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "GASLKIN", Description = "GAS LEAK INSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "GASLKOU", Description = "GAS LEAK OUTSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "GRILL", Description = "BBQ GRILL - FIRE/GAS LEAK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "HAYBALE", Description = "HAYBALES" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "HAZMAT", Description = "HAZARDOUS MATERIALS INCIDENT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "HOUSE", Description = "HOUSE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "INDRES", Description = "INDUSTRIAL RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "INVIN", Description = "ODOR INVESTIGATION INSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "INVOUT", Description = "SMOKE/ODOR INVEST OUTSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "KNOX", Description = "KNOX BOX NOTIFICATION" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "LANDING", Description = "HELICOPTER LANDING" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "LIGHTS", Description = "LIGHTING ASSIST" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "MISCFD", Description = "MISC FIRE DEPT ACTIVITY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "NOTIFFC", Description = "NOTIFY FIRE CHIEF" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "NOTIFFM", Description = "NOTIFY MUNICIPAL FIRE MARSHAL" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "OTHERES", Description = "OTHER TYPE RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "OUTBLDG", Description = "OUT BUILDING/SHED" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "PUBSERV", Description = "PUBLIC SERVICE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "RELOCAT", Description = "RELOCATE FIRE/EMS UNITS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "RESDRES", Description = "RESIDENTIAL RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "SEARCH", Description = "SEARCH DETAIL" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "TRAILER", Description = "MOBILE HOME/TRAILER" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "TRASH", Description = "TRASH/DUMPSTER" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "UNKFIRE", Description = "UNKNOWN TYPE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "VEHICLE", Description = "VEHICLE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "WIRES", Description = "\0POLES" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "WTRREC", Description = "WATER RECOVERY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "FIRE", SubTypeCode = "WTRRES", Description = "WATER RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AABDPN", Description = "ABDOMINAL PAIN - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AALLERG", Description = "ALLERGC/MED REACTION - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AARREST", Description = "CARDIAC/RESP ARREST - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AASSINJ", Description = "ASSAULT W/ INJ (JO/IP) - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ABACKPN", Description = "BACK PAIN - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ABURN", Description = "BURNS - MISC - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ACHMBRN", Description = "BURNS - CHEMICAL - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ACHOKNG", Description = "CHOKING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ACHSTPN", Description = "CHEST PAINS - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ACVA", Description = "CVA/STROKE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ADIABET", Description = "DIABETIC EMERGENCY - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ADROWN", Description = "DROWNING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AELECTR", Description = "ELECTROCUTION - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AEQUEST", Description = "EQUESTRIAN RELATED INJ - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AEXPOSR", Description = "EXPOSURE TO HEAT/COLD - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AFALL", Description = "FALLS - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AFIRBRN", Description = "BURNS - FIRE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AFRAC", Description = "FRACTURES - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AHEAD", Description = "NEUROLOGICAL/HEAD INJ - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AHEART", Description = "HEART PROBLEMS - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AHEMORG", Description = "HEMORRHAGING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AHYPOTN", Description = "HYPO TENSION - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AINJ", Description = "INJURED PERSON - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ALABOR", Description = "MATERNITY/LABOR PAIN - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ALS", Description = "ALS CALL - TERT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AMENTAL", Description = "EMOTIONAL DISORDER - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AOD", Description = "OVERDOSE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "APOISON", Description = "POISONING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ARESPDF", Description = "RESPIRATORY DIFFICULTY - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ASCALD", Description = "BURNS - SCALDING/OTHER - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ASEIZUR", Description = "SEIZURES - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ASHOOTG", Description = "SHOOTING - W/INJ (JO/IP)-ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "ASTAB", Description = "STABBING - W/INJ (JO/IP)-ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AUNC", Description = "UNCONSCIOUS PERSON - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AUNKEMS", Description = "UNKNOWN NATURE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "AUNRESP", Description = "UNRESPONSIVE PERSON - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BABDPN", Description = "ABDOMINAL PAIN - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BALLERG", Description = "ALLERGC/MED REACTION - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BASSINJ", Description = "ASSAULT W/ INJ (JO/IP) - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BBACKPN", Description = "BACK PAIN - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BBURN", Description = "BURNS - MISC - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BCHMBRN", Description = "BURNS - CHEMICAL - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BCHOKNG", Description = "CHOKING - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BDOA", Description = "DOA - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BEQUEST", Description = "EQUESTRIAN RELATED INJ - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BET", Description = "HOSP TO HOSP EMERG TRANS - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BEXPOSR", Description = "EXPOSURE TO HEAT/COLD - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BFALL", Description = "FALL / LIFT ASSIST - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BFIRBRN", Description = "BURNS - FIRE - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BFRAC", Description = "FRACTURES - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BFSTDBY", Description = "FIRE STANDBY (AMB) - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BHEMORG", Description = "HEMORRHAGING - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BHYPRTN", Description = "HYPER TENSION - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BINJ", Description = "INJURED PERSON - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BLABOR", Description = "MATERNITY/LABOR PAIN - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BLACR", Description = "LACERATIONS - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BLS", Description = "BLS CALL - TERT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BMENTAL", Description = "EMOTIONAL DISORDER - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BOD", Description = "OVERDOSE - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BRT", Description = "NON-EMERGENCY EMS TRANS - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BSCALD", Description = "BURNS - SCALDING/OTHER - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BSEIZUR", Description = "SEIZURES - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BSICK", Description = "SICK PERSON - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "MEDICAL", SubTypeCode = "BSYNCOP", Description = "SYNCOPE - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "POLICE", SubTypeCode = "ACTIVETHRT", Description = "ACTIVE THREAT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "STORM", SubTypeCode = "1", Description = "FIRE ALARM" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "STORM", SubTypeCode = "3", Description = "LIGHTNING STRIK/NO FIRE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "STORM", SubTypeCode = "4", Description = "\0POLES" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "STORM", SubTypeCode = "5", Description = "WATER RESCUE NO ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.FIRE, TypeCode = "STORM", SubTypeCode = "EOC", Description = "EOC NOTIFICATION" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ACCIDENT", SubTypeCode = "BLS", Description = "BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ACCIDENT", SubTypeCode = "HAZ", Description = "HAZ-MAT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ACCIDENT", SubTypeCode = "HR", Description = "HIT & RUN NO INJURY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ACCIDENT", SubTypeCode = "MASS", Description = "MASS TRANSIT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ACCIDENT", SubTypeCode = "PD", Description = "PROPERTY DAMAGE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ACCIDENT", SubTypeCode = "SER", Description = "SERIOUS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ALARM", SubTypeCode = "CO", Description = "CARBON MONOXIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ALARM", SubTypeCode = "FIRE", Description = "FIRE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ALARM", SubTypeCode = "GAS", Description = "GAS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ALARM", SubTypeCode = "HAZ", Description = "HAZMAT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ALARM", SubTypeCode = "MED", Description = "MEDICAL - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "ALARM", SubTypeCode = "POLICE", Description = "POLICE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "AGRES", Description = "AGRICULTURAL RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "APLNC", Description = "APPLIANCE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "APT", Description = "APARTMENT BUILDING" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "ASSTAMB", Description = "ASSIST THE AMBULANCE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "BARN", Description = "BARN" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "BOMBFND", Description = "BOMB/EXPLOSIVES FOUND" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "BRUSH", Description = "BRUSH" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "BUILDNG", Description = "BUILDING" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "BURNING", Description = "BURNING COMPLAINT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "CHIMNEY", Description = "CHIMNEY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "DEBRIS", Description = "DEBRIS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "ELECIN", Description = "ELECTRICAL INSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "ELEVRES", Description = "STUCK/MALFUNCTIONING ELEVATOR" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "FARMEQ", Description = "FARM EQUIPMENT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "FIRE", Description = "FIRE CALL - TERT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "FORCIBL", Description = "FORCIBLE ENTRY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "FP", Description = "FIRE POLICE REQUEST" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "FUEL", Description = "FUEL SPILL" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "GARAGE", Description = "GARAGE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "GASLKIN", Description = "GAS LEAK INSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "GASLKOU", Description = "GAS LEAK OUTSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "GRILL", Description = "BBQ GRILL - FIRE/GAS LEAK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "HAYBALE", Description = "HAYBALES" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "HAZMAT", Description = "HAZARDOUS MATERIALS INCIDENT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "HOUSE", Description = "HOUSE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "INDRES", Description = "INDUSTRIAL RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "INVIN", Description = "ODOR INVESTIGATION INSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "INVOUT", Description = "SMOKE/ODOR INVEST OUTSIDE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "LANDING", Description = "HELICOPTER LANDING" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "LIGHTS", Description = "LIGHTING ASSIST" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "OTHERES", Description = "OTHER TYPE RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "OUTBLDG", Description = "OUT BUILDING/SHED" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "PUBSERV", Description = "PUBLIC SERVICE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "RESDRES", Description = "RESIDENTIAL RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "SEARCH", Description = "SEARCH DETAIL" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "TRAILER", Description = "MOBILE HOME/TRAILER" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "TRASH", Description = "TRASH/DUMPSTER" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "UNKFIRE", Description = "UNKNOWN TYPE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "VEHICLE", Description = "VEHICLE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "WIRES", Description = "\0POLES" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "WTRREC", Description = "WATER RECOVERY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "FIRE", SubTypeCode = "WTRRES", Description = "WATER RESCUE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AABDPN", Description = "ABDOMINAL PAIN - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AALLERG", Description = "ALLERGC/MED REACTION - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AARREST", Description = "CARDIAC/RESP ARREST - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AASSINJ", Description = "ASSAULT W/ INJ (JO/IP) - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ABACKPN", Description = "BACK PAIN - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ABURN", Description = "BURNS - MISC - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ACHMBRN", Description = "BURNS - CHEMICAL - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ACHOKNG", Description = "CHOKING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ACHSTPN", Description = "CHEST PAINS - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ACVA", Description = "CVA/STROKE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ADIABET", Description = "DIABETIC EMERGENCY - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ADROWN", Description = "DROWNING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AELECTR", Description = "ELECTROCUTION - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AEQUEST", Description = "EQUESTRIAN RELATED INJ - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AEXPOSR", Description = "EXPOSURE TO HEAT/COLD - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AFALL", Description = "FALLS - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AFIRBRN", Description = "BURNS - FIRE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AFRAC", Description = "FRACTURES - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AHEAD", Description = "NEUROLOGICAL/HEAD INJ - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AHEART", Description = "HEART PROBLEMS - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AHEMORG", Description = "HEMORRHAGING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AHYPOTN", Description = "HYPO TENSION - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AINJ", Description = "INJURED PERSON - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ALABOR", Description = "MATERNITY/LABOR PAIN - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ALS", Description = "ALS CALL - TERT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AMENTAL", Description = "EMOTIONAL DISORDER - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AOD", Description = "OVERDOSE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "APOISON", Description = "POISONING - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ARESPDF", Description = "RESPIRATORY DIFFICULTY - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ASCALD", Description = "BURNS - SCALDING/OTHER - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ASEIZUR", Description = "SEIZURES - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ASHOOTG", Description = "SHOOTING - W/INJ (JO/IP)-ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "ASTAB", Description = "STABBING - W/INJ (JO/IP)-ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AUNC", Description = "UNCONSCIOUS PERSON - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AUNKEMS", Description = "UNKNOWN NATURE - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "AUNRESP", Description = "UNRESPONSIVE PERSON - ALS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BABDPN", Description = "ABDOMINAL PAIN - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BALLERG", Description = "ALLERGC/MED REACTION - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BASSINJ", Description = "ASSAULT W/ INJ (JO/IP) - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BBACKPN", Description = "BACK PAIN - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BBURN", Description = "BURNS - MISC - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BCHMBRN", Description = "BURNS - CHEMICAL - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BCHOKNG", Description = "CHOKING - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BDOA", Description = "DOA - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BEQUEST", Description = "EQUESTRIAN RELATED INJ - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BEXPOSR", Description = "EXPOSURE TO HEAT/COLD - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BFALL", Description = "FALL / LIFT ASSIST - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BFIRBRN", Description = "BURNS - FIRE - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BFRAC", Description = "FRACTURES - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BHEMORG", Description = "HEMORRHAGING - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BHYPRTN", Description = "HYPER TENSION - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BINJ", Description = "INJURED PERSON - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BLABOR", Description = "MATERNITY/LABOR PAIN - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BLACR", Description = "LACERATIONS - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BLS", Description = "BLS CALL - TERT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BMENTAL", Description = "EMOTIONAL DISORDER - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BOD", Description = "OVERDOSE - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BSCALD", Description = "BURNS - SCALDING/OTHER - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BSEIZUR", Description = "SEIZURES - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BSICK", Description = "SICK PERSON - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "MEDICAL", SubTypeCode = "BSYNCOP", Description = "SYNCOPE - BLS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "911", Description = "911 HANG UP" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ABDUCT", Description = "ABDUCTION (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ACTIVETHRT", Description = "ACTIVE THREAT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ANIMAL", Description = "ANIMAL COMPLAINT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ASSAULT", Description = "ASSAULT - NO INJURIES (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ASSIST", Description = "ASSIST OTHER PD" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ASSTFD", Description = "ASSIST THE FIRE DEPARTMENT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "BACKUP", Description = "BACK-UP POLICE OFFICER" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "BANKCHK", Description = "BANK CHECK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "BAR", Description = "BAR CHECK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "BOMBTH", Description = "BOMB THREAT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "BURG", Description = "BURGLARY (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "BUSINES", Description = "BUSINESS CHECK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "CRIMMIS", Description = "CRIM/MISCHIEF (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "DISTURB", Description = "DISTURBANCE (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "DOMESTI", Description = "DOMESTIC (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "DRUGS", Description = "DRUGS (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "DRUNK", Description = "PUBLC DRUNK (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "DUI", Description = "DUI" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ERRATIC", Description = "ERRATIC DRIVER (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ESCORT", Description = "ESCORT - PERSON/MONEY" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "FIGHT", Description = "FIGHT (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "FPDI", Description = "FOR POLICE DEPT.INFORMATION" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "HARASS", Description = "HARASSMENT (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "HSECHK", Description = "HOUSE CHECK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "IDEMERG", Description = "EMERGENCY RADIO ID ACTIVATION" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "KEY", Description = "\0KEYS - LOCKED (VEH" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "KEYAUTO", Description = "KEYS-CHILD/PERSON LOCKD IN VEH" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "LICCHK", Description = "HUNTING/FISHING LICENSE CHECK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "LOITER", Description = "LOITERING(PERSON/GROUP)(JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "MISC", Description = "MISCELLANEOUS POLICE NATURE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "MISSING", Description = "MISSING PERSON" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "NOISE", Description = "NOISE COMPLAINT (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "NOTIFPC", Description = "NOTIFY POLICE CHIEF" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "PARKING", Description = "PARKING COMPLAINT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "PATCHK", Description = "PATROL CHECK - OUTSIDE AREAS" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "PFA", Description = "PFA SERVICE/VIOLATION" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "PHONE", Description = "PHONE ASSIGNMENT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "POLICE", Description = "POLICE TYPE CODE - TERT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "PROWLER", Description = "PROWLER (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "REPO", Description = "REPOSSESSION" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "REPORT", Description = "REPORT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "ROBBERY", Description = "ROBBERY (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "SHOTS", Description = "SHOTS HEARD (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "SPURS", Description = "SUBJECT PURSUIT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "SSTOP", Description = "SUBJECT STOP" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "STVH", Description = "STOLEN VEHICLE (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "SUSPCON", Description = "SUSPICIOUS CONDITION(S)(JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "THEFT", Description = "THEFT (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "TPURS", Description = "TRAFFIC PURSUIT" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "TRAFFIC", Description = "TRAFFIC-GENERAL/HAZARDS (PD)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "TRESPAS", Description = "TRESPASSER(S) (JO/IP)" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "TSTOP", Description = "TRAFFIC STOP" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "UNKPD", Description = "UNKNOWN TYPE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "WARRANT", Description = "WARRANT HIT / SERVICE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "POLICE", SubTypeCode = "WELLBNG", Description = "WELL-BEING CHECK" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "STORM", SubTypeCode = "1", Description = "FIRE ALARM" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "STORM", SubTypeCode = "2", Description = "POLICE ALARM" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "STORM", SubTypeCode = "3", Description = "LIGHTNING STRIK/NO FIRE" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "STORM", SubTypeCode = "4", Description = "\0POLES" });
                    _evtCode.Add(new EventCode { Group = EventGroup.POLICE, TypeCode = "STORM", SubTypeCode = "5", Description = "WATER RESCUE NO ALS" });


                }
                return _evtCode;
            }
        }
    }

    public class Municipality
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Code { get; set; }
    }

    public class EventCode
    {
        public EventGroup Group { get; set; }
        public string TypeCode { get; set; }
        public string SubTypeCode { get; set; }
        public string Description { get; set; }
    }

    public enum EventGroup
    {
        FIRE,
        EMS,
        POLICE
    }

    public enum ReportType
    {
        Dispatch,
        Update,
        Clear,
        Close
    }
}
