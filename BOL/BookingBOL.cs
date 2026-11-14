namespace MediProApi.BOL
{
    public class BookingBOL
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public String? PatientName { get; set; }
        public String? Department { get; set; }
        public String? Doctor { get; set; }
        public String? Date { get; set; }
        public String Time { get; set; }
        public String? Clinic { get; set; }
        public String? P_Place { get; set; }
        public String? P_Address { get; set; }
        public String? P_Contact { get; set; }
        public int P_Age { get; set; }
        public String? P_Gender { get; set; }
        public int SlotNum { get; set; }
    }
}
