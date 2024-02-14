using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Core.Entities
{
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public virtual Reservation? Reservation { get; set; }
        public virtual User User { get; set; }
        public virtual Pool Pool { get; set; }
        public int StayTime { get; set; }
        public override string ToString()
        {
            return $"{Id};{Date};{Reservation.Id};{StayTime};{User.Id};{Pool.Id}";
        }

    }

}
