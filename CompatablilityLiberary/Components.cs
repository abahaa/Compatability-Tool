using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompatablilityLiberary
{
    public class Components
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
