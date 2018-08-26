using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompatablilityLiberary
{
    public class Compatability
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        public int MasterComponentID { get; set; }
        public int DependantComponentID { get; set; }
        public double MinVersion { get; set; }
        public double MaxVersion { get; set; }
    }
}
