using System;
using System.Collections.Generic;
using System.Text;
using CompatabilityLiberaryCore1.Models;
using System.Linq;

namespace CompatabilityLiberaryCore1
{
    public class ReleasesRep
    {
        private string  connectionstring { get; set; }

        public ReleasesRep(string connectionstring)
        {
            this.connectionstring = connectionstring; 
        }

        public void Add(Releases release)
        {
            ComptabilityContext context = new ComptabilityContext(connectionstring);
            context.Releases.Add(release);
            context.SaveChanges();
        }

        public List<double> GetReleasesByID(int MasterCompID)
        {
            ComptabilityContext context = new ComptabilityContext(connectionstring);
            List<double> ReleasesVersions = (from r in context.Releases
                                          where r.ComponentId == MasterCompID
                                          select r.ReleaseNo).ToList();
            return ReleasesVersions;
        }
    }
}
