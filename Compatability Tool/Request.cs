using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compatability_Tool
{
    public class CompatibilityComponent
    {
        public int deviceID1 { get; set; }
        public int deviceID2 { get; set; }
        public double masterVersion { get; set; }
        public double? minVersion { get; set; }
        public double? maxVersion { get; set; }
    }

    public class Request
    {
        public List<CompatibilityComponent> compatibility { get; set; }
    }

    public class options
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}
