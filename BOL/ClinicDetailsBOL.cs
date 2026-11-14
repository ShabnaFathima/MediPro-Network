namespace MediProApi.BOL
{
    public class ClinicDetailsBOL
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Place { get; set; }
        public String Address { get; set; }
        public String Contact { get; set; }
        public String? location { get; set; }
    }
}

