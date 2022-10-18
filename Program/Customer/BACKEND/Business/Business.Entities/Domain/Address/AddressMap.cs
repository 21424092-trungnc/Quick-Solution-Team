namespace Business.Entities.Domain
{
    public class ProvinceMap
    {
        public int PROVINCEID { get; set; }
        public string PROVINCENAME { get; set; }
        public string ALTNAME { get; set; }
        public string KEYWORDS { get; set; }
        public string ZIPCODE { get; set; }
    }
    public class DistictMap
    {
        public int DISTRICTID { get; set; }
		public string DISTRICTNAME { get; set; }
        public string KEYWORDS { get; set; }
        public string ZIPCODE { get; set; }
        public string PROVINCEID { get; set; }
    }

    public class WardMap {
         public int WARDID { get; set; }
         public string WARDNAME { get; set; }
        public int DISTRICTID { get; set; }
	    public int PROVINCEID { get; set; }
	    public string KEYWORDS { get; set; }
        public int PRIORITY { get; set; }
    }
}
