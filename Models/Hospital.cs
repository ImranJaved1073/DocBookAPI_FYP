namespace DocBookAPI.Models
{
    public class Hospital
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        // Navigation properties
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
