namespace Business.Entities.Domain
{
    public class CustomerDataLeadsParam
    {
        public string DataLeadsID { get; set; }
	    public string FullName { get; set; }
        public string BirthDay { get; set; }
        public int GenDer{ get; set; }
        public string PhoneNumber { get; set; }
	    public string Email { get; set; }
	    public int MaritalStatus { get; set; }
        public string IDCard { get; set; }
        public string IDCardDate { get; set; }
        public string IDCardPlace { get; set; }
	    public string Address  { get; set; }
        public string AddressFull { get; set; }
	    public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int CountryID { get; set; }
        public int WardID { get; set; }
	    public int BusinessID  { get; set; }
        public int IsActive { get; set; }
	    public string CreatedUser { get; set; }
    }
    public class CustomerDataLeadsMap
    {
        public string DATALEADSID { get; set; }
    }
}
