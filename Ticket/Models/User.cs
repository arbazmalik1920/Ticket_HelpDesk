
using System.ComponentModel.DataAnnotations;

namespace Ticket.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Invalid mobile number format.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
    }
}














































//namespace Ticket.Models
//{
//    public class User
//    {
//        public int UserId { get; set; }
//        public string Username { get; set; }
//        public string Password { get; set; }
//        public string Name { get; set; }
//        public int DepartmentId { get; set; }
//        public string DepartmentName { get; set; }
//        public string MobileNo { get; set; }
//        public string Email { get; set; }
//        public string Role { get; set; }
//        public string Status { get; set; }
//    }
//}