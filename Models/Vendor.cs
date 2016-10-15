using System.ComponentModel.DataAnnotations;

namespace mn_you.Models.SQLite
{
    public class Vendor
    {
        public int VendorId { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [RequiredAttribute]
        public string Email { get; set; }

        [RequiredAttribute]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Bio { get; set; }

    }

}