namespace CarServiceAPI.Models
{
    public class Technician
    {
        public int TechnicianId { get; set; }

        public int UserId { get; set; }

        public string Specialization { get; set; }

        public string Status { get; set; } // available / busy
    }
}
