namespace MediProApi.BOL
{
    public class DoctorClinicsBOL
    {
        public int DocId { get; set; }
        public int ClinId { get; set; }
        public int NumSlots { get; set; }
        public int AvgConTime { get; set; }
        public int StartTimeHr { get; set; }
        public int StartTimeMin { get; set; }
        public String AmPm { get; set; }
    }
}
