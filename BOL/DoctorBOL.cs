using System.Xml.Linq;

namespace MediProApi.BOL
{
    public class DoctorBOL
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Department { get; set; }
        public String Specializations { get; set; }
        public String Qualifications { get; set; }

    }
}