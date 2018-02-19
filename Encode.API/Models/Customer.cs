using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Encode.API.Models
{
    public class Customer
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title of the Customer
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title must be 200 characters or less")]
        public string Title { get; set; }

        /// <summary>
        /// Number Of Employees per Customers
        /// </summary>
        [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int NumberOfEmployees { get; set; }
    }
}