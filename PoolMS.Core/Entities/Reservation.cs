using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Core.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Subscription Subscription { get; set; }
        public DateTime Date { get; set; }
        public override string ToString()
        {
            return $"{Id};{Subscription.Id};{Date}";
        }
    }
}
