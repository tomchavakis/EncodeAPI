namespace Encode.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Customer
    {
        public Customer()
        {
            //CustomerContacts = new HashSet<CustomerContact>();
        }

        [Key]
        public int Id { get; set; }

        //[Column(TypeName = "VARCHAR")]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title must be 200 characters or less")]
        public string Title { get; set; }

        [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int NumberOfEmployees { get; set; }

        //public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
    }
}
