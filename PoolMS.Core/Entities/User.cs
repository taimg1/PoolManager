using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Core.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }

        public override string ToString()
        {
            return $"{Id};{Fname};{Sname};{Email};{Phone};{Role.Id}";
        }
    }
}
