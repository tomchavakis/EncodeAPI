using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Encode.API.Models
{
    public class CustomerContact
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(100, ErrorMessage = "FirstName must be 100 characters or less")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(100, ErrorMessage = "LastName must be 100 characters or less")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "The email address is required")]
        [MaxLength(100, ErrorMessage = "LastName must be 100 characters or less")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}