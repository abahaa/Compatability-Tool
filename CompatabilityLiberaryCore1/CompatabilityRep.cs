using CompatabilityLiberaryCore1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace CompatabilityLiberaryCore1
{
    public class CompatabilityRep
    {
        private string connectionstring { get; set; }

        public CompatabilityRep(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        private List<ComptabilityMatrix> GetPrevAddedList(List<ComptabilityMatrix> NewCompatabilityList)
        {
            ComptabilityContext context = new ComptabilityContext(connectionstring);
            IQueryable<ComptabilityMatrix> query = null;
            foreach (var comp in NewCompatabilityList)
            {
                if (query == null)
                {
                    query = context.ComptabilityMatrix.
                               Where(c => c.MasterVersion == comp.MasterVersion &&
                              c.MasterComponentId == comp.MasterComponentId &&
                              c.DependantComponentId == comp.DependantComponentId);
                }
                else
                {
                    query = query.Where(c => c.MasterVersion == comp.MasterVersion &&
                           c.MasterComponentId == comp.MasterComponentId &&
                           c.DependantComponentId == comp.DependantComponentId);
                }
            }
            return query.ToList(); 
        }

        public void Create(List<ComptabilityMatrix> CompatabilityList)
        {
            ComptabilityContext context = new ComptabilityContext(connectionstring);
            List<Releases> AddedReleases = new List<Releases>();
            foreach (var comp in CompatabilityList)
            {
                #region LoadAllEntities
                //List<ComptabilityMatrix> BigMatrix = context.ComptabilityMatrix.ToList();
                //ComptabilityMatrix elem = BigMatrix.Find(x => x.MasterComponentId == comp.MasterComponentId);
                //if (elem != null)
                //{
                //    context.ComptabilityMatrix.Add(comp);
                //}
                #endregion
            
                //Check if the master component is not compatabile with the dependant component
                if (comp.MasterVersion != null && comp.MinDependVersion != null)
                {
                    //Check if component with master component ID Already Exists
                    List<ComptabilityMatrix> ExistingCompatabilities = 
                        context.ComptabilityMatrix.
                        Where(c => c.MasterVersion == comp.MasterVersion &&
                        c.MasterComponentId == comp.MasterComponentId &&
                        c.DependantComponentId == comp.DependantComponentId).ToList();

                    

                    bool isExist = false;
                    if (ExistingCompatabilities.Count() > 0)
                    {
                        foreach (var existingcomp in ExistingCompatabilities)
                        {
                            //check if min entered component ID is between the existing interval 
                            if ((existingcomp.MinDependVersion <= comp.MinDependVersion
                                && existingcomp.MaxDependVersion >= comp.MinDependVersion) ||

                                (existingcomp.MaxDependVersion >= comp.MinDependVersion
                                && existingcomp.MaxDependVersion <= comp.MaxDependVersion))
                            {
                                if (existingcomp.MaxDependVersion < comp.MaxDependVersion)
                                {
                                    existingcomp.MaxDependVersion = comp.MaxDependVersion;
                                    context.Update<ComptabilityMatrix>(existingcomp);
                                }
                                if(existingcomp.MinDependVersion > comp.MinDependVersion)
                                {
                                    existingcomp.MinDependVersion = comp.MinDependVersion;
                                    context.Update<ComptabilityMatrix>(existingcomp);
                                }
                                isExist = true;
                                break;
                            }
                        }
                    }
                    else 
                    {
                        //Check if it was added before
                        Releases toAdd = new Releases()
                        {
                            ReleaseNo = (double)comp.MasterVersion,
                            ComponentId = comp.MasterComponentId
                        };

                        if (AddedReleases.Where(r => 
                        r.ComponentId == toAdd.ComponentId 
                        && r.ReleaseNo == toAdd.ReleaseNo).ToList().Count() == 0)
                        {
                            AddedReleases.Add(toAdd);
                            ReleasesRep repo = new ReleasesRep(connectionstring);
                            repo.Add(toAdd);
                        }
                    }
                    if (!isExist)
                    {
                        context.ComptabilityMatrix.Add(comp);
                    }
                }
            }
            context.SaveChanges();
        }

        public List<ComptabilityMatrix> GetCompatabileReleases(int MasterComponentID,int DependComponentID,double MasterComponentVersion)
        {
            ComptabilityContext context = new ComptabilityContext(connectionstring);
            List<ComptabilityMatrix> MatchedList =  context.ComptabilityMatrix.Where(cm => cm.MasterComponentId == MasterComponentID &&
                                                                cm.DependantComponentId == DependComponentID &&
                                                                cm.MasterVersion == MasterComponentVersion).ToList();

            return MatchedList;
        }

        //private bool check_compatabiilty(List<ComptabilityMatrix> BigList , ComptabilityMatrix newComp)
        //{
            
        //}
    }
}
