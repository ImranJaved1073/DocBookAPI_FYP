namespace DocBookAPI.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }

        public string Role { get; set; } // Patient, Doctor, Admin

        public string Gender { get; set; } // Male, Female, Other
    }
}
