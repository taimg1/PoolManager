using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Core.Entities
{
    public class Pool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual PoolSize Size { get; set; }
        public int MaxCapacity { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    public override string ToString()
        {
            return $"{Size.Title};{MaxCapacity}";
        }
    }
}
