
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TAINATechTest.Data.Models
{
    public class Person
    {
        public long Id { get; set; }
        
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
