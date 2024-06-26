﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Core.Entities
{
    public class SubType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Days { get; set; }
        public int Price { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public override string ToString()
        {
            return $"{Id};{Title};{Price}";
        }
    }
}
