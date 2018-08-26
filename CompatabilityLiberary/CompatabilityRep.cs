using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompatabilityLiberary
{
    public class CompatabilityRep
    {
        public static void Create(List<ComptabilityMatrix> CompatabilityList)
        {
            ComptabilityEntities context = new ComptabilityEntities();
            foreach(var comp in CompatabilityList)
            {
                context.ComptabilityMatrices.Add(comp);
            }
            context.SaveChanges();
        }
    }
}
