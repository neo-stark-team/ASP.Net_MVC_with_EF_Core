
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
    public class BloodDonor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DOB { get; set; }
         [Required]
        public string BloodGroup { get; set; }
         [Required]
        public string Gender { get; set; }
         [Required]
        public string Location { get; set; }
         [Required]
        public string MobileNumber { get; set; }
         [Required]
        public string Email { get; set; }

    }
}