using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ticket.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string Status { get; set; }

        // Navigation Properties
        public string CategoryName { get; set; }
        public string DepartmentName { get; set; }
    }

}