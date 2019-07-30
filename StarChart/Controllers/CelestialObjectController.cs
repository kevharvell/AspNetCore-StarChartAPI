using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
	[Route("")]
	[ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

		[HttpGet("{id:int}", Name = "GetById")]
		public IActionResult GetById(int id)
        {
            var celObj = _context.CelestialObjects.Find(id);
            if (celObj == null)
                return NotFound();
            celObj.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(celObj);
        }

		[HttpGet("{name}")]
		public IActionResult GetByName(string name)
        {
            var celObjs = _context.CelestialObjects.Where(e => e.Name == name).ToList();
            if (!celObjs.Any())
                return NotFound();
			foreach(var celObj in celObjs)
            {
                celObj.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celObj.Id).ToList();
            }
            return Ok(celObjs);
        }

		[HttpGet]
		public IActionResult GetAll()
        {
            var celObjs = _context.CelestialObjects.ToList();
			foreach(var celObj in celObjs)
            {
                celObj.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celObj.Id).ToList();
            }
            return Ok(celObjs);
        }
    }
}
