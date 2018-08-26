using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompatabilityLiberaryCore1;

using CompatabilityLiberaryCore1.Models;
using Microsoft.Extensions.Configuration;
using Compatability_Tool.Enum;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Compatability_Tool.Controllers
{
    [Produces("application/json")]
    public class CompatabilityController : Controller
    {
        private ContextUnitOfWork contextUnitOfWork ;
        private List<options> DevicesOptions;

        public CompatabilityController(IConfiguration configuration)
        {
            string connectionstring = configuration.GetConnectionString("DataConnection");
            contextUnitOfWork = new ContextUnitOfWork(connectionstring);
            DevicesOptions = configuration.GetSection("Devices:Options").Get<List<options>>();         
        }

        [Route("api/post")]
        public IActionResult Post([FromBody]Request requset)
        {
            try
            {
                if (requset.compatibility == null)
                {
                    return BadRequest("The File You uploaded contains errors");
                }
                List<ComptabilityMatrix> Matrix = new List<ComptabilityMatrix>();
                foreach (var component in requset.compatibility)
                {
                    if(component.deviceID1 == 0 || component.deviceID2 == 0 || component.masterVersion == 0 || component.minVersion > component.maxVersion)
                    {
                        return BadRequest("The File You uploaded contains errors");
                    }
                    Matrix.Add(new ComptabilityMatrix()
                    {
                        MasterComponentId = component.deviceID1,
                        DependantComponentId = component.deviceID2,
                        MasterVersion = component.masterVersion,
                        MinDependVersion = component.minVersion,
                        MaxDependVersion = component.maxVersion
                    });
                }

                contextUnitOfWork.compatabilityRep.Create(Matrix);

                return Ok("Data is Saved Successfully");
            }
            catch (Exception ex)
            {
                return NotFound("An exception occured During Execution");
            }
        }

        [Route("/api/get/{MasterComponentID:int}/{DependComponentID:int}/{MasterComponentVersion:double}")]
        public IActionResult Get(int MasterComponentID, int DependComponentID, double MasterComponentVersion)
        {
            List<ComptabilityMatrix> MatchedReleases = contextUnitOfWork.compatabilityRep.GetCompatabileReleases(MasterComponentID, DependComponentID, MasterComponentVersion);

            if (MatchedReleases.Count > 0)
            {
                //string matchedReleasesIntervals = "";
                //foreach (var release in MatchedReleases)
                //{
                //    matchedReleasesIntervals += String.Format("<p> {0} Version:{1:0.0} is Compatabile with  {2} at versions from version:{3:0.0} to version:{4:0.0}</p>",
                //                                                DevicesOptions.Where(d => d.id == MasterComponentID).FirstOrDefault().Name, MasterComponentVersion , DevicesOptions.Where(d => d.id == DependComponentID).FirstOrDefault().Name,
                //                                                release.MinDependVersion, release.MaxDependVersion);
                //}

                return Ok(MatchedReleases);
            }
            return NotFound("No Such Compatability Between Both of these Devices");
        }

        [Route("/api/getReleases/{MasterComponentID:int}")]
        public IActionResult GetReleases(int MasterComponentID)
        {
            return Ok(contextUnitOfWork.releasesRep.GetReleasesByID(MasterComponentID));
        }
    }
}