using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Compatability_Tool.Controllers
{
    public class Emp
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Emp()
        {
            ID = 1;
            Name = "Ahmed";
        }
    }

    [Produces("application/json")]
    [Route("api/Testing")]
    public class TestingController : Controller
    {
        [Route("/api/test/")]
        [HttpGet]
        public IActionResult GO()
        {
            List<Emp> employees = new List<Emp>()
            {
                new Emp(),
                new Emp(),
                new Emp(),
                new Emp(),
                new Emp(),
                new Emp(),
                new Emp(),
                new Emp(),

            };
            return StatusCode(StatusCodes.Status415UnsupportedMediaType);
            
        }
    }
}