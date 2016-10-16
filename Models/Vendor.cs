using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace mn_you.Models.SQLite
{
    public class Vendor
    {
        public int VendorId { get; set; }

        [RequiredAttribute]
        public string Name { get; set; }

        [EmailAddressAttribute]
        [RequiredAttribute]
        public string Email { get; set; }

        [RequiredAttribute]
        [DataTypeAttribute(DataType.Password)]
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public string Zip { get; set; }

        public string Bio { get; set; }

        [DataTypeAttribute(DataType.MultilineText)]
        public string Slug { get; set; }


        public void GenerateSlug() {
            string str = Name.ToLower();
            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens

            Slug = str;
        }

        public void CopyValuesFrom(Vendor vendor) {
            Name = vendor.Name;
            Email = vendor.Email;
            Address = vendor.Address;
            City = vendor.City;
            State = vendor.State;
            Zip = vendor.Zip;
            Bio = vendor.Bio;
        }


    }

}