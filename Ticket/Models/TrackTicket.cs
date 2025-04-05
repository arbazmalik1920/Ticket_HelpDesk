
using System;
using System.ComponentModel.DataAnnotations;

namespace Ticket.Models
{
    public class TrackTicket
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ticket number is required.")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } // For display

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } // For display

        [Required(ErrorMessage = "Subcategory is required.")]
        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; } // For display

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? Date { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? ClosingDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "TAT must be a positive number.")]
        public int? TAT { get; set; } // Nullable for tickets without a ClosingDate

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }

    }
}


//using System;

//namespace Ticket.Models
//{
//    public class TrackTicket
//    {
//        public int Id { get; set; }
//        public string TicketNo { get; set; }
//        public int DepartmentId { get; set; }
//        public string DepartmentName { get; set; } // For display
//        public int CategoryId { get; set; }
//        public string CategoryName { get; set; } // For display
//        public int SubCategoryId { get; set; }
//        public string SubCategoryName { get; set; } // For display
//        public DateTime? Date { get; set; }
//        public DateTime? ClosingDate { get; set; }
//        public int? TAT { get; set; } // Nullable for tickets without a ClosingDate
//        public string Status { get; set; } // Example: Open, Closed, Pending
//    }
//}