using System;
using System.Collections.Generic;

namespace CompatabilityLiberaryCore1.Models
{
    public partial class ComptabilityMatrix
    {
        public int Id { get; set; }
        public int MasterComponentId { get; set; }
        public int DependantComponentId { get; set; }
        public double? MasterVersion { get; set; }
        public double? MinDependVersion { get; set; }
        public double? MaxDependVersion { get; set; }
    }
}
