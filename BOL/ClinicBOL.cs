using System.ComponentModel.DataAnnotations;

namespace MediProApi.BOL
{
    public class ClinicBOL
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
