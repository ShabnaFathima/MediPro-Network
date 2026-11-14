namespace MediProApi.BOL
{
    public class ResultBOL
    {
        public ResultBOL()
        {
            Details = RecordStatus = "";

        }
        public string RecordId { get; set; }
        public string RecordStatus { get; set; }
        public string Details { get; set; }
    }
}
