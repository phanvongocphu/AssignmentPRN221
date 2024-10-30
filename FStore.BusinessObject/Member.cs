using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStore.BusinessObject
{
    public class Member
    {
        [Key]
        [Required]
        public int MemberId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
