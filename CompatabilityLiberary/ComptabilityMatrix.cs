//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompatabilityLiberary
{
    using System;
    using System.Collections.Generic;
    
    public partial class ComptabilityMatrix
    {
        public int ID { get; set; }
        public int MasterComponentID { get; set; }
        public int DependantComponentID { get; set; }
        public double minVersion { get; set; }
        public double maxVersion { get; set; }
    }
}