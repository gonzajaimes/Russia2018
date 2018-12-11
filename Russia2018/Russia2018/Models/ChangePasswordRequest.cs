

namespace Russia2018.Models
{
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }

        public string Email { get; set; }

        public string NewPassword { get; set; }
    }
}
