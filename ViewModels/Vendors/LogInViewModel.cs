using System.ComponentModel.DataAnnotations;

namespace mn_you.ViewModels.Vendors
{
    public class LogInViewModel
    {
        [EmailAddress]
        [RequiredAttribute]
        public string Email { get; set; }

        [RequiredAttribute]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}